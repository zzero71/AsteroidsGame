using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 distance;
        Player player;
        List<Bullets> bullets = new List<Bullets>();
        private KeyboardState pastKey;
        public int score;
        private SpriteFont fontScore;
        private ScoreManager scoreManager;

        List<Asteroids> astroids = new List<Asteroids>();
        Random rand = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.Load(Content);

            scoreManager = ScoreManager.Load();

            fontScore = Content.Load<SpriteFont>("Score");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        float spawn = 0;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            LoadAsteroids();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                Shoot();
            }
            player.Update(gameTime);
            UpdateBullets();
            
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;


            foreach (Asteroids astroid in astroids)
            {
                astroid.Update(graphics.GraphicsDevice);
            }
            foreach(Bullets bullet in bullets)
            {
                bullet.Update(graphics.GraphicsDevice);
            }


            foreach(Bullets bullet in bullets)
            {
                foreach(Asteroids astroid in astroids)
                {
                    if(bullet.hitBox.Intersects(astroid.hitBox))
                    {
                        astroid.isVisible = false;
                        bullet.isVisible = false;
                        score += 100;
                    }
                }
            }

            foreach (Asteroids asteroid in astroids)
            {
                if (player.hitBox.Intersects(asteroid.hitBox))
                {
                    
                    scoreManager.Add(new Models.Score()
                    {
                        PlayerName = "Charles",
                        Value = score,
                    });

                    ScoreManager.Save(scoreManager);

                    score = 0;
                }
            }

            


        // TODO: Add your update logic here

        pastKey = Keyboard.GetState();
            base.Update(gameTime);
        }

        public void LoadAsteroids()
        {
            int randY = rand.Next(0, 980);
            int randX = rand.Next(0, 1820);

            if (spawn >= 0)
            {
                spawn = 0;
                if (astroids.Count() < 25)
                {
                    astroids.Add(new Asteroids(Content.Load<Texture2D>("astroid3"), new Vector2(700, randY)));
                }




                for (int i = 0; i < astroids.Count; i++)
                {
                    if (!astroids[i].isVisible)
                    {
                        astroids.RemoveAt(i);
                        i--;

                    }
                }
            }
        }

        public void UpdateBullets()
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.position += bullet.velocity;

                if (Vector2.Distance(bullet.position, player.position) > 1000)
                    bullet.isVisible = false;
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].isVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shoot()
        {
            Bullets newBullet = new Bullets(Content.Load<Texture2D>("Bullet"));
            newBullet.velocity = new Vector2((float)Math.Cos(player.rotation), (float)Math.Sin(player.rotation)) * 10f;
            newBullet.position = player.position + newBullet.velocity * 5f;
            
            newBullet.isVisible = true;

            if (bullets.Count() < 3)
                bullets.Add(newBullet);
        }




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

           

            foreach (Asteroids astroid in astroids)
            {
                astroid.Draw(spriteBatch);
            }

            foreach (Bullets bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            spriteBatch.DrawString(fontScore, "Score " + score, new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(fontScore, "Highscores:\n" + string.Join("\n", scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(550, 10), Color.Red);


            player.Draw(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}