using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Game_with_sfmlui
{
    class Game
    {
        // Global values
        private Vector2f _position;
        private Font _font;
        private RenderWindow _window;
        private float PlayerSpeed = 10;
        public float _volume;

        // Game events
        public event EventHandler BackToMenu;
        public event EventHandler PauseGame;
        public event EventHandler GameOver;

        // Controlls
        private Controlls _controller;


        // Game Generic Entities
        private Player _player;


        public Game(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
            _volume = 1;
            _position = new Vector2f(0, 0);

            _player = new Player(window, new Vector2f(window.Size.X * 0.5f, window.Size.Y));
            _player.Color = Color.Yellow;

            _controller = new Controlls(window, Controlls.Type.WASD);

        }
        public void Update(TimeSpan deltaT)
        {   
            _player.Update(deltaT);

            switch (_controller.GetInput())
            {
                case Controlls.Input.ESC: BackToMenu?.Invoke(this, EventArgs.Empty); break;
                case Controlls.Input.Left: _player.Acceleration = new Vector2f(-1 * PlayerSpeed, 0); break;
                case Controlls.Input.Right: _player.Acceleration = new Vector2f(PlayerSpeed, 0); break;
                default: _player.Acceleration = new Vector2f(0, 0); break;
            }
        }
        public void Draw()
        {
            _player.Draw();
        }
    }
}
