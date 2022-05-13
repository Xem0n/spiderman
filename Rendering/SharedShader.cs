using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Core;
using OpenTK.Graphics.OpenGL4;


namespace spiderman.Rendering
{
    internal class SharedShader : IDisposable
    {
        public int Shader { get; }
        bool _disposed = false;

        public SharedShader(string shaderName)
        {
            Shader = Compile(shaderName);
        }

        private int Compile(string shaderName)
        {
            string path = @"E:\cs\spiderman\Shaders\Shared\";
            string shaderPath = path + shaderName + ".vert";
            string shaderSource;
            int shader;

            using (StreamReader reader = new(shaderPath, Encoding.UTF8))
            {
                shaderSource = reader.ReadToEnd();
            }

            shader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(shader, shaderSource);

            GL.CompileShader(shader);

            string infoLog = GL.GetShaderInfoLog(shader);

            if (infoLog != String.Empty)
                Console.WriteLine(infoLog);

            return shader;
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

            GL.DeleteShader(Shader);

            _disposed = true;
        }

        ~SharedShader()
        {
            Dispose(disposing: false);
        }
    }
}
