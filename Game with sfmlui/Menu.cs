using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SfmlUI;

namespace Game_with_sfmlui
{
    public class Menu
    {
        private bool _active;
        private RenderWindow _window;
        private Button _playButton;
        private Button _settingsButton;
        private Text _playText;
        private Text _settingsText;
        public event EventHandler StateShiftToPlay;
        public event EventHandler StateShiftToSettings;

        public Menu(RenderWindow window, Font font)
        {
            _window = window;
            
            _playButton = new Button(window, new Vector2f(window.Size.X*0.5f-200, window.Size.Y*0.66f), new Vector2f(400, 100));
            _playButton.OuterColor = Color.Red;
            _playButton.CenterOutlineColor = Color.Red;
            _playButton.ButtonHeld += highlightPlayTextColor;
            _playButton.ButtonRealeased += dehighlightPlayTextColor;
            
            _settingsButton = new Button(window, _playButton.Position + new Vector2f(0, _playButton.Size.Y+20), new Vector2f(400, 100));
            _settingsButton.OuterColor = Color.Red;
            _settingsButton.CenterOutlineColor = Color.Red;
            _settingsButton.ButtonPressed += highlightSettingsTextColor;
            _settingsButton.ButtonRealeased += dehighlightSettingsTextColor;
            
            _playText = new Text("PLAY", font, 400 / 4);
            m_CenterTextInButton(_playButton, _playText);

            _settingsText = new Text("SETTINGS", font, 400 / 4);
            m_CenterTextInButton(_settingsButton, _settingsText);

        }

        public void Draw()
        {
            _active = true;
            _playButton.Draw();
            _window.Draw(_playText);
            _settingsButton.Draw();
            _window.Draw(_settingsText);
        }

        private void m_CenterTextInButton(Button button, Text text)
        {
            float x = button.Position.X + button.Size.X * 0.5f - text.GetGlobalBounds().Width * 0.5f;
            float y = button.Position.Y - button.Size.Y * 0.11f - text.GetGlobalBounds().Height * 0.5f;

            text.Position = new Vector2f(x, y);
        }

        private void highlightPlayTextColor()
        {
            if (_active)
            {
                _playText.Color = Color.Blue;
            }
        }
        private void highlightSettingsTextColor()
        {
            if (_active)
            {
                _settingsText.Color = Color.Blue;
            }
        }
        private void dehighlightPlayTextColor()
        {
            if (_active)
            {
                _playText.Color = Color.White;
                StateShiftToPlay?.Invoke(this, EventArgs.Empty);
                _active = false;
            }    
        }
        private void dehighlightSettingsTextColor()
        {
            if (_active)
            {
                _settingsText.Color = Color.White;
                StateShiftToSettings?.Invoke(this, EventArgs.Empty);
                _active = false;
            }
        }
    }
}
