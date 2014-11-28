using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Enemy : AnimatedSprite, ICombatSprite
    {
        public Rectangle FutureRec { get; private set; }

        public bool IsDead
        {
            get;
            protected set;
        }

        public int Health
        {
            get;
            protected set;
        }

        public int MaxHealth
        {
            get;
            protected set;
        }

        public Item Drop
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
            FutureRec = new Rectangle((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y), Rec.Width, Rec.Height);
            if (isColliding(FutureRec, Rotation, stage.Character))
                Velocity = Vector2.Zero;

            Position += velocity;

            //Checks for Collision
            foreach (Bullet bull in stage.Character.CurGun.Bullets)
                if (isColliding(bull))
                    Health -= bull.Damage;
        }


        protected virtual void drop()
        {
            Drop = new Salvage(Position);

            //Drops----
            //Salvage
            //Ammo
            //~~Money
        }
    }
}
