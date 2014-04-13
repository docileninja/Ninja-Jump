using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SlXnaApp2
{
    public class Ninja : Entity
    {
        public Vector2 center, vel, accel;
        public float angle;
        public bool wallGrab;

        public Ninja(Vector2 pos, Vector2 vel, Vector2 center, float angle)
            : base(pos)
        {
            this.vel = vel;
            this.center = center;
            this.angle = angle;
            this.wallGrab = false;
        }
    }
}
