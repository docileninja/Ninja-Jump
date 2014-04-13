using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameName3
{
    public class Entity
    {
        public Vector2 pos, vel, accel;
        public string textureName;

        public Entity(Vector2 pos, Vector2 vel, Vector2 accel)
        {
            this.pos = pos;
            this.vel = vel;
            this.accel = accel;
        }

        public Entity(Vector2 pos)
        {
            this.pos = pos;
            this.vel = new Vector2();
            this.accel = new Vector2();
        }

        
    }
}
