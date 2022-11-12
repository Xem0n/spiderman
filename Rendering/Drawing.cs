using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace spiderman.Rendering
{
    internal static class Drawing
    {
        static float _zIndex = 1.0f;
        const float ZIndexModifier = 0.000001f;

        static public void Triangle(Vector2i a, Vector2i b, Vector2i c, Color4 color)
        {
            float[] aPos = Transform(a);
            float[] bPos = Transform(b);
            float[] cPos = Transform(c);

            float[] vertices =
            {
                aPos[0], aPos[1], _zIndex, color.R, color.G, color.B,
                bPos[0], bPos[1], _zIndex, color.R, color.G, color.B,
                cPos[0], cPos[1], _zIndex, color.R, color.G, color.B
            };

            uint[] indices =
            {
                0, 1, 2
            };

            BatchConfig config = new(
                PrimitiveType.Triangles,
                BufferUsageHint.StreamDraw,
                ShaderManager.Get("solid")
            );

            BatchManager.Add(vertices, indices, config);
            _zIndex -= ZIndexModifier;
        }

        static public void Rectangle(Vector2i pos, Vector2i size, Color4 color)
        {
            float[] leftTop = Transform(pos);
            float[] leftBottom = Transform(new(pos.X, pos.Y + size.Y));
            float[] rightTop = Transform(new(pos.X + size.X, pos.Y));
            float[] rightBottom = Transform(new(pos.X + size.X, pos.Y + size.Y));

            float[] vertices =
            {
                leftTop[0], leftTop[1], _zIndex, color.R, color.G, color.B,
                leftBottom[0], leftBottom[1], _zIndex, color.R, color.G, color.B,
                rightTop[0], rightTop[1], _zIndex, color.R, color.G, color.B,
                rightBottom[0], rightBottom[1], _zIndex, color.R, color.G, color.B
            };

            uint[] indices =
            {
                0, 1, 2,
                1, 2, 3
            };

            BatchConfig config = new(
                PrimitiveType.Triangles,
                BufferUsageHint.StreamDraw,
                ShaderManager.Get("solid")
            );

            BatchManager.Add(vertices, indices, config);
            _zIndex -= ZIndexModifier;
        }

        static public void Rectangle(Vector2i pos, Vector2i size, Texture texture)
        {
            int width = Renderer.Resolution.X;
            int height = Renderer.Resolution.Y;

            float[] leftTop = Transform(pos);
            float[] leftBottom = Transform(new(pos.X, pos.Y + size.Y));
            float[] rightTop = Transform(new(pos.X + size.X, pos.Y));
            float[] rightBottom = Transform(new(pos.X + size.X, pos.Y + size.Y));

            float[] vertices =
            {
                //rightTop[0], rightTop[1], _zIndex, rightTop[0] / width, rightTop[1] / height,
                //rightBottom[0], rightBottom[1], _zIndex, rightBottom[0] / width, rightBottom[1] / height,
                //leftBottom[0], leftBottom[1], _zIndex, leftBottom[0] / width, leftBottom[1] / height,
                //leftTop[0], leftTop[1], _zIndex, leftTop[0] / width, leftTop[1] / height,
                rightTop[0], rightTop[1], _zIndex, 1.0f, 1.0f,
                rightBottom[0], rightBottom[1], _zIndex, 1.0f, 0.0f,
                leftBottom[0], leftBottom[1], _zIndex, 0.0f, 0.0f,
                leftTop[0], leftTop[1], _zIndex, 0.0f, 1.0f
                //rightTop[0], rightTop[1], _zIndex, rightTop[0], rightTop[1],
                //rightBottom[0], rightBottom[1], _zIndex, rightBottom[0], rightBottom[1],
                //leftBottom[0], leftBottom[1], _zIndex, leftBottom[0], leftBottom[1],
                //leftTop[0], leftTop[1], _zIndex, leftTop[0], leftTop[1]
                //0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
                 //0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
                //-0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
                //-0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
            };

            uint[] indices =
            {
                0, 1, 3,
                1, 2, 3
            };

            TextureBatchConfig config = new(
                PrimitiveType.Triangles,
                BufferUsageHint.StreamDraw,
                ShaderManager.Get("texture"),
                texture
            );

            BatchManager.Add(vertices, indices, config);
            _zIndex -= ZIndexModifier;
        }

        static public void Circle(Vector2i center, int radius, Color4 color)
        {
            Vector2i pos = new(0, radius);
            int d = 3 - 2 * radius;

            Circle(center, pos, color);

            while (pos.Y >= pos.X)
            {
                pos.X++;

                if (d > 0)
                {
                    pos.Y--;
                    d = d + 4 * (pos.X - pos.Y) + 10;
                }
                else
                {
                    d = d + 4 * pos.X + 6;
                }

                Circle(center, pos.X, color);
            }
        }

        static private void Circle(Vector2i center, Vector2i pos, Color4 color)
        {
            Drawing.Point(new(center.X + pos.X, center.Y + pos.Y), color);
        }

        static public void Line(Vector2i start, Vector2i finish, int width, Color4 color)
        {
            float[] startPos = Transform(start);
            float[] finishPos = Transform(finish);

            float[] vertices =
            {
                startPos[0], startPos[1], _zIndex, color.R, color.G, color.B,
                finishPos[0], finishPos[1], _zIndex, color.R, color.G, color.B
            };

            uint[] indices =
            {
                0, 1
            };

            LineBatchConfig config = new(
                PrimitiveType.Lines,
                BufferUsageHint.StreamDraw,
                ShaderManager.Get("solid"),
                width
            );

            BatchManager.Add(vertices, indices, config);
            _zIndex -= ZIndexModifier;
        }

        static public void Point(Vector2i pos, Color4 color)
        {
            float[] transformed = Transform(pos);

            float[] vertices =
            {
                transformed[0], transformed[1], _zIndex, color.R, color.G, color.B
            };

            uint[] indices = { 0 };

            BatchConfig config = new(
                PrimitiveType.Points,
                BufferUsageHint.StreamDraw,
                ShaderManager.Get("solid")
            );

            BatchManager.Add(vertices, indices, config);
            _zIndex -= ZIndexModifier;
        }

        static float[] Transform(Vector2i pos) =>
            new float[] {
                (float)((2.0 * pos.X + 1.0) / Renderer.Resolution.X - 1.0),
                (float)((2.0 * pos.Y + 1.0) / Renderer.Resolution.Y - 1.0)
            };
    }
}
