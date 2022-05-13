using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace spiderman.Rendering
{
    internal class Texture
    {
        int _handle;

        public Texture(string path)
        {
            _handle = GL.GenTexture();

            Load(path);
        }

        private void Load(string path)
        {
            Use();

            path = String.Format(@"E:\cs\spiderman\Textures\{0}", path);

            Image<Rgba32> image = Image.Load<Rgba32>(path);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            byte[] pixels = GetImagePixels(image);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
        }

        private byte[] GetImagePixels(Image<Rgba32> image)
        {
            byte[] pixels = new byte[4 * image.Width * image.Height];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    pixels.Append(pixel.R);
                    pixels.Append(pixel.G);
                    pixels.Append(pixel.B);
                    pixels.Append(pixel.A);
                }
            }

            return pixels;
        }

        public void Use()
        {
            GL.BindTexture(TextureTarget.Texture2D, _handle);
        }
    }
}
