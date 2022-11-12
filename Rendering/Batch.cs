using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace spiderman.Rendering
{
    internal class Batch : IDisposable
    {
        int _vertexArray;
        int _vertexBuffer;
        int _elementBuffer;

        int _maxVertices = 100_000;
        int _usedVertices = 0;

        int _maxIndices = 10_000;
        int _usedIndices = 0;

        uint _largestIndex = 0;

        BatchConfig _config;

        bool _disposed = false;

        public Batch(BatchConfig config)
        {
            GL.GetError();
            _config = config;

            InitBuffers();
        }

        private void InitBuffers()
        {
            _vertexArray = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArray);

            _vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, _maxVertices * sizeof(float), (IntPtr)null, _config.RenderType);

            _elementBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _maxIndices * sizeof(uint), (IntPtr)null, _config.RenderType);

            for (int i = 0; i < _config.Attributes.Length; i++)
            {
                int[] attribute = _config.Attributes[i];

                GL.VertexAttribPointer(i, attribute[0], VertexAttribPointerType.Float, false, attribute[1] * sizeof(float), attribute[2] * sizeof(float));
                GL.EnableVertexAttribArray(i);
            }

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Add(float[] vertices, uint[] indices)
        {
            indices = TransformIndices(indices);

            GL.BindVertexArray(_vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBuffer);

            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)(_usedVertices * sizeof(float)), (IntPtr)(vertices.Length * sizeof(float)), vertices);
            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)(_usedIndices * sizeof(uint)), (IntPtr)(indices.Length * sizeof(uint)), indices);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            _usedIndices += indices.Length;
            _usedVertices += vertices.Length;
        }

        // TODO: make it better xd
        private uint[] TransformIndices(uint[] indices)
        {
            uint[] transformed = _largestIndex == 0 ? indices : new uint[indices.Length];
            uint largestIndex = _largestIndex;

            for (int i = 0; i < indices.Length; i++)
            {
                if (_largestIndex > 0)
                    transformed[i] = indices[i] + _largestIndex;

                if (transformed[i] > largestIndex)
                    largestIndex = transformed[i];
            }

            _largestIndex = largestIndex + 1;

            return transformed;
        }

        public void Render()
        {
            _config.BeforeRender();
            ShaderManager.Get(_config.ShaderId).Use();

            GL.BindVertexArray(_vertexArray);
            GL.DrawElements(_config.Primitive, _usedIndices, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            _usedVertices = 0;
            _usedIndices = 0;
            _largestIndex = 0;
        }

        public bool EqualsBatchConfig(BatchConfig config) => _config == config;

        public bool CanFit(float[] vertices, uint[] indices) =>
            (_usedVertices + vertices.Length) <= _maxVertices &&
            (_usedIndices + indices.Length) <= _maxIndices;

        public bool IsEmpty() => _usedVertices == 0;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            CleanUp();

            _disposed = true;
        }

        ~Batch()
        {
            Dispose(disposing: false);
        }

        private void CleanUp()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vertexBuffer);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(_elementBuffer);

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(_vertexArray);
        }
    }
}
