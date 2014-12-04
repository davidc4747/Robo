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
        private Vector2 offset;

        protected List<Bullet> bulls;

        private float fireTimer, reloadTimer;
        private bool isReloading, isAutomatic;
        private bool isShooting;

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

        public Gun(Character character,Texture2D texture, float scaleFactor, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int ammoCap, int MagSize, int numShots = 1, bool isAutomatic = true, bool isSmallArms = true)
            : base(texture, scaleFactor, 10, Vector2.Zero)
        {
            if (!isSmallArms)
                offset = new Vector2(character.Rec.Width * 0.30f, character.Rec.Height * 0.001f);
            else
                offset = new Vector2(character.Rec.Width * 0.17f, character.Rec.Height * -0.1f);


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

        public override void update(GameTime gameTime, IStage stage)
        {
            input.start();

            //Recoil animation
            Matrix myRotationMatrix = Matrix.CreateRotationZ(Character.Rotation);
            Vector2 rotatedVector = Vector2.Transform(offset, myRotationMatrix);
            Vector2 desiredVec = Character.Position + rotatedVector;           

            if ((desiredVec - Position).Length() >= Rec.Width * 0.001f)
            {
                this.Velocity = desiredVec - Position;
                this.Velocity.Normalize();
                this.Velocity *= (Speed != 0) ? Speed : 1;
            }

            this.Position += Velocity;
            this.Rotation = Character.Rotation;            

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
                //Shoot if fireRate allows itS
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

        public override void draw(SpriteBatch spriteBatch)
        {
            //Draw bullets
            foreach (Bullet bull in bulls)
                bull.draw(spriteBatch);

            //Draw Muzzle flare
            RotatingSprite muzzleFire = new RotatingSprite(Image.Muzzle.Physical, 0.05f, 0, Vector2.Zero, this.Rotation);

            Vector2 offset = new Vector2(this.Position.X, (int)(this.Position.Y - muzzleFire.Rec.Height * 0.28f - this.Rec.Height / 2));
            Matrix myRotationMatrix = Matrix.CreateRotationZ(this.Rotation);
            offset = Vector2.Transform(offset - this.Position, myRotationMatrix) + this.Position;

            muzzleFire = new RotatingSprite(Image.Muzzle.Physical, 0.05f, 0, offset, this.Rotation);

            if (isShooting)
                muzzleFire.draw(spriteBatch);

            //Draw Ammo count
            Vector2 ammoPos = Character.Position + new Vector2(Character.Speed * 10, Character.Speed * 10);
            spriteBatch.DrawString(Fonts.Normal, "" + CurrMag + "/" + CurAmmo, ammoPos, Color.White);

            //Draw Reload time
            if (reloadTimer > 0)
            {
                Vector2 reloadPos = Character.Position + new Vector2(-Character.Speed * 10, Character.Speed * 10);
                spriteBatch.DrawString(Fonts.Normal, reloadTimer.ToString("0.0"), reloadPos, Color.Red);
            }

            //Draw Gun
            base.draw(spriteBatch);
        }

        private Vector2 rotate(Vector2 displacemnet)
        {
            Matrix myRotationMatrix = Matrix.CreateRotationZ(Character.Rotation);

            Vector2 moveVec = Vector2.Transform(displacemnet, myRotationMatrix);

            return moveVec;
        }

        public void Clear()
        {
            bulls.Clear();
        }
        
        protected virtual void shoot(GameTime gameTime)
        {
            for (int i = 0; i < NumShots; i++)
            {
                bulls.Add(new Shot(this));
                this.Position += rotate(new Vector2(0, 1) * Speed * 2);//recoil
            }
        }

        protected virtual void reloading(float reloadTime)
        {
        }
    }
}
