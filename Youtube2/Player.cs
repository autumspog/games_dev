using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube2
{
    public class Player : Sprite
    {
        public Player(Texture2D texture, Rectangle drect, Rectangle srect) :  base(texture, drect, srect) { }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                drect.X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                drect.X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                drect.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                drect.Y += 5;
            }
            //base.Update();
        }
    }
}
