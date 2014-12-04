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
        public class Ship
        {
            public static Texture2D Starter
            {
                get { return Game1.GameContent.Load<Texture2D>("Ship/defaultDrone"); }
            }
        }

        //Gun Imgs
        public class Gun
        {
            public static Texture2D PhysPistol
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/bulletpistol"); }
            }
        }

        //Bulet Imgs
        public class Bullet
        {
            public static Texture2D Physical
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Bullets/bulletshot"); }
            }

            public static Texture2D Plasma
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Bullets/plasmashot"); }
            }

            public static Texture2D Laser
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Bullets/lasershot"); }
            }
        }

        public class Muzzle
        {
            public static Texture2D Physical
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Muzzle/muzzleflare"); }
            }

            public static Texture2D Plasma
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Muzzle/plasmaEww"); }
            }

            public static Texture2D Laser
            {
                get { return Game1.GameContent.Load<Texture2D>("Guns/Muzzle/lasserball"); }
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
