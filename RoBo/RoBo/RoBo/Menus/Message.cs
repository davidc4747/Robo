using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Message
    {
        Vector2 position;
        Color color;
        string mess;
        
        const float SEC_TO_CROSS_SCREEN = 5.0f;
        int speed;

        int alpha = 256;

        public bool IsDead
        {
            get;
            private set;
        }

        public Message(Vector2 pos, string mess, Color color)
        {
            this.position = pos;
            this.mess = mess;
            this.color = color;
            speed = (int)(Game1.View.Width / (SEC_TO_CROSS_SCREEN * 60) + 0.5f);
        }

        public Message(Vector2 pos, string mess)
        {
            this.position = pos;
            this.mess = mess;
            this.color = Color.White;
            speed = (int)(Game1.View.Width / (SEC_TO_CROSS_SCREEN * 60) + 0.5f);
        }

        public void update(GameTime gameTime)
        {
            if (!IsDead)
            {
                alpha -= 9;
                color = new Color(256, 256, 256, alpha);
                position += new Vector2(0, -0.35f) * speed;
                IsDead = alpha <= 0;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 size = Fonts.Normal.MeasureString(mess) / 2f;
            spriteBatch.DrawString(Fonts.Normal, mess, position - size, color);
        }
    }
}
