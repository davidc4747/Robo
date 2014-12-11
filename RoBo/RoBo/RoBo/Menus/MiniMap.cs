using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBo
{
    public class MiniMap
    {
        private IStage stage;

        Rectangle rec;

        public MiniMap(IStage stage)
        {
            this.stage = stage;
            float scaleFactor = 0.20f;

            rec.X = (int)(Game1.View.Width * 0.03f);
            rec.Y = (int)(Game1.View.Height * 0.03f);

            rec.Width = (int)(Game1.View.Width * scaleFactor + 0.5f);
            float aspectRatio = (float)stage.Rec.Width / stage.Rec.Height;
            rec.Height = (int)(rec.Width / aspectRatio + 0.5f);
            
        }

        public void update(GameTime gametime)
        {
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image.Particle, rec, new Color(128, 128, 128, 128));

            foreach (Floor floor in stage.Floors)
            {
                spriteBatch.Draw(Image.Particle, scale(floor.Rec), Color.Black);
                if (floor.GetType() == typeof(Corridor))
                {
                    Corridor tempFloor = (Corridor)floor;
                    spriteBatch.Draw(Image.Particle, scale(tempFloor.Rec2), Color.Black);
                }
            }

            spriteBatch.Draw(Image.Particle, scale(stage.Character.Rec), Color.Yellow);
        }

        private Rectangle scale(Rectangle inRec)
        {
            Rectangle newRec = new Rectangle();
            float xFactor = (float)inRec.X / stage.Rec.Width;
            float yFactor = (float)inRec.Y / stage.Rec.Height;
            float widthFactor = (float)inRec.Width / stage.Rec.Width;
            float heightFactor = (float)inRec.Height / stage.Rec.Height;

            newRec.X = (int)(rec.Width * xFactor + 0.5f);
            newRec.Y = (int)(rec.Height * yFactor + 0.5f);

            newRec.Width = (int)(rec.Width * widthFactor + 0.5f);
            newRec.Height = (int)(rec.Height * heightFactor + 0.5f);

            newRec.Offset(rec.X, rec.Y);
            return newRec;
        }

    }
}
