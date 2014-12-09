using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace RoBo
{
    public class Character : AnimatedSprite, ICombatSprite
    {
        Input input = new Input();

        Vector2 mousePos;

        bool isSwaping;
        float swapTimer = 0;

        int gunIndex;
        Inventory invent;
        List<Gun> guns;        
        Hud hud;

        public Rectangle FutureRec { get; private set; }

        public Gun CurGun
        {
            get;
            private set;
        }

        public bool IsDead
        {
            get;
            private set;
        }

        public int Health
        {
            get;
            private set;
        }

        public int MaxHealth
        {
            get;
            private set;
        }

        public Character()
            : base(Image.Ship.Starter, 0.14f, 2.5f, Vector2.Zero)
        {
            guns = new List<Gun>();
            guns.Add(new Pistol(this));
            guns.Add(new Shotgun(this));
            guns.Add(new LaserPistol(this));
            guns.Add(new PlasmaPistol(this));

            CurGun = guns[0]; 
            hud = new Hud(this);
            invent = new Inventory(this);
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            input.start();

            //Calc Rotation
            mousePos.X = input.MousePos.X + Position.X - Game1.View.Width / 2;
            mousePos.Y = input.MousePos.Y + Position.Y - Game1.View.Height / 2;

            Vector2 lookVek = mousePos - this.Position;
            Rotation = (float)Math.Atan2(lookVek.X, -lookVek.Y);
            
            //Wep Swap
            if (input.SwapActDown || input.DeltaWheelValue < 0)            
                gunIndex = (gunIndex - 1 < 0) ? guns.Count - 1 : gunIndex - 1;            
            else if (input.SwapActUp || input.DeltaWheelValue > 0)            
                gunIndex = (gunIndex + 1) % guns.Count;
            
            //Small Delay between switching weapons
            if (CurGun != guns[gunIndex])
            {
                isSwaping = true;
                swapTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (swapTimer >= 0.3f)
                {
                    CurGun.Clear();
                    CurGun = guns[gunIndex];
                    swapTimer = 0;
                    isSwaping = false;
                }
            }
            else
                isSwaping = false;

            //Check for Movment
            if (input.Right && this.Position.X < stage.Rec.Width)             
                velocity.X = this.Speed;            
            if (input.Left && this.Position.X > stage.Rec.X)            
                velocity.X = -this.Speed;
            if (input.Up && this.Position.Y > stage.Rec.Y)            
                velocity.Y = -this.Speed;            
            if (input.Down && this.Position.Y < stage.Rec.Height)            
                velocity.Y = this.Speed;
            
            //Movement
            this.Position += this.velocity;
            velocity = velocity * 0.90f;

            //Check Collision
            FutureRec = new Rectangle((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y), Rec.Width, Rec.Height);
            foreach (RotatingSprite sprite in stage.Everything)
                if (isColliding(FutureRec, Rotation, sprite))
                    Velocity = Vector2.Zero;

            foreach (Item item in stage.Items)
                if (isColliding(item))
                    invent.add(item);

            //update aggregate classes            
            hud.update(gameTime);
            foreach (Gun gun in guns)
                gun.update(gameTime, stage);
            
            input.end();
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (!isSwaping)
                CurGun.draw(spriteBatch);
            base.draw(spriteBatch);
            hud.draw(spriteBatch);

            spriteBatch.Draw(Image.Particle, new Rectangle((int)mousePos.X, (int)mousePos.Y, 3, 3), Color.Red);            
        }
    }
}
