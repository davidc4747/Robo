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
        MiniMap map;
        ExpBar expBar;

        public Hud(IStage stage)
        {
            map = new MiniMap(stage);
            expBar = new ExpBar(stage.Character);
        }

        public void update(GameTime gameTime)
        {
            map.update(gameTime);
            expBar.update(gameTime);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            map.draw(spriteBatch);
            expBar.draw(spriteBatch);
        }
    }
}
