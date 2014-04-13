using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameName3
{
    public class Ninja : Entity
    {
        Vector2 center;
        float rotation;
        bool wallGrab;

        public Ninja(Vector2 pos, Vector2 vel, Vector2 accel, Vector2 center)
            : base(pos, vel, accel)
        {
            this.center = center;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, pos - center, null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }
    }
}
