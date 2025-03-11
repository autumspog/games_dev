using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube1
{
    internal class Player : Sprite
    {
        List<Sprite> collisionGroup;
        public Player(Texture2D texture, Vector2 position, List<Sprite> collisionGroup) : base(texture, position) 
        {
            this.collisionGroup = collisionGroup;
        }

        //bool space_pressed = false;
        
        public override void Update(GameTime gameTime)
        {
            float changeX = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                changeX -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                changeX += 5;
            }
            position.X += changeX;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rect.Intersects(Rect))
                {
                    position.X -= changeX;
                }
            }

            float changeY = 0;
            /* Basic Gravity
            changeY += 5;
            if (!space_pressed && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                space_pressed = true;
                changeY -= 100;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                space_pressed = false;
            }
            */
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                changeY -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                changeY += 5;
            }
            position.Y += changeY;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rect.Intersects(Rect))
                {
                    position.Y -= changeY;
                }
            }

            base.Update(gameTime);

        }

    }
}
