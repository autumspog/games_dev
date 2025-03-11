using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube1
{
    internal class ScaleSprite : Sprite //This is hwo we can inhert traits from another class
    {
        //public Rectangle Rect
        //{
        //    get
        //    {
        //        return new Rectangle((int)position.X, (int)position.Y, 200, 200);
        //    }
        //}
        public ScaleSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {

        }
    }
}
