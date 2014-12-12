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
        float checkTimer, visTimer;
        int flashCount = 0;

        public string Name
        {
            get;
            protected set;
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

            checkTimer = 1;
            visTimer = 60000;// 1 mins
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            base.update(gameTime, stage);

            //Check for pick up
            checkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (checkTimer > 1 && isColliding(stage.Character))
            {
                checkTimer = 0;

                //Check if the player wants to pick you up
                this.IsDead = stage.Character.pickUp(this);

                if (IsDead)
                    Stage.showMessage(this.Position, "+" + Quantity + " " + Name);
                else
                    Stage.showMessage(this.Position, "Full: " + Name);
            }

            //Flash animation
            visTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (visTimer <= 0)
            {
                float nextVal; 
                if (IsVisible)
                {
                    flashCount++;
                    nextVal = (float)(1000 / Math.Pow(flashCount, 1.3f) + 50);
                }
                else
                {
                    nextVal = (float)(2000 / Math.Pow(flashCount, 1.5f) + 100);
                    IsDead = (int)(nextVal + .5f) <= 115;
                }
                visTimer = nextVal;
                IsVisible = !IsVisible;
            }
        }
    }
}
