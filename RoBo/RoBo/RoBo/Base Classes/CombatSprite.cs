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

        public int Exp
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

        public Gun CurGun
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
        }

    }
}
