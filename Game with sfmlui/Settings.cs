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
        private RenderWindow _window;
        private Text _title;
        private Dropdown _resolutionPicker;
        private Text _back;
        private Text _apply;
        private Checkbox _fullscreen;
        public event EventHandler StateShiftToMenu;

        public Settings(RenderWindow window, Font font)
        {
            _window = window;
            _window.MouseMoved += OnMouseMove;

            List<string> resolutions = new List<string>();
            foreach (VideoMode videoMode in VideoMode.FullscreenModes)
            {
                resolutions.Add(videoMode.Width.ToString() + " x " + videoMode.Height.ToString());
            }
            _resolutionPicker = new Dropdown(_window, new Vector2f(window.Size.X * 0.1f, window.Size.Y * 0.1f), font, 60, "");
            _resolutionPicker.BackgroundColor = new Color(50, 50, 50, 122);
            _resolutionPicker.OutlineColor = Color.Red;
            _resolutionPicker.TextColor = Color.White;
            _resolutionPicker.HighlightColor = Color.Red;
            foreach (string res in resolutions)
            {
                _resolutionPicker.AddItem(res);
            }
            _resolutionPicker.RemoveItem("");

            _title = new Text("SETTINGS", font, 100);
            _title.Position = new Vector2f(window.Size.X * 0.5f, 10) - new Vector2f(_title.GetGlobalBounds().Width * 0.5f, 0);

            _apply = new Text("Apply", font, 100);
            _apply.Position = new Vector2f(window.Size.X * 0.5f - _apply.GetGlobalBounds().Width * 0.5f, window.Size.Y * 0.75f);

            _back = new Text("Back", font, 100);
            _back.Position = new Vector2f(window.Size.X * 0.5f - _back.GetGlobalBounds().Width * 0.5f, _apply.GetGlobalBounds().Top) + new Vector2f(0, _apply.GetGlobalBounds().Height + 5);
        }

        public void Draw()
        {
            _window.Draw(_title);
            _window.Draw(_apply);
            _window.Draw(_back);
            _resolutionPicker.Draw();
        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            if (IsInside(e, _apply)){
                _apply.FillColor = Color.Red;
                _back.FillColor = Color.White;
            } else if (IsInside(e, _back))
            {
                _back.FillColor = Color.Red;
                _apply.FillColor = Color.White;
            } else
            {
                _back.FillColor = Color.White;
                _apply.FillColor = Color.White;
            }
        }

        private bool IsInside(MouseMoveEventArgs e, Text box)
        {
            return ((e.X >= box.GetGlobalBounds().Left && e.X <= box.GetGlobalBounds().Left + box.GetGlobalBounds().Width) &&
                (e.Y >= box.GetGlobalBounds().Top && e.Y <= box.GetGlobalBounds().Top + box.GetGlobalBounds().Height));
        }
    }
}
