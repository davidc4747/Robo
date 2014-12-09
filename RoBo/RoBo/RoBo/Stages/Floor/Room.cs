using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Room : Floor
    {
        public Room(IStage stage)
            : base(stage)
        {
            color = stage.Color;

            int width = rand.Next(stage.Character.Rec.Width * 3, stage.Character.Rec.Width * 10);
            int height = rand.Next(stage.Character.Rec.Height * 3, stage.Character.Rec.Height * 10);
            int x = rand.Next(stage.Rec.Width - width);
            int y = rand.Next(stage.Rec.Height - height);

            this.Rec = new Rectangle(x, y, width, height);
            //generate objects, houses, scavange
        }
    }
}
