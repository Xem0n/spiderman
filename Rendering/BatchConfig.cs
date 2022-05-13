using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace spiderman.Rendering
{
    internal class BatchConfig
    {
        public PrimitiveType Primitive;
        public BufferUsageHint RenderType;
        public int ShaderId;

        public BatchConfig(PrimitiveType primitive, BufferUsageHint renderType, int shaderId)
        {
            Primitive = primitive;
            RenderType = renderType;
            ShaderId = shaderId;
        }

        public virtual void BeforeRender() { }

        public static bool operator ==(BatchConfig a, BatchConfig b) => 
            a.Primitive == b.Primitive && 
            a.RenderType == b.RenderType && 
            a.ShaderId == b.ShaderId;

        public static bool operator !=(BatchConfig a, BatchConfig b) => !(a == b);
    }
}
