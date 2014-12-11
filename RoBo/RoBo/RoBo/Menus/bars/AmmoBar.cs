using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class AmmoBar : Progressbar
    {
        public AmmoBar(Character character)
            : base(character, Image.Hud.AmmoBar, 0.07f, Vector2.Zero, new Color(64, 64, 64))
        {
            offset = new Vector2(character.Rec.Width * 0.23f, -character.Rec.Height * 0.5f);//TODO: Set position of progressBar properly
        }

        public override void update(GameTime gameTime, IStage stage)
        {
            //Set Percent
            Percent = character.CurGun.CurrMag / (float)character.CurGun.MagSize;

            //scale cropRec
            cropRec.Width = texture.Width;
            cropRec.Y = (int)(texture.Height * (1 - Percent));
            cropRec.Height = (int)(texture.Height * Percent);

            cropDisplace.Y = rec.Height * (1 - Percent);

            base.update(gameTime, stage);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
}
