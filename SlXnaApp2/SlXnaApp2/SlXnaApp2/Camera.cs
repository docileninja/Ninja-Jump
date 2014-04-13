using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SlXnaApp2
{
    public class Camera
    {
        public Vector2 pos;
        public float scale;

        public Camera(Vector2 pos)
        {
            this.pos = pos;
            this.scale = 1f;
        }
    }
}
