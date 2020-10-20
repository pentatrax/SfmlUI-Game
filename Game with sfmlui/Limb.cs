using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class Limb
    {
        enum Type { Head, Leg, Arm }
        private Vector2f _link;
        private Vector2f _endPoint;
        private Type _type;
        private Sprite _sprite;

    }
}
