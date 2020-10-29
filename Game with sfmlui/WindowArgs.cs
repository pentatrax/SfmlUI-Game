using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class WindowArgs
    {
        private string _resolution;
        private bool _fullscreen;
        private Controlls.Type _input;

        public string Resolution { get { return _resolution; } }
        public bool Fullscreen { get { return _fullscreen; } }
        public Controlls.Type InputType { get { return _input; } }


        public WindowArgs(string res, bool fullscreen, Controlls.Type input)
        {
            _resolution = res;
            _fullscreen = fullscreen;
            _input = input;
        }
    }
}
