using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;
using spiderman.Rendering;

namespace spiderman.Entities
{
    internal class Player
    {
        Vector2i _size = new(40, 40);
        Vector2i _pos;
        Color4 _color = new(0.9f, 0.22f, 0.27f, 1f);

        bool _isLine = false;
        Vector2i _lineStart;
        Vector2i _lineFinish;

        public Player()
        {
            _pos = new(
                Renderer.Resolution.X / 2 - _size.X / 2,
                Renderer.Resolution.Y / 2 - _size.Y / 2
            );

            _lineStart = new(
                _pos.X + _size.X / 2,
                _pos.Y + _size.Y / 2
            );
        }

        public void Update(FrameEventArgs args, MouseState mouse)
        {
            if (!mouse[MouseButton.Left])
            {
                _isLine = false;
                return;
            }

            if (_isLine)
                return;

            _lineFinish.X = (int)mouse.X;
            _lineFinish.Y = Renderer.Resolution.Y - (int)mouse.Y;
            _isLine = true;
        }

        public void Draw()
        {
            Drawing.Rectangle(_pos, _size, _color);

            if (_isLine)
                Drawing.Line(_lineStart, _lineFinish, 2, _color);
        }
    }
}
