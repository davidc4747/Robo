using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Enemy : CombatSprite
    {        
        public Item Drop
        {
            get;
            protected set;
        }

        public int Strength
        {
            get;
            protected set;
        }

        public Enemy(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos, int maxHealth)
            : base(texture, scaleFactor, secondsToCrossScreen, startPos)
        {
            //TODO: Changed based on IQ
            Health = MaxHealth = maxHealth;
            Strength = 1;
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            if (Health <= 0)
            {
                IsDead = true;
                IsVisible = false;
                //Drop Item
                setDrop();
                return;
            }

            base.update(gameTime, stage);

            //Calc Rotation
            Vector2 lookVek = stage.Character.Center - this.Center;
            Rotation = (float)Math.Atan2(lookVek.X, -lookVek.Y);

            //Movement
            lookVek.Normalize();
            velocity = lookVek * Speed;

            //Checks for Collision
            FutureRec = new Rectangle((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y), Rec.Width, Rec.Height);
            if (isColliding(FutureRec, Rotation, stage.Character))
            {
                Velocity = Vector2.Zero;
                stage.Character.damage(this);
            }
            Position += velocity;
        }

        protected virtual void setDrop()
        {
            //Select an Item to drop

            double randNum = rand.NextDouble();
            if (randNum > 0.75f)
                Drop = new HealthPack(Position);
            else if (randNum > 0.65f)
                Drop = new Salvage(Position);
            else if (randNum > 0.40f)
                Drop = new Ammo(Position);
            else
                Drop = new GunItem(Position, this);

            //---Drops---
            //Salvage
            //Health Kit
            //Ammo
            //Bomb
            //~~Money
        }
    }
}
