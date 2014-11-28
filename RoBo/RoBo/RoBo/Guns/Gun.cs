using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoBo
{
    public abstract class Gun
    {
        Input input = new Input();

        protected List<Bullet> bulls;

        private float fireTimer, reloadTimer;
        private bool isReloading, isAutomatic;

        public Character Character
        {
            get;
            private set;
        }

        public Bullet[] Bullets
        {
            get { return bulls.ToArray(); }
        }


        //Gun Stats--------------

        private int maxAmmo;
        public int MaxAmmo
        {
            get { return maxAmmo; }
            protected set
            {
                maxAmmo = (value < 0) ? 0 : value;
            }
        }

        private int curAmmo;
        public int CurAmmo
        {
            get { return curAmmo; }
            protected set
            {
                curAmmo = (value < 0) ? 0 : value;
            }
        }

        private int magSize;
        public int MagSize
        {
            get { return magSize; }
            protected set
            {
                magSize = (value < 0) ? 0 : value;
            }
        }

        private int curMag;
        public int CurrMag
        {
            get { return curMag; }
            protected set
            {
                curMag = (value < 0) ? 0 : value;
            }
        }

        private float reloadSpd;
        public float ReloadSpd
        {
            get { return reloadSpd; }
            protected set
            {
                reloadSpd = (value < 0) ? 0 : value;
            }
        }

        private float fireRate;
        public float FireRate
        {
            get { return fireRate; }
            protected set
            {
                fireRate = (value < 0) ? 0.001f : value;
            }
        }

        private int numShots;
        public int NumShots
        {
            get { return numShots; }
            protected set
            {
                numShots = (value < 0) ? 0 : value;
            }
        }

        private float accuracy = 1;
        public float Accuracy
        {
            get { return accuracy; }
            protected set
            {//Clamp value between 1.0 and 0.0
                accuracy = (value > 1) ? 1 : (value < 0) ? 0 : value;
            }

        }

        public float Range
        {
            get;
            protected set;
        }

        public int Damage
        {
            get;
            protected set;
        }

        public Gun(Character character, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int ammoCap, int MagSize, int numShots = 1, bool isAutomatic = true)
        {
            bulls = new List<Bullet>();
            this.Character = character;

            this.isAutomatic = isAutomatic;

            this.Damage = damage;
            this.NumShots = numShots;
            this.Range = range;
            this.Accuracy = accuracy;
            this.ReloadSpd = reloadSpd;
            this.FireRate = fireRate;
            

            this.CurrMag = this.MagSize = MagSize;
            this.CurAmmo = this.MaxAmmo = ammoCap;

            fireTimer = 1 / FireRate;
        }

        public virtual void update(GameTime gameTime, IStage stage)
        {
            input.start();

            //if the player wants to reload
            if (input.ReloadPressed)
            {
                if (CurAmmo >= 0 && MagSize != CurrMag)                
                    isReloading = true;
            }

            //if (not reloading and you have ammo in the Mag)
            if (!isReloading && CurrMag > 0)
            {
                //Shoot if fireRate allows itS
                reloadTimer = 0;
                fireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (fireTimer >= 1 / FireRate)
                {
                    if (isAutomatic && input.LeftClick)
                    {
                        shoot(gameTime);
                        CurrMag--;
                        fireTimer = 0;                        
                    }
                    else if (input.LeftClickPressed)
                    {
                        shoot(gameTime);
                        CurrMag--;
                        fireTimer = 0;                        
                    }
                }
            }
            else if (CurAmmo > 0)//if(reloading and you have ammo in your bag)
            {
                //Reload after ReloadSpd has passed
                fireTimer = 1 / FireRate;
                reloading(reloadTimer);
                reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (reloadTimer >= ReloadSpd)
                {
                    int ammoNeeded = MagSize - CurrMag;

                    if (CurAmmo >= ammoNeeded)
                    {
                        CurrMag = MagSize;
                        CurAmmo -= ammoNeeded;
                    }
                    else
                    {
                        CurrMag += CurAmmo;
                        CurAmmo = 0;
                    }

                    isReloading = false;
                }
            }
            else
                isReloading = false;


            //Update or remove bullets
            int i = 0;
            while (i < bulls.Count)
            {
                if (bulls[i].Enabled)
                    bulls[i].update(gameTime, stage);
                else
                {
                    bulls.RemoveAt(i);
                    i--;
                }
                i += 1;
            }

            input.end();
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bull in bulls)
                bull.draw(spriteBatch);


            Vector2 ammoPos = Character.Position + new Vector2(Character.Speed * 10, Character.Speed * 10);
            spriteBatch.DrawString(Fonts.Normal, "" + CurrMag + "/" + CurAmmo, ammoPos, Color.White);

            if (reloadTimer > 0)
            {
                Vector2 reloadPos = Character.Position + new Vector2(-Character.Speed * 10, Character.Speed * 10);
                spriteBatch.DrawString(Fonts.Normal, reloadTimer.ToString("0.0"), reloadPos, Color.Red);
            }
        }

        public virtual void shoot(GameTime gameTime)
        {
            for (int i = 0; i < NumShots; i++)
                bulls.Add(new Shot(this));
        }

        public virtual void reloading(float reloadTime)
        {
        }
    }
}
