using System;
using SfmlUI;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Game_with_sfmlui
{
    class Program
    {
        
        public enum State {Menu, Settings, Play, Pause, GameOver}
        static void Main(string[] args)
        {
            const string TITLE = "Breakout";
            string LocalDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString();
            WindowArgs GlobalWindowState;
            if (File.Exists(LocalDataFolder + "/GTG/saved.resolution"))
            {
                Console.WriteLine("Found settings file...");
                string[] fileContent = File.ReadAllText(LocalDataFolder + "/GTG/saved.resolution").Split(",");
                bool tempBoolHolder;
                if (fileContent[1] == "False")
                {
                    tempBoolHolder = false;
                } else
                {
                    tempBoolHolder = true;
                }
                Controlls.Type tempInputHolder;
                switch (fileContent[2])
                {
                    case "WASD": tempInputHolder = Controlls.Type.WASD; break;
                    case "Arrows": tempInputHolder = Controlls.Type.Arrows; break;
                    case "Mouse": tempInputHolder = Controlls.Type.Mouse; break;
                    default: tempInputHolder = Controlls.Type.WASD; break;
                }
                Console.WriteLine("Found saved input type: " + fileContent[2]);
                GlobalWindowState = new WindowArgs(fileContent[0], tempBoolHolder, tempInputHolder);
            }
            else
            {
                GlobalWindowState = new WindowArgs("1920 x 1080", true, Controlls.Type.WASD);
                Console.WriteLine("No saved settings found, picking available resolution...");
                Console.WriteLine("No saved settings found, window will go fullscreen by default...");
                Console.WriteLine("No saved settings found, choosing default input type...");
            }
            RenderWindow Window = new RenderWindow(GetVideoMode(GlobalWindowState.Resolution), TITLE, GetScreenStyle(GlobalWindowState.Fullscreen));
            Window.Closed += CloseGame;

            Font GlobalFont = new Font("8-bit Arcade In.ttf");

            State state = State.Menu;

            Background background = new Background(Window, new Image("rsrc/background-1.png"));

            Menu menu = new Menu(Window, GlobalFont, TITLE);
            menu.StateShiftToPlay += StateToPlay;
            menu.StateShiftToSettings += StateToSettings;
            menu.QuitGame += CloseGame;

            Settings settings = new Settings(Window, GlobalFont, GlobalWindowState);
            settings.StateShiftToMenu += StateToMenu;
            settings.ApplyMenuSettings += ApplySettings;

            Game game = new Game(Window, GlobalFont, GlobalWindowState.InputType);
            game.BackToMenu += StateToMenu;
            game.PauseGame += StateToPause;
            game.GameOver += StateToGameOver;

            DateTime prevTime = DateTime.Now;
            TimeSpan deltaT;

            // the main runtime loop
            while (Window.IsOpen)
            {
                Window.Clear();
                Window.DispatchEvents();
                deltaT = DateTime.Now - prevTime;
                prevTime += deltaT;
                switch (state)
                {
                    case State.Menu: background.Draw(); menu.Draw(); break;
                    case State.Settings: background.Draw(); settings.Draw(); break;
                    case State.Play: background.Draw(); game.Update(deltaT); game.Draw(); break;
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
                if (Directory.Exists(LocalDataFolder + "/GTG"))
                {
                    File.WriteAllText(LocalDataFolder + "/GTG/saved.resolution", e.Resolution + "," + e.Fullscreen.ToString() + "," + e.InputType.ToString());
                } else
                {
                    Directory.CreateDirectory(LocalDataFolder + "/GTG");
                    File.WriteAllText(LocalDataFolder + "/GTG/saved.resolution", e.Resolution + "," + e.Fullscreen.ToString() + "," + e.InputType.ToString());
                }

                background = new Background(Window, new Image("rsrc/background-1.png"));

                menu = new Menu(Window, GlobalFont, TITLE);
                menu.StateShiftToPlay += StateToPlay;
                menu.StateShiftToSettings += StateToSettings;
                menu.QuitGame += CloseGame;

                settings = new Settings(Window, GlobalFont, GlobalWindowState);
                settings.StateShiftToMenu += StateToMenu;
                settings.ApplyMenuSettings += ApplySettings;

                game = new Game(Window, GlobalFont, e.InputType);
                game.BackToMenu += StateToMenu;
                game.PauseGame += StateToPause;
                game.GameOver += StateToGameOver;
            }

            // State shift commands
            void CloseGame(object sender, EventArgs e)
            {
                Window.Close();
            }

            void StateToMenu(object sender, EventArgs e)
            {
                state = State.Menu;
                Console.WriteLine(state);
            }
            void StateToPlay(object sender, EventArgs e)
            {
                state = State.Play;
                Console.WriteLine(state);
            }
            void StateToSettings(object sender, EventArgs e)
            {
                state = State.Settings;
                Console.WriteLine(state);
            }
            void StateToPause(object sender, EventArgs e)
            {
                state = State.Pause;
            }
            void StateToGameOver(object sender, EventArgs e)
            {
                state = State.GameOver;
            }

            // Method to pick a window style
            Styles GetScreenStyle(bool e)
            {
                if (e) 
                {
                    return Styles.Fullscreen;
                } else 
                {
                    return Styles.Close | Styles.Titlebar;
                }
            }

            // Method to get chosen window mode the
            VideoMode GetVideoMode(string res)
            {
                foreach (VideoMode resolution in VideoMode.FullscreenModes)
                {
                    if (resolution.Width.ToString() + " x " + resolution.Height.ToString() == res)
                    {
                        Console.WriteLine("Found possible resolution size: " + res);
                        return resolution;
                    }
                }
                Console.WriteLine("Could not get screen resolutions, getting some anyway.");
                return new VideoMode(920, 680);
            }
        }
    }
}
