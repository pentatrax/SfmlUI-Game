﻿using System;
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
        private Vector2f _unit;
        private RenderWindow _window;
        private Text _title;
        private Dropdown _resolutionPicker;
        private Text _resolution;
        private Text _back;
        private Text _apply;
        private Text _fullscreen;
        private Checkbox _fullscreenCheckbox;
        public event EventHandler StateShiftToMenu;
        public event EventHandler<WindowArgs> ApplyMenuSettings;

        public Settings(RenderWindow window, Font font)
        {
            // Necesary info/values
            _window = window;
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
            _resolutionPicker.BackgroundColor = new Color(50, 50, 50, 122);
            _resolutionPicker.OutlineColor = Color.Red;
            _resolutionPicker.TextColor = Color.White;
            _resolutionPicker.HighlightColor = Color.Red;
            foreach (string res in resolutions)
            {
                _resolutionPicker.AddItem(res);
            }
            _resolutionPicker.RemoveItem("");
            _resolutionPicker.Position = new Vector2f(_resolution.GetGlobalBounds().Left + _resolution.GetGlobalBounds().Width * 0.5f - _resolutionPicker.Width * 0.5f, _resolution.Position.Y + _resolution.GetGlobalBounds().Height * 3.5f);

            // Fullscreen text
            _fullscreen = new Text("Fullscreen ", font, 3 * (uint)_unit.X);
            _fullscreen.Position = new Vector2f(_window.Size.X - _window.Size.X * 0.15f - _fullscreen.GetGlobalBounds().Width * 0.5f, _title.GetGlobalBounds().Top + _title.GetGlobalBounds().Height);


            // Fullscreen checkbox
            _fullscreenCheckbox = new Checkbox(window, new Vector2f(0,0));
            _fullscreenCheckbox.Width = _fullscreen.GetGlobalBounds().Height;
            _fullscreenCheckbox.Height = _fullscreen.GetGlobalBounds().Height;
            _fullscreenCheckbox.Position = new Vector2f(_window.Size.X - _window.Size.X * 0.15f - _fullscreen.GetGlobalBounds().Width * 0.5f, _fullscreen.GetGlobalBounds().Top + _fullscreen.GetGlobalBounds().Height);

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
                ApplyMenuSettings?.Invoke(this, new WindowArgs(_resolutionPicker.ChosenItem, false));
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
