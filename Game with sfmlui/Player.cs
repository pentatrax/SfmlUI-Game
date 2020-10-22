using SFML.Graphics;
using SFML.System;
using SfmlUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game_with_sfmlui
{
    class Player
    {
        // Global values
        private RenderWindow _window;
        //private Font _font;

        // Positional values
        private Vector2f _position;
        private Vector2f _velocity;
        private Vector2f _acceleration;

        // Player parts
        //private List<Bodypart> _bodyParts = new List<Bodypart>();
        //private Text name;
        private Bodypart _torso;

        public Player(RenderWindow window, Vector2f position)
        {
            _torso = new Bodypart(window, position, Bodypart.Type.Torso, new Sprite());
            _torso.Size
        }

        void Draw()
        {
            _torso.Draw();
        }

        List<Bodypart> SortBodyPartsByIndex_Z(List<Bodypart> list)
        {
            List<Bodypart> tempList = new List<Bodypart>();
            int tempCounter = 0;

            for (int i=0; i<list.Count; i++)
            {
                if (list[i].zIndex == tempCounter)
                {
                    tempList.Add(list[i]);
                    list.RemoveAt(i);
                }

                if (list.Count > 0 && i == list.Count - 1)
                {
                    i = 0;
                    tempCounter++;
                }
            }

            return tempList;
        }

    }
}
