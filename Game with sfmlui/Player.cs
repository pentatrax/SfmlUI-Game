using SFML.Graphics;
using SFML.System;
using SfmlUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Game_with_sfmlui
{
    class Player
    {
        // Global values
        private RenderWindow _window;
        private Vector2f _unit;
        private Color _color;
        //private Font _font;

        // Positional values
        private Vector2f _position;
        private Vector2f _velocity;
        private Vector2f _acceleration;

        // Player parts
        //private List<Bodypart> _bodyParts = new List<Bodypart>();
        //private Text name;
        private Bodypart _torso;

        // Player properties
        public Vector2f Position 
        { 
            get { return _torso.Position; } 
            set { _position = value; _torso.Position = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; _torso.Color = value; }
        }
        public Vector2f Acceleration { get { return _acceleration; } set { _acceleration = value; } }

        public Player(RenderWindow window, Vector2f position)
        {
            _window = window;
            _unit = new Vector2f(window.Size.X / 100f, window.Size.Y / 100f);
            _torso = new Bodypart(window, position, Bodypart.Type.Torso, new Sprite(new Texture(new Image("rsrc/player.png"))));
            _torso.Size = new Vector2f(15f * _unit.X, 3f * _unit.Y);
        }

        public void Draw()
        {
            _torso.Draw();
        }
        public void Update(TimeSpan deltaT) 
        {
            float time = Convert.ToSingle(deltaT.TotalMilliseconds / 1000);
            _velocity += new Vector2f((_acceleration.X * _unit.X) * time, 0f);
            Position += _velocity;

            if (_position.X - _torso.Size.X * 0.5f < 0)
            {
                Position += -1 * _velocity;
            } else if (_position.X + _torso.Size.X * 0.5f > _window.Size.X)
            {
                Position += -1 * _velocity;
            }
            _velocity = new Vector2f(_velocity.X * 0.95f, 0f);
            //Console.WriteLine(_velocity.X * 0.95f);
        }
    }
}
