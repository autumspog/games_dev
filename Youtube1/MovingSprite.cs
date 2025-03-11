using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube1
{
    internal class MovingSprite : ScaleSprite
    {
        private float speed;

        public MovingSprite(Texture2D texture, Vector2 position, float speed) : base(texture, position)
        {
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position.X += speed;
        }
    }
}
