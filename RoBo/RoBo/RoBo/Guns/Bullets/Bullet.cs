using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Bullet : RotatingSprite
    {
        protected float disTraveled;
        private static int randCount = 0;

        public bool Enabled
        {
            get;
            protected set;
        }

        public float Range
        {
            get;
            private set;
        }

        public int Damage
        {
            get;
            private set;
        }

        public Bullet(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Gun gun)
            : base(texture, scaleFactor, secondsToCrossScreen, gun.Character.Position)
        {
            const int MAX_INACCURACY = 1;//radians
            double acurracyRange = MAX_INACCURACY * (1 - gun.Accuracy);

            randCount++;
            Random inAccGen = new Random(randCount);
            float randNum = (float)(acurracyRange * 2 * inAccGen.NextDouble() - acurracyRange);

            //Set velocity and rotation
            Rotation = gun.Character.Rotation + randNum;
            velocity.X += (int)(Speed * Math.Sin(Rotation));
            velocity.Y -= (int)(Speed * Math.Cos(Rotation));

            Range = gun.Range;
            Damage = gun.Damage;
            Enabled = true;
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            if (Enabled && disTraveled < Range)
            {
                Position += velocity;
                disTraveled += velocity.Length();

                foreach(RotatingSprite obj in stage.Everything)
                    if (isColliding(obj))
                    {
                        hitObject();
                        Enabled = false;
                    }
            }
            else
                Enabled = IsVisible = false;
                

        }

        protected virtual void hitObject()
        {
        }
    }    
}
