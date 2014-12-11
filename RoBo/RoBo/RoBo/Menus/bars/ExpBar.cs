using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class ExpBar
    {
        Character character;
        Rectangle rec;

        public ExpBar(Character character)
        {
            this.character = character;
        }

        public void update(GameTime gameTime)
        {
            rec.Width = (int)(Game1.View.Width * (character.Exp / (float)character.MaxExp));
            rec.Height = (int)(Game1.View.Height * 0.015f);
            rec.Y = (int)(Game1.View.Height - rec.Height);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            string mess = "" + character.Exp + "/" + character.MaxExp;
            Vector2 size = Fonts.Normal.MeasureString(mess);

            spriteBatch.DrawString(Fonts.Normal, "IQ: " + character.IQ.ToString(), new Vector2(rec.X + 8, rec.Y - size.Y), Color.White);
            spriteBatch.DrawString(Fonts.Normal, mess, new Vector2(Game1.View.Width - size.X - 8, rec.Y - size.Y), Color.White);
            spriteBatch.Draw(Image.Particle, rec, Color.Yellow);
        }


    }
}
