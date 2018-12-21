using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullets : Object
    {
        public Texture2D tex;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;


        public Bullets(Texture2D newTexture)
        {
            tex = newTexture;
            
        }

        public void Update(GraphicsDevice graphics)
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex,position,hitBox, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
