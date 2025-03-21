﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube2
{
    class AnimationManager
    {
        int numFrames;
        int numColumns;
        Vector2 size;

        int counter;
        int activeFrame;
        int interval;

        int rowPos;
        int colPos;


        public AnimationManager(int numFrames, int numColumns, Vector2 size)
        {
            this.numFrames = numFrames;
            this.numColumns = numColumns;
            this.size = size;

            counter = 0;
            activeFrame = 0;
            interval = 2;
            rowPos = 0;
            colPos = 0;
        }

        public void Update()
        {
            counter++;
            if (counter > interval)
            {
                counter = 0;
                NextFrame();
            }

        }

        public void NextFrame()
        {
            activeFrame++;
            colPos++;
            if (activeFrame >= numFrames)
            {
                activeFrame = 0;
                colPos = 0;
                rowPos = 0;

            }

            if (colPos >= numColumns)
            {
                colPos = 0;
                rowPos++;
            }
        }
         
        public Rectangle GetFrame()
        {
            return new Rectangle(colPos * (int)size.X, rowPos * (int)size.Y, (int)size.X, (int)size.Y);
        }
    }
}
