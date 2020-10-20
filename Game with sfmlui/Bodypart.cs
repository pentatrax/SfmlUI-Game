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
        private Vector2f _link;
        private Type _type;
        private Sprite _sprite;

        public int zIndex { get { return _zIndex; } set { _zIndex = value; } }
        public float Rotation { get { return _sprite.Rotation; } set { _sprite.Rotation = value; } }
        public Vector2f Link { get { return _link; } set { _link = value; _sprite.Position = value; } }

        public Bodypart(RenderWindow window, Vector2f link, Type type, Sprite sprite)
        {
            _window = window;
            _link = link;
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
    }
}
