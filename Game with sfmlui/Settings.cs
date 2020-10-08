using System;
using SfmlUI;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game_with_sfmlui
{
    class Settings
    {
        private RenderWindow _window;
        private Dropdown _resolutionPicker;
        private Button _back;
        private Checkbox _fullscreen;
        public event EventHandler StateShiftToMenu;

        public Settings(RenderWindow window, Font font)
        {
            _window = window;
            
            _resolutionPicker = new Dropdown(_window, new Vector2f(window.Size.X * 0.33f, window.Size.Y * 0.33f), font, 20, "");
            


        }

        public void Draw()
        {

        }
    }
}
