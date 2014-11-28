using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public static class Image
    {
        public static Texture2D Particle
        {
            get { return Game1.GameContent.Load<Texture2D>("Testing/Particle"); }
        }

        //Character Imgs
        public class Character
        {
            public static Texture2D Starter
            {
                get { return Game1.GameContent.Load<Texture2D>("Character/NewPointer"); }
            }
        }

        //Gun Imgs
        public class Gun
        {
        }

        //Bulet Imgs
        public class Bullet
        {
            public static Texture2D Normal
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Bullets/bullet"); }
            }
        }

        //Enemy Imgs
        public class Enemy
        {
            public static Texture2D Zombie
            {
                get { return Game1.GameContent.Load<Texture2D>("Enemies/Robot"); }
            }
        }

        //Object imgs
        public class Object
        {
            public static Texture2D Salvage
            {
                get { return Game1.GameContent.Load<Texture2D>("Testing/Pomru_StrawberryCakeHealth"); }
            }
        }

        
        // other images...
    }
}
