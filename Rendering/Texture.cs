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
        static List<Texture> _textures = new();

        string _path;
        int _handle;

        public static Texture Get(string path)
        {
            Texture texture;
            int index = _textures.FindIndex(x => x._path == path);

            if (index == -1)
            {
                texture = new(path);
            }
            else
            {
                texture = _textures[index];
            }

            return texture;
        }

        private Texture(string path)
        {
            _path = path;
            _handle = GL.GenTexture();

            Load();

            _textures.Add(this);
        }

        private void Load()
        {
            Use();

            string path = String.Format(@"E:\cs\spiderman\Textures\{0}", _path);

            Image<Rgba32> image = Image.Load<Rgba32>(path);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            byte[] pixels = GetImagePixels(image);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            image.Dispose();
        }

        private byte[] GetImagePixels(Image<Rgba32> image)
        {
            //byte[] pixels = new byte[4 * image.Width * image.Height];
            List<byte> pixels = new(4 * image.Width * image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    //pixels[y * x + 0] = pixel.R;
                    //pixels[y * x + 1] = pixel.G;
                    //pixels[y * x + 2] = pixel.B;
                    //pixels[y * x + 3] = pixel.A;
                    //pixels.Append(pixel.R);
                    //pixels.Append(pixel.G);
                    //pixels.Append(pixel.B);
                    //pixels.Append(pixel.A);
                    pixels.Add(pixel.R);
                    pixels.Add(pixel.G);
                    pixels.Add(pixel.B);
                    pixels.Add(pixel.A);
                }
            }

            return pixels.ToArray();
        }

        public void Use()
        {
            //GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _handle);
        }

        public static bool operator ==(Texture a, Texture b) => a._handle == b._handle;

        public static bool operator !=(Texture a, Texture b) => !(a == b);
    }
}
