using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    class Player : Object
    {
        ContentManager Content;
        Texture2D tex;
        public Vector2 position;
        Vector2 spriteorigin, distance;
        public float rotation;

        

        public void Load(ContentManager Content)
        {
            tex = Content.Load<Texture2D>("asteroids2");
            
        }

        public Player() {  }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += 3;
            }

            
            MouseState mouse = Mouse.GetState();

            distance.X = mouse.X - position.X;
            distance.Y = mouse.Y - position.Y;

            rotation = (float)Math.Atan2(distance.Y, distance.X);

            
        }

        public void Update(GameTime gameTime)
        {

            hitBox = new Rectangle((int)position.X, (int)position.Y,tex.Width, tex.Height);
            Input(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, position,null, Color.White, rotation-MathHelper.ToRadians(45), spriteorigin,1, SpriteEffects.None, 0);

            
        }
    }
}
