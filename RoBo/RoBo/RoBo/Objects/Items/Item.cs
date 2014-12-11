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
        public string Name
        {
            get;
            private set;
        }

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

        public Item(Texture2D texture, float scaleFactor, Vector2 startPos, string name = "")
            : base(texture, scaleFactor, 0, startPos)
        {
            Quantity = 1;
            Name = name;
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            base.update(gameTime, stage);

            if (isColliding(stage.Character))
            {
                this.IsDead = true;
                stage.Character.pickUp(this);

                Stage.showMessage(this.Position, "+" + Quantity + " " + Name);
            }
        }
    }
}
