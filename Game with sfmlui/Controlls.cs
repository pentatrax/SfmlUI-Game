using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Game_with_sfmlui
{
    class Controlls
    {
        // Global values
        public enum Type { WASD, Arrows, Mouse }
        public enum Direction { Left, Right, NONE }
        private RenderWindow _window;

        // Events
        //public event EventHandler Up;
        //public event EventHandler Down;
        //public event EventHandler Left;
        //public event EventHandler Right;
        public event EventHandler MouseMove;

        // Variables
        private Type _controllType;
        private Direction _direction;
        //private bool _left = false;
        //private bool _right = false;

        public Controlls(RenderWindow window, Type controllType)
        {
            _window = window;
            _controllType = controllType;
            _direction = Direction.NONE;
            switch (_controllType)
            {
                case Type.WASD:
                    window.KeyPressed += OnWASDPressEvent;
                    window.KeyReleased += OnWASDReleaseEvent;
                    break;
                case Type.Arrows:
                    window.KeyPressed += OnArrowsPressEvent;
                    window.KeyReleased += OnArrowsReleaseEvent;
                    break;
                case Type.Mouse:
                    window.MouseMoved += OnMouseMoveEvent;
                    break;
                default:
                    break;
            }

        }

        // Events

        // WASD Events
        private void OnWASDPressEvent(object sender, KeyEventArgs e) 
        {
            if (e.Code == Keyboard.Key.A){ _direction = Direction.Left; }
            else if (e.Code == Keyboard.Key.D){ _direction = Direction.Right; }
        }
        private void OnWASDReleaseEvent(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.A) { _direction = Direction.NONE; }
            else if (e.Code == Keyboard.Key.D) { _direction = Direction.NONE; }
        }

        // Arrow keys events
        private void OnArrowsPressEvent(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left) { _direction = Direction.Left; }
            else if (e.Code == Keyboard.Key.Right) { _direction = Direction.Right; }
        }
        private void OnArrowsReleaseEvent(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left) { _direction = Direction.NONE; }
            else if (e.Code == Keyboard.Key.Right) { _direction = Direction.NONE; }
        }

        // Mouse events
        private void OnMouseMoveEvent(object sender, MouseMoveEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        // Methods

        public Direction GetDirection()
        {
            return _direction;
        }
    }
}
