using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Game_with_sfmlui
{
    class Controlls
    {
        // Global values
        public enum Type { WASD, Arrows, Keyboard, Mouse }
        public enum Direction { Left, Right, NONE }
        private RenderWindow _window;

        // Events
        //public event EventHandler Up;
        //public event EventHandler Down;
        public event EventHandler Left;
        public event EventHandler Right;
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
            if (controllType == Type.WASD || controllType == Type.Arrows || controllType == Type.Keyboard)
            {
                window.KeyPressed += OnKeyPressedEvent;
                window.KeyReleased += OnKeyReleasedEvent;
            }
            if (controllType == Type.Mouse)
            {
                window.MouseMoved += OnMouseMoveEvent;
            }

        }

        public async void OnKeyPressedEvent(object sender, KeyEventArgs e) 
        {
            switch (_controllType)
            {
                case Type.WASD: 
                    if (e.Code == Keyboard.Key.A)
                    {
                        _direction = Direction.Left;
                    }
                    if (e.Code == Keyboard.Key.D)
                    {
                        _direction = Direction.Right;
                    }
                    break;
                case Type.Arrows:
                    if (e.Code == Keyboard.Key.Left)
                    {
                        _direction = Direction.Left;
                    }
                    if (e.Code == Keyboard.Key.Right)
                    {
                        _direction = Direction.Right;
                    }
                    break;
                case Type.Keyboard:
                    if (e.Code == Keyboard.Key.Left)
                    {
                        _direction = Direction.Left;
                    }
                    if (e.Code == Keyboard.Key.Right)
                    {
                        _direction = Direction.Right;
                    }
                    if (e.Code == Keyboard.Key.A)
                    {
                        _direction = Direction.Left;
                    }
                    if (e.Code == Keyboard.Key.D)
                    {
                        _direction = Direction.Right;
                    }
                    break;
            }
            await Task.Delay(8);
        }
        public async void OnKeyReleasedEvent(object sender, KeyEventArgs e)
        {
            switch (_controllType)
            {
                case Type.Arrows:
                    if (_direction == Direction.Left || e.Code == Keyboard.Key.Left)
                    {
                        _direction = Direction.NONE;
                    }
                    if (_direction == Direction.Right || e.Code == Keyboard.Key.Right)
                    {
                        _direction = Direction.NONE;
                    }
                    break;
                case Type.WASD:
                    if (_direction == Direction.Left || e.Code == Keyboard.Key.A)
                    {
                        _direction = Direction.NONE;
                    }
                    if (_direction == Direction.Right || e.Code == Keyboard.Key.D)
                    {
                        _direction = Direction.NONE;
                    }
                    break;
                case Type.Keyboard:
                    if (_direction == Direction.Left || e.Code == Keyboard.Key.Left)
                    {
                        _direction = Direction.NONE;
                    }
                    if (_direction == Direction.Right || e.Code == Keyboard.Key.Right)
                    {
                        _direction = Direction.NONE;
                    }
                    if (_direction == Direction.Left || e.Code == Keyboard.Key.A)
                    {
                        _direction = Direction.NONE;
                    }
                    if (_direction == Direction.Right || e.Code == Keyboard.Key.D)
                    {
                        _direction = Direction.NONE;
                    }
                    break;
            }
            await Task.Delay(8);
        }
        public void OnMouseMoveEvent(object sender, MouseMoveEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }
        public Direction GetDirection()
        {
            return _direction;
        }
    }
}
