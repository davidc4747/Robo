using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Salvage : Item
    {
        public Salvage(Vector2 startPos)
            : base(Image.Object.Salvage, 0.037f, startPos, "Salvage")
        {
            Quantity = rand.Next(31);
        }
    }
}
