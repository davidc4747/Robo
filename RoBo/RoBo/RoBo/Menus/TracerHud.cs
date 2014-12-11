using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class TracerHud
    {
        Character character;
        Progressbar healthBar, actionBar, ammoBar;
        
        public TracerHud(Character character)
        {
            this.character = character;

            healthBar = new HealthBar(character);
            ammoBar = new AmmoBar(character);
            actionBar = new ActionBar(character);
        }

        public void update(GameTime gameTime, IStage stage)
        {
            healthBar.update(gameTime, stage);
            ammoBar.update(gameTime, stage);
            actionBar.update(gameTime, stage);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            healthBar.draw(spriteBatch);
            ammoBar.draw(spriteBatch);
            actionBar.draw(spriteBatch);
        }
    }
}
