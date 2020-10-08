using System;
using SfmlUI;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Game_with_sfmlui
{
    class Settings
    {
        private bool _active = false;
        private RenderWindow _window;
        private Text _title;
        private Dropdown _resolutionPicker;
        private Text _back;
        private Text _apply;
        private Checkbox _fullscreen;
        public event EventHandler StateShiftToMenu;
        public event EventHandler ApplyMenuSettings;

        public Settings(RenderWindow window, Font font)
        {
            _window = window;
            _window.MouseMoved += OnMouseMove;
            _window.MouseButtonPressed += OnMousePress;
            _window.MouseButtonReleased += OnMouseRelease;

            _title = new Text("SETTINGS", font, 100);
            _title.Position = new Vector2f(window.Size.X * 0.5f, 10) - new Vector2f(_title.GetGlobalBounds().Width * 0.5f, 0);

            List<string> resolutions = new List<string>();
            foreach (VideoMode videoMode in VideoMode.FullscreenModes)
            {
                resolutions.Add(videoMode.Width.ToString() + " x " + videoMode.Height.ToString());
            }
            _resolutionPicker = new Dropdown(_window, new Vector2f(window.Size.X * 0.1f, _title.GetGlobalBounds().Top + _title.GetGlobalBounds().Height), font, 60, "");
            _resolutionPicker.BackgroundColor = new Color(50, 50, 50, 122);
            _resolutionPicker.OutlineColor = Color.Red;
            _resolutionPicker.TextColor = Color.White;
            _resolutionPicker.HighlightColor = Color.Red;
            foreach (string res in resolutions)
            {
                _resolutionPicker.AddItem(res);
            }
            _resolutionPicker.RemoveItem("");

            _apply = new Text("Apply", font, 100);
            _apply.Position = new Vector2f(window.Size.X * 0.5f - _apply.GetGlobalBounds().Width * 0.5f, window.Size.Y * 0.75f);

            _back = new Text("Back", font, 100);
            _back.Position = new Vector2f(window.Size.X * 0.5f - _back.GetGlobalBounds().Width * 0.5f, _apply.GetGlobalBounds().Top) + new Vector2f(0, _apply.GetGlobalBounds().Height + 5);
        }

        public void Draw()
        {
            _active = true;
            _window.Draw(_title);
            _window.Draw(_apply);
            _window.Draw(_back);
            _resolutionPicker.Draw();
        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            if (IsInside(new Vector2f(e.X, e.Y), _apply) && _active)
            {
                _apply.FillColor = Color.Red;
                _back.FillColor = Color.White;
            } else if (IsInside(new Vector2f(e.X, e.Y), _back) && _active)
            {
                _back.FillColor = Color.Red;
                _apply.FillColor = Color.White;
            } else
            {
                _back.FillColor = Color.White;
                _apply.FillColor = Color.White;
            }
        }
        private void OnMousePress(object sender, MouseButtonEventArgs e)
        {
            if (IsInside(new Vector2f(e.X, e.Y), _apply) && _active)
            {
                _apply.FillColor = new Color(122, 0, 0, 255);
            }
            else if (IsInside(new Vector2f(e.X, e.Y), _back) && _active)
            {
                _back.FillColor = new Color(122, 0, 0, 255);
            }
        }
        private void OnMouseRelease(object sender, MouseButtonEventArgs e)
        {
            if (IsInside(new Vector2f(e.X, e.Y), _apply) && _active)
            {
                _apply.FillColor = new Color(255, 0, 0, 255);
                ApplyMenuSettings?.Invoke(this, e);
                _active = false;
            }
            else if (IsInside(new Vector2f(e.X, e.Y), _back) && _active)
            {
                _back.FillColor = new Color(255, 0, 0, 255);
                StateShiftToMenu?.Invoke(this, e);
                _active = false;
            }
        }

        private bool IsInside(Vector2f e, Text box)
        {
            return ((e.X >= box.GetGlobalBounds().Left && e.X <= box.GetGlobalBounds().Left + box.GetGlobalBounds().Width) &&
                (e.Y >= box.GetGlobalBounds().Top && e.Y <= box.GetGlobalBounds().Top + box.GetGlobalBounds().Height));
        }
    }
}
