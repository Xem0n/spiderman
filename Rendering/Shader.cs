using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace spiderman.Rendering
{
    internal class Shader : IDisposable
    {
        public string ShaderName { get; }

        int _handle;
        bool _disposed = false;

        public Shader(string shaderName)
        {
            ShaderName = shaderName;

            (int Vertex, int Fragment) shader = Compile();
            _handle = CreateProgram(shader.Vertex, shader.Fragment);

        }

        private (int, int) Compile()
        {
            string path = @"E:\cs\spiderman\Shaders\";
            string vertexPath = path + ShaderName + ".vert";
            string fragmentPath = path + ShaderName + ".frag";
            string vertexShaderSource, fragmentShaderSource;
            int vertexShader, fragmentShader;

            using (StreamReader reader = new(vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new(fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            GL.CompileShader(vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(vertexShader);

            if (infoLogVert != String.Empty)
                Console.WriteLine(infoLogVert);

            GL.CompileShader(fragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

            if (infoLogFrag != String.Empty)
                Console.WriteLine(infoLogFrag);

            return (vertexShader, fragmentShader);
        }

        private int CreateProgram(int vertexShader, int fragmentShader)
        {
            int handle = GL.CreateProgram();

            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);

            GL.LinkProgram(handle);

            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return handle;
        }

        public void Use()
        {
            GL.UseProgram(_handle);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            GL.DeleteProgram(_handle);

            _disposed = true;
        }

        ~Shader()
        {
            Dispose(disposing: false);
        }
    }
}
