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
        public TechType TechType
        {
            get;
            private set;
        }

        public Salvage(Vector2 startPos)
            : base(Image.Object.Salvage, 0.037f, startPos)
        {
            TechType = (TechType)rand.Next(Enum.GetNames(typeof(TechType)).Length);
            Quantity = rand.Next(31) + 1;

            Name = "";
            switch (TechType)
            {
                case TechType.HUMAN:
                    //TODO: change img
                    Name += "Human ";
                    break;
                case TechType.ALIEN:
                    //change img
                    Name += "Alien ";
                    break;
                case TechType.ROBOT:
                    //change img
                    Name += "Robot ";
                    break;
            }

            Name += "Salvage";
        }
    }
}
