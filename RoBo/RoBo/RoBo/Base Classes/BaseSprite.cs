using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    //This class used to draw an image on the screen
    public class BaseSprite
    {
        protected Texture2D texture;
        protected Color color;

        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            protected set { velocity = value; }
        }

        protected Rectangle rec;
        public Rectangle Rec
        {
            get { return rec; }
            protected set { rec = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(Rec.X, Rec.Y); }
            set
            {
                if (value.X > 0)                
                    rec.X = (int)(value.X + .5);                
                else
                    rec.X = (int)(value.X - .5);

                if (value.Y > 0)
                    rec.Y = (int)(value.Y + .5);
                else
                    rec.Y = (int)(value.Y - .5);
            }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Vector2 Center
        {
            get { return new Vector2(Rec.Center.X, Rec.Center.Y); }
        }

        public virtual bool IsVisible
        {
            get;
            protected set;
        }

        public int Speed
        {
            get;
            private set;
        }

        public float ScaleFactor
        {
            get;
            private set;
        }

        public BaseSprite(Texture2D texture, float scaleFactor, Vector2 startPos)
        {
            this.texture = texture;
            this.ScaleFactor = scaleFactor;
            color = Color.White;
            IsVisible = true;

            int inDisplayWidth = Game1.View.Width;

            if (texture != null)
            {
                rec.Width = (int)(inDisplayWidth * ScaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                rec.Height = (int)(Rec.Width / aspectRatio + 0.5f);
            }

            Speed =  0;

            Position = startPos;
        }

        public BaseSprite(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos)
        {
            this.texture = texture;
            this.ScaleFactor = scaleFactor;
            color = Color.White;
            IsVisible = true;

            int inDisplayWidth = Game1.View.Width;

            if (texture != null)
            {
                rec.Width = (int)(inDisplayWidth * scaleFactor + 0.5f);
                float aspectRatio = (float)texture.Width / texture.Height;
                rec.Height = (int)(Rec.Width / aspectRatio + 0.5f);
            }

            // Shorthand condition logic
            Speed = (secondsToCrossScreen > 0) ? (int)(inDisplayWidth / (secondsToCrossScreen * 60.0f)) : 0;

            Position = startPos;
        }

        public virtual void update(GameTime gameTime, IStage stage)
        {
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
                spriteBatch.Draw(texture, Rec, color);
        }


        //uses dis formula to get dis between 2 points
        public float measureDis(Vector2 point1, Vector2 point2)
        {
            return (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public float measureDis(Vector2 point1)
        {
            return measureDis(this.Center, point1);
        }

        public float measureDis(BaseSprite sprite)
        {
            return measureDis(this.Center, sprite.Center);
        }

        //Checks for collision
        public bool isColliding(Rectangle inRec)
        {
            return this.Rec.Intersects(inRec);
        }

        public bool isColliding(BaseSprite sprite)
        {
            return this.Rec.Intersects(sprite.Rec);
        }

        public bool isColliding(Vector2 point)
        {
            bool inX = Rec.X <= point.X && point.X <= Rec.X + Rec.Width;
            bool inY = Rec.Y <= point.Y && point.Y <= Rec.Y + Rec.Height;
            return inX && inY;
        }
    }
}
