using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class CombatSprite : Object
    {
        //Min Human : 55
        //Max Human : 160+
        //Avg       : 85-115
        public int IQ
        {
            get;
            protected set;
        }

        private int health;
        public int Health
        {
            get { return health; }
            protected set
            {//Clamp value between MaxHealth and 0.0
                health = (value > MaxHealth) ? MaxHealth : (value < 0) ? 0 : value;
            }

        }

        public int MaxHealth
        {
            get;
            protected set;
        }

        public Gun CurGun//TODO: move to RangedEnemy
        {
            get;
            protected set;
        }

        public Rectangle FutureRec { get; protected set; }

        public CombatSprite(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos)
            : base(texture, scaleFactor, secondsToCrossScreen, startPos)
        {
        }

        public virtual void damage(Object obj)
        {
        }

        public virtual void damage(Bullet bull)
        {
            Health -= bull.Damage;
            Stage.showMessage(this.Position, bull.Damage.ToString(), Color.Red);
        }

    }
}
