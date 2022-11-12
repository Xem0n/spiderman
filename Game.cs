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

namespace spiderman
{
    internal class Game : GameWindow
    {
        World _world;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _world = new(this);
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

            _world.Update();

            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Renderer.StartRender();

            _world.Draw();

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
