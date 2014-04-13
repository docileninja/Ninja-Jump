using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameName3
{
    public class GameScreen : Screen
    {
        ContentManager content;
        Ninja ninja;
        Wall wall;

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            ninja = new Ninja(new Vector2(100, 100), Vector2.Zero, Vector2.Zero, new Vector2(20, 20));
            wall = new Wall(new Vector2(100, 200), new Vector2(200, 200));
            base.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Texture2D ninjaT, wallT;
            ninjaT = content.Load<Texture2D>("Curl");
            wallT = content.Load<Texture2D>("WhiteWall");
            ninja.Draw(spriteBatch, ninjaT);
            wall.Draw(spriteBatch, wallT);
            base.Draw(spriteBatch);
        }
    }
}
