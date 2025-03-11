using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube1; 

class AnimationManager
{
    int numFrames;
    int numColumns;
    Vector2 size;

    int counter;
    int activeFrame;
    int interval;

    int colPos;
    int rowPos;


    public AnimationManager(int numFrames, int numColumns, Vector2 size)
    {
        this.numFrames = numFrames;
        this.numColumns = numColumns;
        this.size = size;

        counter = 0;
        activeFrame = 0;
        interval = 5;

        colPos = 0;
        rowPos = 0;
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

    private void NextFrame()
    {
        activeFrame++;
        colPos++;

        if (activeFrame > numFrames)
        {
            ResetAnimation();
        }
        if (colPos >= numColumns)
        {
            colPos = 0;
            rowPos++;
        }
    }

    private void ResetAnimation()
    {
        activeFrame = 0;
        colPos = 0;
        rowPos = 0;
    }
    public Rectangle GetFrame()
    {
        return new Rectangle(
            colPos * (int)size.X,
            rowPos * (int)size.Y,
            (int)size.X,
            (int)size.Y
            );
    }
}
