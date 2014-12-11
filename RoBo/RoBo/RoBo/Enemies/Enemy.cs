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
            Health = MaxHealth = maxHealth;
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            if (Health < 0)
            {
                IsDead = true;
                IsVisible = false;
                //Drop Item
                drop();
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
                Velocity = Vector2.Zero;

            Position += velocity;
        }

        protected virtual void drop()
        {
            //Select an Item to drop
            Drop = new Salvage(Position);

            //---Drops---
            //Salvage
            //Ammo
            //~~Money
        }
    }
}
