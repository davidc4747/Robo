using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Floor : BaseSprite
    {
        protected static Random rand = new Random();

        public Floor(IStage stage)
            :base(stage.Background, 0.0f, Vector2.Zero)
        {
            //generate ground images
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            //spriteBatch.Draw(this.Texture, this.Rec, this.Rec, this.color, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
