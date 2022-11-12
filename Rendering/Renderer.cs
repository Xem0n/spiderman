using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace spiderman.Rendering
{
    internal static class Renderer
    {
        static Vector2i _resolution = new(1280, 720);

        static public Vector2i Resolution
        {
            get => _resolution;
            set
            {
                GL.Viewport(0, 0, value.X, value.Y);
                _resolution = value;
            }
        }

        static public void Setup()
        {
            GL.Enable(EnableCap.Multisample);
            GL.Hint(HintTarget.MultisampleFilterHintNv, HintMode.Nicest);
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            //GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            //GL.ActiveTexture(TextureUnit.Texture0);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            //GL.Enable(EnableCap.Texture2D);
            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
        }

        static public void Unload()
        {
            ShaderManager.CleanUp();
            BatchManager.CleanUp();
        }

        static public void StartRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        static public void FinishRender(IGLFWGraphicsContext context)
        {
            BatchManager.Render();

            context.SwapBuffers();
        }
    }
}
