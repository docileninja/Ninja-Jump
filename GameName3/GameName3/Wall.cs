using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameName3
{
    public class Wall : Entity
    {
        Vector2 p2;

        public Wall(Vector2 pos, Vector2 p2)
            : base(pos)
        {
            this.p2 = p2;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, pos, null, Color.White, (float)Math.Atan2(p2.Y - pos.Y, p2.X - pos.X), Vector2.Zero, new Vector2(Vector2.Distance(pos, p2), 1f), SpriteEffects.None, 0f);
        }
    }
}
