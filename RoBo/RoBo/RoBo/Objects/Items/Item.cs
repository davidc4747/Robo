using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Item : Object
    {
        public float DropRate
        {
            get;
            protected set;
        }

        public int Quantity
        {
            get;
            protected set;
        }

        public Item(Texture2D texture, float scaleFactor, Vector2 startPos)
            : base(texture, scaleFactor, 0, startPos)
        {
        }

        public override void update(GameTime gameTime, IStage Stage)
        {
            base.update(gameTime, Stage);

            if (isColliding(Stage.Character))
            {
                this.IsDead = true;
                Stage.Character.pickUp(this);
            }
        }
    }
}
