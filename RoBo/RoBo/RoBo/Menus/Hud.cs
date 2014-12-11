using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Hud
    {
        Character character;

        public Hud(Character character)
        {
            this.character = character;
        }

        public void update(GameTime gameTime)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Fonts.Normal, character.Exp.ToString(), character.Position + new Vector2(-100, 0), Color.Green);
        }
    }
}
