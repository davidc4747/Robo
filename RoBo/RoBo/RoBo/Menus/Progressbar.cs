using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Progressbar : BaseSprite
    {
        protected Character character;

        protected Vector2 offset, cropDisplace;
        protected Rectangle cropRec;
        Color topColor;

        private float percent = 1;
        public float Percent
        {
            get { return percent; }
            protected set
            {//Clamp value between 1.0 and 0.0
                percent = (value > 1) ? 1 : (value < 0) ? 0 : value;
            }

        }

        public Progressbar(Character character, Texture2D texture, float scaleFactor, Vector2 offset, Color topColor)
            : base(texture, scaleFactor, Vector2.Zero)
        {
            this.offset = offset;
            this.topColor = topColor;
            this.character = character;
            color = new Color(128, 128, 128, 64);
            Percent = 1.0f;            
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            //Position Bar
            this.Position = stage.Character.Position + offset;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);

            float scale = rec.Width / (float)texture.Width;
            spriteBatch.Draw(texture, new Vector2(rec.X + (int)cropDisplace.X, rec.Y + (int)cropDisplace.Y), cropRec, topColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
