using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class Bodypart
    {
        public enum Type { Head, Limb, Torso }
        private RenderWindow _window;
        private int _zIndex = 0;
        private Type _type;
        private Sprite _sprite;

        public int zIndex { get { return _zIndex; } set { _zIndex = value; } }
        public float Rotation { get { return _sprite.Rotation; } set { _sprite.Rotation = value; } }
        public Vector2f Size { 
            get { return new Vector2f(_sprite.GetGlobalBounds().Width, _sprite.GetGlobalBounds().Height); }
            //set { _sprite.Scale = new Vector2f((Size.X / 100f) * )}
        }

        public Bodypart(RenderWindow window, Vector2f link, Type type, Sprite sprite)
        {
            _window = window;
            _type = type;
            _sprite = sprite;
            switch (type)
            {
                case Type.Limb:
                    _sprite.Origin = new Vector2f(sprite.GetGlobalBounds().Width * 0.5f, sprite.GetGlobalBounds().Height * 0.05f);
                    break;
                case Type.Head:
                    _sprite.Origin = new Vector2f(sprite.GetGlobalBounds().Width * 0.5f, sprite.GetGlobalBounds().Height * 0.95f);
                    break;
                case Type.Torso:
                    _sprite.Origin = new Vector2f(sprite.GetGlobalBounds().Width * 0.5f, sprite.GetGlobalBounds().Height);
                    break;
            }
            _sprite.Position = link;

        }

        public void Draw()
        {
            _window.Draw(_sprite);
        }

        public void Rotate(float angle)
        {
            _sprite.Transform.Rotate(angle);
        }

        public void UpdatePos(Vector2f position)
        {
            _sprite.Position = position;
        }
    }
}
