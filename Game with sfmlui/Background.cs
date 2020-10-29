using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class Background
    {
        private RenderWindow _window;
        private Sprite _image;

        public Vector2f Position { 
            get
            {
                return new Vector2f(_image.GetGlobalBounds().Left, _image.GetGlobalBounds().Top);
            } 
            set
            {
                _image.Position = value;
            }
        }
        public Vector2f Scale
        {
            get
            {
                return _image.Scale;
            }
            set
            {
                _image.Scale = value;
            }
        }
        public Image Image
        {
            get
            {
               return _image.Texture.CopyToImage();
            }
            set
            {
                _image.Texture.Swap(new Texture(value, new IntRect(0, 0, (int)value.Size.X, (int)value.Size.Y)));
            }
        }


        public Background(RenderWindow window, Image image)
        {
            _window = window;
            IntRect tempIntRect = new IntRect(0, 0, (int)image.Size.X, (int)image.Size.Y);
            _image = new Sprite(new Texture(image, tempIntRect), tempIntRect);
            _image.Position = new Vector2f(0, 0);
            _image.Scale = new Vector2f((float)window.Size.X / (float)image.Size.X, (float)window.Size.Y / (float)image.Size.Y);
            //Console.WriteLine("Background to screen ratio: " + _image.Scale.ToString());
            _image.Texture.Smooth = false;
            _image.Texture.Srgb = true;
        }

        public void Draw()
        {
            _window.Draw(_image);
        }


    }
}
