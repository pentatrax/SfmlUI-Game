using System;
using SfmlUI;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Game_with_sfmlui
{
    class Program
    {
        public enum State {Menu, Settings, Play, Pause, GameOver}
        static void Main(string[] args)
        {
            const string TITLE = "SfmlUI Game";
            WindowArgs GlobalWindowState = new WindowArgs("1920 x 1080", true);
            RenderWindow Window = new RenderWindow(VideoMode.FullscreenModes[0], TITLE, Styles.Fullscreen);
            Window.Closed += CloseGame;

            Font GlobalFont = new Font("8-bit Arcade In.ttf");

            State _state = State.Menu;

            Background background = new Background(Window, new Image("rsrc/background-1.png"));

            Menu menu = new Menu(Window, GlobalFont);
            menu.StateShiftToPlay += StateToPlay;
            menu.StateShiftToSettings += StateToSettings;
            menu.QuitGame += CloseGame;

            Settings settings = new Settings(Window, GlobalFont, GlobalWindowState);
            settings.StateShiftToMenu += StateToMenu;
            settings.ApplyMenuSettings += ApplySettings;

            // the main runtime loop
            while (Window.IsOpen)
            {
                Window.Clear();
                Window.DispatchEvents();
                switch (_state)
                {
                    case State.Menu: background.Draw(); menu.Draw(); break;
                    case State.Settings: background.Draw(); settings.Draw(); break;
                    case State.Play: break;
                    case State.Pause: break;
                    case State.GameOver: break;
                }
                Window.Display();
                
            }

            void ApplySettings(object sender, WindowArgs e)
            {
                VideoMode res = VideoMode.FullscreenModes[0];
                Styles style;
                foreach (VideoMode videoMode in VideoMode.FullscreenModes)
                {
                    if (videoMode.Width.ToString() + " x " + videoMode.Height.ToString() == e.Resolution)
                    {
                        res = videoMode;
                    }
                }
                if (e.Fullscreen)
                {
                    style = Styles.Fullscreen;
                }
                else
                {
                    style = Styles.Close | Styles.Titlebar;
                }
                Console.WriteLine("Changing Resolution to: " + e.Resolution);
                Window.Close();
                Window = new RenderWindow(res, TITLE, style);
                Window.Closed += CloseGame;
                GlobalWindowState = e;

                background = new Background(Window, new Image("rsrc/background-1.png"));

                menu = new Menu(Window, GlobalFont);
                menu.StateShiftToPlay += StateToPlay;
                menu.StateShiftToSettings += StateToSettings;
                menu.QuitGame += CloseGame;

                settings = new Settings(Window, GlobalFont, GlobalWindowState);
                settings.StateShiftToMenu += StateToMenu;
                settings.ApplyMenuSettings += ApplySettings;
            }

            // State shift commands
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
