using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{

    //To allows sub classes to easily play animations
    public abstract class AnimatedSprite : RotatingSprite
    {
        Animation curAni;
        int aniIndex;

        float time = 0;

        private SpriteEffects flip;
        public bool Flip
        {
            get
            {
                return flip == SpriteEffects.FlipHorizontally;
            }
            protected set
            {
                if (value)
                    flip = SpriteEffects.FlipHorizontally;
                else
                    flip = SpriteEffects.None;
            }
            
        }

        public AnimatedSprite(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos)
            : base (texture, scaleFactor, secondsToCrossScreen, startPos)
        {
            this.Flip = false;
        }

        public override void update(GameTime gameTime, IStage Stage)
        {
            if (curAni != null)
            {
                //Adjust frame index
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time >= curAni.getFrame(aniIndex).Time)
                {
                    aniIndex = (aniIndex + 1) % curAni.FrameCount;
                    time = 0;
                }
            }

        }

        public override void draw(SpriteBatch spriteBatch)
        {
            //If (there's no animation) draw normally
            if (IsVisible)
                if (curAni == null)
                    base.draw(spriteBatch);
                else
                    spriteBatch.Draw(curAni.Strip, this.rec, curAni.getFrame(aniIndex).Rec, this.color, 0, Vector2.Zero, flip, 0);
        }

        protected void playAnimation(Animation ani)
        {
            //Play animation if it's not already playing or null
            if (ani != null && ani != curAni)
            {

                //Scale rectangle for new animaiton
                int inDisplayWidth = Game1.View.Width;

                if (ani.Strip != null)
                {
                    rec.Width = (int)(inDisplayWidth * ScaleFactor + 0.5f);
                    float aspectRatio = (float)ani.avFrameWidth / ani.avFrameHeight;
                    rec.Height = (int)(rec.Width / aspectRatio + 0.5f);
                }
                
                //Play
                curAni = ani;
                time = 0f;
                aniIndex = 0;
            }
        }


    }
}
