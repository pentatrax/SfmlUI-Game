using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class Player
    {
        // Global values
        private RenderWindow _window;
        private Font _font;

        // Positional values
        private Vector2f _position;
        private Vector2f _velocity;
        private Vector2f _acceleration;

        // Player parts
        private Text name;
        private Limb _head;
        private Limb _leftArm;
        private Limb _rightArm;
        private Limb _leftLeg;
        private Limb _rightLeg;


    }
}
