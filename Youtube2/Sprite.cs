using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Youtube2
{
    public class Sprite
    {
        public Texture2D texture;
        public Rectangle rect;
        public Rectangle srect;
        public Vector2 velocity;
        public bool Grounded { get; set; }
        public int Direction { get; set; } //-1 for left, 1 for right

        private int numJumps;

        public int jumpCounter;

        //public AnimationManager am;

        public Sprite(Texture2D texture, Rectangle rect, Rectangle srect)
        {
            this.texture = texture;
            this.rect = rect;
            this.srect = srect;
            Grounded = false;
            Direction = -1;
            numJumps = 2;
            jumpCounter = 0;
            velocity = new();
        }

        public void Update(KeyboardState keystate, KeyboardState prevKeystate, GameTime gameTime)
        {
            //velocity = Vector2.Zero;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            int prevDir = Direction;
            // remove to make friction velocity.X = 0;
            velocity.Y += 35f * dt;

            velocity.Y = Math.Min(25.0f, velocity.Y);



            /* same as aboveif (velocity.Y > 25f)
            {
                velocity.Y = 25f;
            }
            */
            if (keystate.IsKeyDown(Keys.Left) || keystate.IsKeyDown(Keys.A))
            {
                //make stop dead
                //velocity.X = -300 * dt;
                //Make a sldie stop
                velocity.X += -15 * dt;
                Direction = 1;
            }
            if (keystate.IsKeyDown(Keys.Right) || keystate.IsKeyDown(Keys.D))
            {
                //make stop dead
                //velocity.X = 300 * dt;
                //slow slide
                velocity.X += +15 * dt;
                Direction = -1;
            }

            velocity.X = Math.Max(-300, Math.Min(300, velocity.X)); //stay in with max speeds.
            velocity.X *= 0.95f;
            //Debug.Write(jumpCounter);
            if (jumpCounter < numJumps && keystate.IsKeyDown(Keys.Space) && !prevKeystate.IsKeyDown(Keys.Space))
            {
                velocity.Y = -600 * dt;
                jumpCounter++;
            }
            
            /*
            if (keystate.IsKeyDown(Keys.Up) || keystate.IsKeyDown(Keys.W))
            {
                velocity.Y = -5;
            }
            if (keystate.IsKeyDown(Keys.Down) || keystate.IsKeyDown(Keys.S))
            {
                velocity.Y = 5;
            }
            */

            if (prevDir != Direction)
            {
                srect.X += srect.Width;
                srect.Width = -srect.Width;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
           // Rectangle dest = new(drect.X + (int)offset.X, drect.Y + (int)offset.Y, drect.Width, drect.Height);

            spriteBatch.Draw(texture, rect, srect, Color.White);
        }

        
    }
}
