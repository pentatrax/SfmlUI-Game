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
        public enum Input { Left, Right, NONE, ESC }
        private RenderWindow _window;

        // Events
        //public event EventHandler Up;
        //public event EventHandler Down;
        //public event EventHandler Left;
        //public event EventHandler Right;
        public event EventHandler MouseMove;

        // Variables
        private Type _controllType;
        private Input _input;
        //private bool _left = false;
        //private bool _right = false;

        public Controlls(RenderWindow window, Type controllType)
        {
            _window = window;
            _controllType = controllType;
            _input = Input.NONE;
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
            switch (e.Code)
            {
                case Keyboard.Key.A: _input = Input.Left; break;
                case Keyboard.Key.D: _input = Input.Right; break;
                case Keyboard.Key.Escape: _input = Input.ESC; break;
            }
        }
        private void OnWASDReleaseEvent(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.A: _input = Input.NONE; break;
                case Keyboard.Key.D: _input = Input.NONE; break;
                case Keyboard.Key.Escape: _input = Input.NONE; break;
            }
        }

        // Arrow keys events
        private void OnArrowsPressEvent(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Left: _input = Input.Left; break;
                case Keyboard.Key.Right: _input = Input.Right; break;
                case Keyboard.Key.Escape: _input = Input.ESC; break;
            }
        }
        private void OnArrowsReleaseEvent(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Left: _input = Input.NONE; break;
                case Keyboard.Key.Right: _input = Input.NONE; break;
                case Keyboard.Key.Escape: _input = Input.NONE; break;
            }
        }

        // Mouse events
        private void OnMouseMoveEvent(object sender, MouseMoveEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        // Methods

        public Input GetInput()
        {
            return _input;
        }
    }
}
