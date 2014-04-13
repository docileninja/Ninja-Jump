using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameName3
{
    public class ScreenManager
    {
        ContentManager content;
        Screen newScreen;
        Screen currentScreen;
        public Vector2 dimensions;

        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen = new TitleScreen();
            currentScreen.LoadContent(Content);
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        public void AddScreen(Screen screen)
        {
            newScreen = screen;
            currentScreen.UnloadContent();
            currentScreen = newScreen;
            currentScreen.LoadContent(content);
        }
    }
}
