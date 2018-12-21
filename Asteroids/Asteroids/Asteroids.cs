using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroids : Object
    {
        public Texture2D tex;
        public Vector2 position;
        public Vector2 velocity;

        List<Asteroids> astroids = new List<Asteroids>();

        Random rand = new Random();
        int randX, randY;
        public bool isVisible = true;

        public Asteroids(Texture2D newTexture, Vector2 newPosition)
        {
            tex = newTexture;
            position = newPosition;

            randY = rand.Next(-2, 1);
            randX = rand.Next(-4, 4);

            velocity = new Vector2(randX, randY);
        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            hitBox = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);

            if (position.X < -10 - tex.Width || position.X > 1850 + tex.Width || position.Y < -5 - tex.Height || position.Y > 980 + tex.Height)
            {
                isVisible = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex,hitBox, Color.White);
        }
    }
}
    