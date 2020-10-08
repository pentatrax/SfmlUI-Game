using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class WindowArgs
    {
        public string Resolution;
        public bool Fullscreen;

        public WindowArgs(string res, bool fullscreen)
        {
            Resolution = res;
            Fullscreen = fullscreen;
        }
    }
}
