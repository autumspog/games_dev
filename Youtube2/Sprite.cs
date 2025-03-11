using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube2
{
    public class Sprite
    {
        public Texture2D texture;
        public Rectangle drect, srect;

        public Sprite(Texture2D texture, Rectangle drect, Rectangle srect)
        {
            this.texture = texture;
            this.drect = drect;
            this.srect = srect;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Rectangle dest = new(drect.X + (int)offset.X, drect.Y + (int)offset.Y, drect.Width, drect.Height);

            spriteBatch.Draw(texture, dest, srect, Color.White);
        }

        
    }
}
