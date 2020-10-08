using System;
using SfmlUI;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Game_with_sfmlui
{
    class Program
    {
        public enum State {Menu, Settings, Play, Pause, GameOver}
        static void Main(string[] args)
        {
            RenderWindow Window = new RenderWindow(VideoMode.FullscreenModes[0], "SfmlUI Game", Styles.Fullscreen);
            Window.Closed += CloseGame;
            Font GlobalFont = new Font("8-bit Arcade In.ttf");

            State _state = State.Menu;

            Menu menu = new Menu(Window, GlobalFont);
            menu.StateShiftToPlay += StateToPlay;
            menu.StateShiftToSettings += StateToSettings;

            Settings settings = new Settings(Window, GlobalFont);
            settings.StateShiftToMenu += StateToMenu;

            while (Window.IsOpen)
            {
                Window.Clear();
                Window.DispatchEvents();
                switch (_state)
                {
                    case State.Menu: menu.Draw(); break;
                    case State.Settings: settings.Draw(); break;
                    case State.Play: break;
                    case State.Pause: break;
                    case State.GameOver: break;
                }
                Window.Display();
                
            }

            void CloseGame(object sender, EventArgs e)
            {
                Window.Close();
            }

            void StateToMenu(object sender, EventArgs e)
            {
                _state = State.Menu;
                Console.WriteLine(_state);
            }
            void StateToPlay(object sender, EventArgs e)
            {
                _state = State.Play;
                Console.WriteLine(_state);
            }
            void StateToSettings(object sender, EventArgs e)
            {
                _state = State.Settings;
                Console.WriteLine(_state);
            }
            void StateToPause(object sender, EventArgs e)
            {
                _state = State.Pause;
            }
            void StateToGameOver(object sender, EventArgs e)
            {
                _state = State.GameOver;
            }
        }
    }
}
