using System;
using SfmlUI;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Game_with_sfmlui
{
    /*
     _____________________
    /OOOOOOOOOOOOOOOOOOOOO\
    |                     |
    |OOOOOOOOOOOOOOOOOOOOO|
    |                     |
    |                     |
    |IIIIIIIIIIIIIIIIIIIII|
    |                     |
    |IIIIIIIIIIIIIIIIIIIII|
    \_____________________/

    */
    class Settings
    {
        private bool _active = false;
        private Vector2f _unit;
        private RenderWindow _window;
        private WindowArgs _windowState;
        private Text _title;
        private Dropdown _resolutionPicker;
        private Text _resolution;
        private Text _back;
        private Text _apply;
        private Text _fullscreen;
        private Checkbox _fullscreenCheckbox;
        private Text _input;
        private RadioButton _inputRadiobutton;
        public event EventHandler StateShiftToMenu;
        public event EventHandler<WindowArgs> ApplyMenuSettings;

        public Settings(RenderWindow window, Font font, WindowArgs windowState)
        {
            // Necesary info/values
            _window = window;
            _windowState = windowState;
            _unit = new Vector2f(window.Size.X / 100f, window.Size.Y / 100f);
            _window.MouseMoved += OnMouseMove;
            _window.MouseButtonPressed += OnMousePress;
            _window.MouseButtonReleased += OnMouseRelease;


            // Settings title
            _title = new Text("SETTINGS", font, 5 * (uint)_unit.X);
            _title.Position = new Vector2f(window.Size.X * 0.5f, 0.5f * _unit.Y) - new Vector2f(_title.GetGlobalBounds().Width * 0.5f, 0);


            // Resolution text
            _resolution = new Text("Resolution", font, 3 * (uint)_unit.X);
            _resolution.Position = new Vector2f(window.Size.X * 0.15f - _resolution.GetGlobalBounds().Width * 0.5f, _title.GetGlobalBounds().Top + _title.GetGlobalBounds().Height);

            // Resolution dropdown
            List<string> resolutions = new List<string>();
            foreach (VideoMode videoMode in VideoMode.FullscreenModes)
            {
                resolutions.Add(videoMode.Width.ToString() + " x " + videoMode.Height.ToString());
            }
            _resolutionPicker = new Dropdown(_window, _resolution.Position, font, 3 * (uint)_unit.X, "");
            _resolutionPicker.TextColor = Color.White;
            _resolutionPicker.BackgroundColor = new Color(50, 50, 50, 122);
            _resolutionPicker.OutlineColor = Color.Red;
            _resolutionPicker.HighlightColor = Color.Red;
            foreach (string res in resolutions)
            {
                _resolutionPicker.AddItem(res);
            }
            _resolutionPicker.RemoveItem("");
            for (int i=0; i<resolutions.Count; i++)
            {
                if (resolutions[i] == _window.Size.X.ToString() + " x " + _window.Size.Y.ToString())
                {
                    string tempResHolder = _resolutionPicker.ChosenItem;
                    _resolutionPicker.RemoveItem(resolutions[i]);
                    _resolutionPicker.ReplaceItem(_resolutionPicker.ChosenItem, resolutions[i]);
                    if (resolutions[i] != tempResHolder)
                    {
                        _resolutionPicker.AddItem(tempResHolder);
                    }
                }
            }
            _resolutionPicker.Position = new Vector2f(_resolution.GetGlobalBounds().Left + _resolution.GetGlobalBounds().Width * 0.5f - _resolutionPicker.Width * 0.5f, _resolution.Position.Y + _resolution.GetGlobalBounds().Height * 3.5f);


            // Fullscreen text
            _fullscreen = new Text("Fullscreen", font, 3 * (uint)_unit.X);
            _fullscreen.Position = new Vector2f(_window.Size.X - _window.Size.X * 0.15f - _fullscreen.GetGlobalBounds().Width * 0.5f, _title.GetGlobalBounds().Top + _title.GetGlobalBounds().Height);

            // Fullscreen checkbox
            _fullscreenCheckbox = new Checkbox(window, new Vector2f(_fullscreen.GetGlobalBounds().Left + _fullscreen.GetGlobalBounds().Width * 0.5f - 1.5f * (int)_unit.X, _fullscreen.GetGlobalBounds().Top + _fullscreen.GetGlobalBounds().Height + 0.25f * (int)_unit.X));
            _fullscreenCheckbox.Width = 3 * (int)_unit.X;
            _fullscreenCheckbox.Height = 3 * (int)_unit.X;
            _fullscreenCheckbox.BorderColor = Color.Red;
            _fullscreenCheckbox.BorderEnabled = true;
            _fullscreenCheckbox.BorderThickness = 2;
            _fullscreenCheckbox.FillColor = new Color(50, 50, 50, 122);
            _fullscreenCheckbox.CrossColor = Color.White;
            if (windowState.Fullscreen)
            {
                _fullscreenCheckbox.IsChecked = true;
            } else
            {
                _fullscreenCheckbox.IsChecked = false;
            }
            Console.WriteLine("Fullscreen: " + _fullscreenCheckbox.IsChecked.ToString());

            // Input text
            _input = new Text("Input", font, 3 * (uint)_unit.X);
            _input.Position = new Vector2f(_window.Size.X * 0.5f - _input.GetGlobalBounds().Width * 0.5f, _title.GetGlobalBounds().Top + _title.GetGlobalBounds().Height);

            // Input radiobutton
            _inputRadiobutton = new RadioButton(window, _input.Position + new Vector2f(0, _input.GetGlobalBounds().Height * 3.5f), 0.5f * _unit.X, new Vector2f(0, 4f * _unit.Y), 3);
            Console.WriteLine("Input type: " + windowState.InputType.ToString());

            // Apply button
            _apply = new Text("Apply", font, 5 * (uint)_unit.X);
            _apply.Position = new Vector2f(window.Size.X * 0.5f - _apply.GetGlobalBounds().Width * 0.5f, window.Size.Y * 0.75f);

            // Back button
            _back = new Text("Back", font, 5 * (uint)_unit.X);
            _back.Position = new Vector2f(window.Size.X * 0.5f - _back.GetGlobalBounds().Width * 0.5f, _apply.GetGlobalBounds().Top) + new Vector2f(0, _apply.GetGlobalBounds().Height + 0.25f * _unit.Y);
        }

        public void Draw() // Draw method
        {
            _active = true;
            _window.Draw(_title);
            _window.Draw(_apply);
            _window.Draw(_back);
            _window.Draw(_resolution);
            _resolutionPicker.Draw();
            _window.Draw(_fullscreen);
            _fullscreenCheckbox.Draw();
            _window.Draw(_input);
            _inputRadiobutton.Draw();

        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e) // Mouse move event
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
        private void OnMousePress(object sender, MouseButtonEventArgs e) // Mouse press event
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
        private void OnMouseRelease(object sender, MouseButtonEventArgs e) // Mouse release event
        {
            if (IsInside(new Vector2f(e.X, e.Y), _apply) && _active)
            {
                _apply.FillColor = new Color(255, 0, 0, 255);
                int tempRadiobuttonOutput = _inputRadiobutton._selected;
                Controlls.Type tempInputTypeHolder;
                switch (tempRadiobuttonOutput)
                {
                    case 0: tempInputTypeHolder = Controlls.Type.WASD; break;
                    case 1: tempInputTypeHolder = Controlls.Type.Arrows; break;
                    case 2: tempInputTypeHolder = Controlls.Type.Mouse; break;
                    default: tempInputTypeHolder = Controlls.Type.WASD; break;
                }
                Console.Clear();
                ApplyMenuSettings?.Invoke(this, new WindowArgs(_resolutionPicker.ChosenItem, _fullscreenCheckbox.IsChecked, tempInputTypeHolder));
                _active = false;
            }
            else if (IsInside(new Vector2f(e.X, e.Y), _back) && _active)
            {
                _back.FillColor = new Color(255, 0, 0, 255);
                StateShiftToMenu?.Invoke(this, EventArgs.Empty);
                _active = false;
            }
        }

        private bool IsInside(Vector2f e, Text box) // Check om e er inden for box
        {
            return ((e.X >= box.GetGlobalBounds().Left && e.X <= box.GetGlobalBounds().Left + box.GetGlobalBounds().Width) &&
                (e.Y >= box.GetGlobalBounds().Top && e.Y <= box.GetGlobalBounds().Top + box.GetGlobalBounds().Height));
        }
    }
}
