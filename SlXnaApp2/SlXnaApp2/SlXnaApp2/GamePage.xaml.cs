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
        float trajectory;
        Ninja ninja;
        List<Wall> walls;
        List<Wall> walls2;
        float count;
        float count2;
        int score, layoutCount;
        Vector2 downward;
        Random random;

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
            walls = LoadPiece(1);
            count = 0;
            walls2 = new List<Wall>();
            walls2 = LoadPiece(1);
            walls2 = setAbove(walls2);
            count2 = -800;
            downward = new Vector2(0, 1);
            random = new Random();
            layoutCount = 4;
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
            UpdateGame();
            UpdateCamera();
            UpdateConditions();
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
            spriteBatch.End();
        }

        private void DrawGame(SpriteBatch spriteBatch)
        {
            Texture2D tempTexture;

            tempTexture = contentManager.Load<Texture2D>("Savannah");
            spriteBatch.Draw(tempTexture, Vector2.Zero, Color.White);
            /*
            tempTexture = contentManager.Load<Texture2D>("wall_left");
            spriteBatch.Draw(tempTexture, Vector2.Zero, Color.White);
            tempTexture = contentManager.Load<Texture2D>("wall_right");
            spriteBatch.Draw(tempTexture, new Vector2(0, 462), Color.White);
             * */

            if (ninja.wallGrab)
                tempTexture = contentManager.Load<Texture2D>("WallGrab");
            else
                tempTexture = contentManager.Load<Texture2D>("Curl");
            ninja.angle %= 2 * MathHelper.Pi;
            if (ninja.angle > MathHelper.Pi / 2 && ninja.angle < MathHelper.Pi * 3 / 2 && ninja.wallGrab)
                spriteBatch.Draw(tempTexture, ninja.pos, null, Color.White, ninja.angle, ninja.center, Vector2.One, SpriteEffects.FlipVertically, 0f);
            else
                spriteBatch.Draw(tempTexture, ninja.pos, null, Color.White, ninja.angle, ninja.center, Vector2.One, SpriteEffects.None, 0f);

            tempTexture = contentManager.Load<Texture2D>("wall");
            foreach(Wall wall in walls)
            {
                spriteBatch.Draw(tempTexture, wall.pos, null, Color.White, (float)Math.Atan2(wall.p2.Y-wall.pos.Y, wall.p2.X-wall.pos.X), new Vector2(0, 2), new Vector2(Vector2.Distance(wall.pos, wall.p2)/tempTexture.Width, 1), SpriteEffects.None, 0f);
            }
            foreach (Wall wall in walls2)
            {
                spriteBatch.Draw(tempTexture, wall.pos, null, Color.White, (float)Math.Atan2(wall.p2.Y - wall.pos.Y, wall.p2.X - wall.pos.X), new Vector2(0, 2), new Vector2(Vector2.Distance(wall.pos, wall.p2) / tempTexture.Width, 1), SpriteEffects.None, 0f);
            }

            SpriteFont font = contentManager.Load<SpriteFont>("Font1");
            spriteBatch.DrawString(font, score.ToString(), new Vector2(0, 780), Color.Black);
        }

        private void UpdateGame()
        {
            ninja.accel = Vector2.Zero;
            CollisionCheck();
        }

        private void CollisionCheck()
        {
            if (ninja.wallGrab)
            {
                trajectory = input.swipeDirection(trajectory);
                if ((trajectory < ninja.angle + (MathHelper.Pi / 2) && trajectory > ninja.angle - (MathHelper.Pi / 2)) ||
                    (ninja.angle < MathHelper.Pi / 2 && trajectory > ninja.angle + (MathHelper.Pi * 3 / 2)) ||
                    (ninja.angle > MathHelper.Pi * 3 / 2 && trajectory < ninja.angle - (MathHelper.Pi * 3 / 2)))
                {
                }
                else
                {
                    ninja.wallGrab = false;
                    if (Math.Cos(trajectory) != 0)
                        ninja.vel = Vector2.Multiply(new Vector2((float)Math.Cos(trajectory), (float)Math.Sin(trajectory)), 12);
                    else if (trajectory == MathHelper.Pi / 2)
                        ninja.vel = new Vector2(0, -12);
                    else
                        ninja.vel = new Vector2(0, 12);
                }
            }
            else
            {
                ninja.angle += 0.5f;
                ninja.angle %= MathHelper.Pi * 2;
                ninja.accel += new Vector2(0, 0.25f);
                ninja.vel += ninja.accel;
                ninja.pos += ninja.vel;
                foreach (Wall wall in walls)
                {
                    float t = Vector2.Dot(ninja.pos - wall.pos, wall.p2 - wall.pos)/(float)Math.Pow(Vector2.Distance(wall.p2, wall.pos), 2);
                    if (t > 0 && t < 1)
                    {
                        Vector2 projection = Vector2.Multiply(wall.p2 - wall.pos, t)+wall.pos;
                        if (Vector2.Distance(projection, ninja.pos) < 14)
                        {
                            ninja.wallGrab = true;
                            ninja.pos = projection+Vector2.Multiply(ninja.pos-projection, 12/Vector2.Distance(ninja.pos, projection));
                            ninja.angle = (float)Math.Atan2(projection.Y - ninja.pos.Y, projection.X - ninja.pos.X);
                            trajectory = ninja.angle;
                            ninja.vel = Vector2.Zero;
                            ninja.accel = Vector2.Zero;
                            break;
                        }
                    }
                }
                foreach (Wall wall in walls2)
                {
                    float t = Vector2.Dot(ninja.pos - wall.pos, wall.p2 - wall.pos) / (float)Math.Pow(Vector2.Distance(wall.p2, wall.pos), 2);
                    if (t > 0 && t < 1)
                    {
                        Vector2 projection = Vector2.Multiply(wall.p2 - wall.pos, t) + wall.pos;
                        if (Vector2.Distance(projection, ninja.pos) < 14)
                        {
                            ninja.wallGrab = true;
                            ninja.pos = projection + Vector2.Multiply(ninja.pos - projection, 12 / Vector2.Distance(ninja.pos, projection));
                            ninja.angle = (float)Math.Atan2(projection.Y - ninja.pos.Y, projection.X - ninja.pos.X);
                            trajectory = ninja.angle;
                            ninja.vel = Vector2.Zero;
                            ninja.accel = Vector2.Zero;
                            break;
                        }
                    }
                }
            }
        }

        private void UpdateCamera()
        {
            downward += new Vector2(0, 0.0005f);
            ninja.pos += downward;
            foreach (Wall wall in walls)
            {
                wall.pos += downward;
                wall.p2 += downward;
            }
            count += downward.Y;
            foreach (Wall wall in walls2)
            {
                wall.pos += downward;
                wall.p2 += downward;
            }
            count2 += downward.Y;

            score += 1;

            if (count > 800)
            {
                walls = LoadPiece(random.Next(1, layoutCount));
                walls = setAbove(walls);
                count = -800;
            }
            if (count2 > 800)
            {
                walls2 = LoadPiece(random.Next(1, layoutCount));
                walls2 = setAbove(walls2);
                count2 = -800; 
            }
        }

        private void UpdateConditions()
        {
            if (ninja.pos.X < -12 || ninja.pos.X > 492 || ninja.pos.Y > 812)
            {
                MainPage.Highscore = score;
                score = 0;
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        public List<Wall> LoadPiece(int mapID)
        {
            List<Wall> wallsA = new List<Wall>();

            switch(mapID)
            {
                case (1):
                    wallsA.Add(new Wall(new Vector2(240, 0), new Vector2(480, 0)));
                    wallsA.Add(new Wall(new Vector2(0, 400), new Vector2(240, 400)));
                    wallsA.Add(new Wall(new Vector2(14, 0), new Vector2(14, 800)));
                    wallsA.Add(new Wall(new Vector2(466, 800), new Vector2(466, 0)));
                    break;
                case (2):
                    wallsA.Add(new Wall(new Vector2(240, 0), new Vector2(480, 0)));
                    wallsA.Add(new Wall(new Vector2(480, 0), new Vector2(480, 60)));
                    wallsA.Add(new Wall(new Vector2(0, 400), new Vector2(240, 400)));
                    wallsA.Add(new Wall(new Vector2(240, 400), new Vector2(240, 340)));
                    wallsA.Add(new Wall(new Vector2(2, 0), new Vector2(2, 800)));
                    wallsA.Add(new Wall(new Vector2(478, 800), new Vector2(478, 0)));
                    break;
                case (3):
                    wallsA.Add(new Wall(new Vector2(360, 0), new Vector2(360, 800)));
                    wallsA.Add(new Wall(new Vector2(120, 0), new Vector2(120, 800)));
                    wallsA.Add(new Wall(new Vector2(2, 300), new Vector2(2, 500)));
                    wallsA.Add(new Wall(new Vector2(478, 300), new Vector2(478, 500)));
                    break;
            }
            return wallsA;


            /*
            fileManager = new FileManager();

            attributes = new List<List<string>>();
            contents = new List<List<string>>();

            fileManager.LoadContent("Load/Maps/" + mapID + ".cme", attributes, contents, "Walls");

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "StartWall":
                            for (int k = 0; k < contents[i].Count; k++)
                            {
                                string[] split = contents[i][k].Split(',');
                                wallsA.Add(new Wall(new Vector2(float.Parse(split[0]), float.Parse(split[1])),
                                    new Vector2(float.Parse(split[2]), float.Parse(split[3]))));
                            }
                            break;
                        case "EndEntity":
                            break;
                    }
                }
            }
             * */
        }

        private List<Wall> setAbove(List<Wall> wallsA)
        {
            Vector2 above = new Vector2(0, -800);
            foreach (Wall wall in wallsA)
            {
                wall.pos += above;
                wall.p2 += above;
            }
            return wallsA;
        }
    }
}