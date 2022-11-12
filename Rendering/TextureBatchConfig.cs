using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spiderman.Rendering
{
    internal class TextureBatchConfig : BatchConfig
    {
        Texture _texture;

        public new int[][] Attributes = new int[][]
        {
            new int[] { 3, 5, 0 },
            new int[] { 2, 5, 3 }
        };

        public TextureBatchConfig(PrimitiveType primitive, BufferUsageHint renderType, int shaderId, Texture texture) : base(primitive, renderType, shaderId)
        {
            _texture = texture;
        }

        public override void BeforeRender()
        {
            _texture.Use();
        }

        public static bool operator ==(TextureBatchConfig a, TextureBatchConfig b) =>
            a.Primitive == b.Primitive &&
            a.RenderType == b.RenderType &&
            a.ShaderId == b.ShaderId &&
            a._texture == b._texture;

        public static bool operator !=(TextureBatchConfig a, TextureBatchConfig b) => !(a == b);
    }
}
