using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class HealthPack : Item
    {
        public HealthPack(Vector2 startPos)
            : base(Image.Object.HealthPack, 0.037f, startPos, "Health Pack")
        {
        }
    }
}
