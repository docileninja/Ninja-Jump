using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SlXnaApp2
{
    public partial class GamePage : PhoneApplicationPage
    {
        ContentManager contentManager;
        GameTimer timer;
        SpriteBatch spriteBatch;
        InputManager input;
        float angle;
        Ninja ninja;
        List<Wall> walls;

        public GamePage()
        {
            InitializeComponent();

            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.UpdateInterval = TimeSpan.FromTicks(333333);
            timer.Update += OnUpdate;
            timer.Draw += OnDraw;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);

            // TODO: use this.content to load your game content here
            input = new InputManager();
            ninja = new Ninja(new Vector2(100, 100), Vector2.Zero, new Vector2(20, 20), 0f);
            walls = new List<Wall>();
            walls.Add(new Wall(new Vector2(100, 100), new Vector2(300, 200)));
            // Start the timer
            timer.Start();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Allows the page to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            // TODO: Add your update logic here
            angle = input.swipeDirection();
        }

        /// <summary>
        /// Allows the page to draw itself.
        /// </summary>
        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.Navy);

            Texture2D testTexture;
            testTexture = contentManager.Load<Texture2D>("WallGrab");
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawGame(spriteBatch);
            //spriteBatch.Draw(testTexture, new Vector2(100, 100), null, Color.White, angle, new Vector2(20, 20), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        private void DrawGame(SpriteBatch spriteBatch)
        {
            Texture2D tempTexture;

            tempTexture = contentManager.Load<Texture2D>("Savannah");
            spriteBatch.Draw(tempTexture, Vector2.Zero, Color.White);

            if(ninja.wallGrab)
                tempTexture = contentManager.Load<Texture2D>("WallGrab");
            else
                tempTexture = contentManager.Load<Texture2D>("Curl");
            spriteBatch.Draw(tempTexture, ninja.pos, null, Color.White, ninja.angle, ninja.center, Vector2.One, SpriteEffects.None, 0f);

            tempTexture = contentManager.Load<Texture2D>("WhiteWall");
            foreach(Wall wall in walls)
            {
                spriteBatch.Draw(tempTexture, wall.pos, null, Color.White, (float)Math.Atan2(wall.p2.Y-wall.pos.Y, wall.p2.X-wall.pos.X), Vector2.Zero, new Vector2(Vector2.Distance(wall.pos, wall.p2)/40, 1), SpriteEffects.None, 0f);
            }
        }
    }
}