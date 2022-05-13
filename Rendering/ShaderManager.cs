using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiderman.Rendering
{
    internal static class ShaderManager
    {
        static List<Shader> _shaders = new();

        public static int Get(string shaderName)
        {
            int shaderIndex = _shaders.FindIndex(x => x.ShaderName == shaderName);

            if (shaderIndex == -1)
            {
                _shaders.Add(new(shaderName));
                shaderIndex = _shaders.Count - 1;
            }

            return shaderIndex;
        }

        public static Shader Get(int shaderId)
        {
            return _shaders[shaderId];
        }

        public static void CleanUp()
        {
            foreach (var shader in _shaders)
            {
                shader.Dispose();
            }
        }
    }
}
