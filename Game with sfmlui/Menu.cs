﻿using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SfmlUI;

namespace Game_with_sfmlui
{
    public class Menu
    {
        private bool _active;
        private Color _pressedColor = new Color(50, 50, 50, 255);
        private Vector2f _unit;
        private RenderWindow _window;
        private Button _playButton;
        private Button _settingsButton;
        private Button _quitButton;
        private Text _playText;
        private Text _settingsText;
        private Text _quitText;
        public event EventHandler StateShiftToPlay;
        public event EventHandler StateShiftToSettings;
        public event EventHandler QuitGame;

        public Menu(RenderWindow window, Font font)
        {
            _window = window;
            _unit = new Vector2f(window.Size.X / 100f, window.Size.Y / 100f);
            
            _playButton = new Button(window, new Vector2f(window.Size.X*0.5f - 10f * _unit.X, window.Size.Y* 0.66f), new Vector2f(20f * _unit.X, 10f * _unit.Y));
            _playButton.OuterColor = Color.Red;
            _playButton.CenterOutlineColor = Color.Red;
            _playButton.ButtonHeld += highlightPlayTextColor;
            _playButton.ButtonReleased += dehighlightPlayTextColor;
            
            _settingsButton = new Button(window, _playButton.Position + new Vector2f(0, _playButton.Size.Y+1 * _unit.Y), new Vector2f(20f * _unit.X, 10f * _unit.Y));
            _settingsButton.OuterColor = Color.Red;
            _settingsButton.CenterOutlineColor = Color.Red;
            _settingsButton.ButtonPressed += highlightSettingsTextColor;
            _settingsButton.ButtonReleased += dehighlightSettingsTextColor;

            _quitButton = new Button(window, _settingsButton.Position + new Vector2f(0, _settingsButton.Size.Y + 1f * _unit.Y), new Vector2f(20f * _unit.X, 10f * _unit.Y));
            _quitButton.OuterColor = Color.Red;
            _quitButton.CenterOutlineColor = Color.Red;
            _quitButton.ButtonPressed += highlightQuitTextColor;
            _quitButton.ButtonReleased += dehighlightQuitTextColor;

            _playText = new Text("PLAY", font, 5 * (uint)_unit.X);
            m_CenterTextInButton(_playButton, _playText);

            _settingsText = new Text("SETTINGS", font, 5 * (uint)_unit.X);
            m_CenterTextInButton(_settingsButton, _settingsText);

            _quitText = new Text("QUIT", font, 5 * (uint)_unit.X);
            m_CenterTextInButton(_quitButton, _quitText);

        }

        public void Draw()
        {
            _active = true;
            _playButton.Draw();
            _window.Draw(_playText);
            _settingsButton.Draw();
            _window.Draw(_settingsText);
            _quitButton.Draw();
            _window.Draw(_quitText);
        }

        private void m_CenterTextInButton(Button button, Text text)
        {
            float x = button.Position.X + button.Size.X * 0.5f - text.GetGlobalBounds().Width * 0.5f;
            float y = button.Position.Y - button.Size.Y * 0.05f - text.GetGlobalBounds().Height * 0.5f;

            text.Position = new Vector2f(x, y);
        }

        private void highlightPlayTextColor()
        {
            if (_active)
            {
                _playText.FillColor = _pressedColor;
            }
        }
        private void highlightSettingsTextColor()
        {
            if (_active)
            {
                _settingsText.FillColor = _pressedColor;
            }
        }
        private void dehighlightPlayTextColor()
        {
            if (_active)
            {
                _playText.FillColor = Color.White;
                _active = false;
                StateShiftToPlay?.Invoke(this, EventArgs.Empty);
            }    
        }
        private void dehighlightSettingsTextColor()
        {
            if (_active)
            {
                _settingsText.FillColor = Color.White;
                _active = false;
                StateShiftToSettings?.Invoke(this, EventArgs.Empty);
            }
        }

        private void highlightQuitTextColor()
        {
            if (_active)
            {
                _quitText.FillColor = _pressedColor;
            }
        }
        private void dehighlightQuitTextColor()
        {
            if (_active)
            {
                _quitText.FillColor = Color.White;
                _active = false;
                QuitGame?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
