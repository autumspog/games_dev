using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube1
{
    internal class ColoredSprite : ScaleSprite
    {
        public Color color;

        public ColoredSprite(Texture2D texture, Vector2 position, Color color) : base(texture, position)
        {
            this.color = color;
        }
    }
}
