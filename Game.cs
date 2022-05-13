using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Core;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using spiderman.Rendering;
using spiderman.Entities;

namespace spiderman
{
    internal class Game : GameWindow
    {
        Player _player = new();

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            Renderer.Setup();

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            Renderer.Unload();

            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            _player.Update(args, MouseState);

            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Renderer.StartRender();

            _player.Draw();

            Renderer.FinishRender(Context);

            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            Renderer.Resolution = new(e.Width, e.Height);

            base.OnResize(e);
        }
    }
}
