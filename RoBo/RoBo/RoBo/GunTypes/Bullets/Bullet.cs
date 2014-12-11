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
        private List<RotatingSprite> objCollided;
        private static Random inAccGen = new Random();

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

        public int Pierce
        {
            get;
            private set;
        }

        public Bullet(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Gun gun)
            : base(texture, scaleFactor, secondsToCrossScreen, Vector2.Zero)
        {
            objCollided = new List<RotatingSprite>();

            //set start Pos
            Vector2 offset = new Vector2(gun.Position.X, (int)(gun.Position.Y - gun.Rec.Height / 2));

            //Calc Rotation inaccuracy
            Matrix myRotationMatrix = Matrix.CreateRotationZ(gun.Rotation);
            this.Position = Vector2.Transform(offset - gun.Position, myRotationMatrix) + gun.Position;
            
            const int MAX_INACCURACY = 1;//radians
            double acurracyRange = MAX_INACCURACY * (1 - gun.Accuracy);

            float randNum = (float)(acurracyRange * 2 * inAccGen.NextDouble() - acurracyRange);

            //Set velocity and rotation
            Rotation = gun.Holder.Rotation + randNum;
            velocity.X += (int)(Speed * Math.Sin(Rotation));
            velocity.Y -= (int)(Speed * Math.Cos(Rotation));

            Range = gun.Range;
            Damage = gun.Damage + inAccGen.Next(-gun.Damage / 4, gun.Damage / 4 + 1);
            Pierce = gun.Pierce;
            Enabled = true;
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            if (Enabled && disTraveled < Range)
            {
                Position += velocity;
                disTraveled += velocity.Length();

                foreach(RotatingSprite obj in stage.Everything)
                    if (isColliding(obj) && !objCollided.Contains(obj))
                    {
                        objCollided.Add(obj);
                        hitObject(obj);
                        Pierce--;
                        if (Pierce <= 0)
                            Enabled = false;
                        break;
                    }
            }
            else
                Enabled = IsVisible = false;
                

        }

        protected virtual void hitObject(RotatingSprite obj)
        {
            if (obj.GetType().IsSubclassOf(typeof(Enemy)))
            {
                Enemy ene = (Enemy)obj;
                ene.damage(this);
            }
        }
    }    
}
