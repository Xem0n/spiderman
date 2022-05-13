using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiderman.Rendering
{
    internal class LineBatchConfig : BatchConfig
    {
        public int Width;

        public LineBatchConfig(PrimitiveType primitive, BufferUsageHint renderType, int shaderId, int width) : base(primitive, renderType, shaderId)
        {
            Width = width;
        }

        public override void BeforeRender()
        {
            GL.LineWidth(Width);
        }

        public static bool operator ==(LineBatchConfig a, LineBatchConfig b) =>
            a.Primitive == b.Primitive &&
            a.RenderType == b.RenderType &&
            a.ShaderId == b.ShaderId &&
            a.Width == b.Width;

        public static bool operator !=(LineBatchConfig a, LineBatchConfig b) => !(a == b);

    }
}
