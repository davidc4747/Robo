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
    public abstract class Gun : RotatingSprite
    {
        Input input = new Input();
        WeaponType wepType;

        private Vector2 offset;

        protected RotatingSprite muzzleFlare;
        protected Vector2 muzzleOffset;

        protected List<Bullet> bulls;

        private float fireTimer, reloadTimer;
        protected bool isReloading, isShooting;
        private bool isAutomatic;

        public CombatSprite Holder
        {
            get;
            private set;
        }

        //public Bullet[] Bullets
        //{
        //    private get { return bulls.ToArray(); }
        //}
        
        //Gun Stats--------------
        #region Gun Stats

        public string Name
        {
            get;
            protected set;
        }

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

        public int Pierce
        {
            get;
            protected set;
        }

        #endregion

        public Gun(CombatSprite holder, Texture2D texture, float scaleFactor, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int ammoCap, int MagSize, int numShots = 1, int pierce = 1, bool isAutomatic = true, bool isSmallArms = true)
            : base(texture, scaleFactor, 10, Vector2.Zero)
        {
            if (!isSmallArms)
                offset = new Vector2(holder.Rec.Width * 0.33f, holder.Rec.Height * 0.001f);
            else
                offset = new Vector2(holder.Rec.Width * 0.20f, holder.Rec.Height * -0.12f);

            this.Position = holder.Position;
            muzzleFlare = new RotatingSprite(Image.Muzzle.Physical, 0.05f, 0, Vector2.Zero, this.Rotation);
            muzzleOffset = new Vector2(this.Position.X, (int)(this.Position.Y - muzzleFlare.Rec.Height * 0.28f - this.Rec.Height / 2));

            Name = "";
            bulls = new List<Bullet>();
            this.Holder = holder;

            this.isAutomatic = isAutomatic;

            this.Damage = damage;
            this.Pierce = pierce;
            this.NumShots = numShots;
            this.Range = range;
            this.Accuracy = accuracy;
            this.ReloadSpd = reloadSpd;
            this.FireRate = fireRate;
            
            this.CurrMag = this.MagSize = MagSize;
            this.CurAmmo = this.MaxAmmo = ammoCap;

            fireTimer = 1 / FireRate;
        }

        public sealed override void update(GameTime gameTime, IStage stage)
        {
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

            //if(the gun isn't equipt) : Only update the bullets 
            if (Holder.CurGun != this)
            {
                standBy(gameTime, stage);
                return;
            }

            input.start();

            //Recoil animation
            Matrix myRotationMatrix = Matrix.CreateRotationZ(Holder.Rotation);
            Vector2 rotatedVector = Vector2.Transform(offset, myRotationMatrix);
            Vector2 desiredVec = Holder.Position + rotatedVector;           

            if ((desiredVec - Position).Length() >= Rec.Width * 0.001f)
            {
                this.Velocity = desiredVec - Position;
                this.Velocity.Normalize();
                this.Velocity *= (Speed != 0) ? Speed : 1.0f;
            }

            this.Position += Velocity;
            this.Rotation = Holder.Rotation;            

            //if the player wants to reload
            if (input.ReloadPressed)
            {
                if (CurAmmo >= 0 && MagSize != CurrMag)                
                    isReloading = true;
            }

            isShooting = false;
            //if (not reloading and you have ammo in the Mag)
            if (!isReloading && CurrMag > 0)
            {
                //Shoot if fireRate allows it
                reloadTimer = 0;
                fireTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (fireTimer >= 1 / FireRate)
                {
                    if (isAutomatic && input.LeftClick)
                    {
                        isShooting = true;
                        shoot(gameTime);
                        CurrMag--;
                        fireTimer = 0;
                    }
                    else if (input.LeftClickPressed)
                    {
                        isShooting = true;
                        shoot(gameTime);
                        CurrMag--;
                        fireTimer = 0;                        
                    }
                }
            }
            else if (CurAmmo > 0)//if(reloading and you have ammo in your bag)
            {
                fireTimer = 1 / FireRate;                
                reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                reloading(reloadTimer);                
            }
            else
                isReloading = false;

            if (!isReloading && !isShooting)
                idle(gameTime, stage);
                       

            input.end();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            //Draw bullets
            foreach (Bullet bull in bulls)
                bull.draw(spriteBatch);

            //Draw Muzzle flare
            Matrix myRotationMatrix = Matrix.CreateRotationZ(this.Rotation);
            Vector2 muzzPos = Vector2.Transform(muzzleOffset, myRotationMatrix) + this.Position;

            muzzleFlare = new RotatingSprite(muzzleFlare.Texture, muzzleFlare.ScaleFactor, 0, muzzPos, this.Rotation);

            if (isShooting)
                muzzleFlare.draw(spriteBatch);

            //Draw Ammo count or Reload timer
            Vector2 ammoPos = Holder.Position + new Vector2(Holder.Rec.Width * 0.9f, Holder.Speed * 10);
            if (reloadTimer > 0)            
                spriteBatch.DrawString(Fonts.Normal, reloadTimer.ToString("0.0"), ammoPos, Color.Red);            
            else            
                spriteBatch.DrawString(Fonts.Normal, "" + CurrMag + "/" + CurAmmo, ammoPos, Color.White);
            


            //Draw Gun
            base.draw(spriteBatch);
        }

        protected virtual void standBy(GameTime gameTime, IStage stage)
        {
        }

        protected virtual void idle(GameTime gameTime, IStage stage)
        {
        }
        
        protected virtual void shoot(GameTime gameTime)
        {
            for (int i = 0; i < NumShots; i++)
            {
                bulls.Add(getBullet());
                this.Position += rotate(new Vector2(0, 1) * Speed * 2);//recoil
            }
        }

        protected virtual void reloading(float reloadTimer)
        {
            //Reload after ReloadSpd has passed
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

        //------

        protected Vector2 rotate(Vector2 displacemnet)
        {
            Matrix myRotationMatrix = Matrix.CreateRotationZ(Holder.Rotation);
            Vector2 moveVec = Vector2.Transform(displacemnet, myRotationMatrix);
            return moveVec;
        }

        protected virtual Bullet getBullet()
        {
            return new Shot(this);
        }

        public void Clear()
        {
            isReloading = false;
            isShooting = false;
            fireTimer = 0;
            reloadTimer = 0;
        }

    }
}
