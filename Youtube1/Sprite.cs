using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube1
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        //video 3 updates
        private static readonly float SCALE = 4f;

        
        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width /(int)SCALE, texture.Height / (int)SCALE);
                //return new Rectangle((int)position.X, (int)position.Y, (int)SCALE, (int)SCALE);
               // return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }


        //Video 3 end
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
        
        }

        //Video 3 update.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rect, Color.White);
        }
        //v3 End
    }
}
