using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Object
    {
        public Rectangle hitBox;
        public bool isVisible = true;
        public Vector2 pos;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Rectangle HitBox
        {
            get { return hitBox; }
            set { hitBox = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
    }
}
