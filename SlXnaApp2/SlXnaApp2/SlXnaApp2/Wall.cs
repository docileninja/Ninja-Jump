using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SlXnaApp2
{
    public class Wall : Entity
    {
        public Vector2 p2;

        public Wall(Vector2 pos, Vector2 p2)
            : base(pos)
        {
            this.p2 = p2;
        }
    }
}
