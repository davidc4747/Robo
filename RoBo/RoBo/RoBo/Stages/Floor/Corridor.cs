using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Corridor : Floor
    {
        Rectangle rec2;
        public Rectangle Rec2 { get { return rec2; } }//deltatheta = w avg * t

        public Corridor(IStage stage, Floor floor1, Floor floor2)
            : base(stage)
        {
            color = Color.Black;
            int corridorSpace = rand.Next((int)(stage.Character.Rec.Width * 1.5f), stage.Character.Rec.Width * 2);

            Vector2 direc = floor2.Center - floor1.Center;

            if (rand.NextDouble() > 0.5f)
            {
                //X component first
                if (direc.X > 0)
                {
                    rec.X = floor1.Rec.X + floor1.Rec.Width;
                    rec.Width = rand.Next(corridorSpace, floor2.Rec.Width) + floor2.Rec.X - rec.X;

                    rec2.X = rec.X + rec.Width - corridorSpace;
                }
                else
                {
                    rec.X = rand.Next(floor2.Rec.Width - corridorSpace) + (int)floor2.Rec.X;
                    rec.Width = floor1.Rec.X - rec.X;

                    rec2.X = rec.X;
                }
                rec.Y = rand.Next(floor1.Rec.Height - corridorSpace) + floor1.Rec.Y;
                rec.Height = corridorSpace;

                //--Y component--
                if (direc.Y < 0)
                {
                    rec2.Y = (int)floor2.Center.Y;
                }
                else
                {
                    rec2.Y = rec.Y;
                }
                rec2.Width = corridorSpace;
                rec2.Height = Math.Abs((int)floor2.Center.Y - rec.Y);

            }
            else
            {
                //Y component first
                if (direc.Y > 0)
                {
                    rec.Y = floor1.Rec.Y + floor1.Rec.Height;
                    rec.Height = rand.Next(corridorSpace, floor2.Rec.Height) +  floor2.Rec.Y - rec.Y;

                    rec2.Y = rec.Y + rec.Height - corridorSpace;
                }
                else
                {
                    rec.Y = rand.Next(floor2.Rec.Height - corridorSpace) + (int)floor2.Rec.Y;
                    rec.Height =  floor1.Rec.Y - rec.Y;

                    rec2.Y = rec.Y;
                }
                rec.X = rand.Next(floor1.Rec.Width - corridorSpace) + floor1.Rec.X;
                rec.Width = corridorSpace;

                //--X component--
                if (direc.X < 0)
                {
                    rec2.X = (int)floor2.Center.X;
                }
                else
                {
                    rec2.X = rec.X;
                }
                rec2.Height = corridorSpace;
                rec2.Width = Math.Abs((int)floor2.Center.X - rec.X);

            }

            //generate objects, houses, scavange
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            spriteBatch.Draw(texture, rec2, color);
            //spriteBatch.Draw(this.Texture, this.Rec, this.Rec, this.color, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
