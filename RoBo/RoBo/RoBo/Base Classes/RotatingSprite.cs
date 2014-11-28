using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class RotatingSprite : BaseSprite
    {
        public float Rotation
        {
            get;
            protected set;
        }

        protected Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
        }

        public RotatingSprite(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos)
            : base(texture, scaleFactor, secondsToCrossScreen, startPos)
        {
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            Rotation = 0f;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
                spriteBatch.Draw(texture, rec, null, color, Rotation, origin, SpriteEffects.None, 0f);

            foreach (Vector2 corner in getCorners(this))
                spriteBatch.Draw(Image.Particle, new Rectangle((int)corner.X, (int)corner.Y, 1, 1), color);
        }

        public bool isColliding(Rectangle inRec, float rotation, RotatingSprite obj)
        {
            //Get Rotated Corners
            Vector2[] targetCorners = getCorners(inRec, rotation);
            Vector2[] myCorners = getCorners(obj);

            //Get all axis
            List<Vector2> checkedAxis = new List<Vector2>();

            List<Vector2> axiss = getAxis(myCorners);
            axiss.AddRange(getAxis(targetCorners));

            foreach (Vector2 axis in axiss)
            {
                if (!checkedAxis.Contains(axis))
                {
                    //index: 0 = min, 1 = Max
                    float[] box1 = getMinMaxDP(myCorners, axis);
                    float[] box2 = getMinMaxDP(targetCorners, axis);

                    //Check for overlap
                    if (box1[1] < box2[0] || box1[0] > box2[1])
                        return false;

                    checkedAxis.Add(axis);
                }
            }
            return true;
        }

        public bool isColliding(RotatingSprite obj)
        {
            //Get Rotated Corners
            Vector2[] targetCorners = getCorners(obj);
            Vector2[] myCorners = getCorners(this);
            
            //Get all axis
            List<Vector2> checkedAxis = new List<Vector2>();

            List<Vector2> axiss = getAxis(myCorners);
            axiss.AddRange(getAxis(targetCorners));

            foreach (Vector2 axis in axiss)
            {
                if (!checkedAxis.Contains(axis))
                {
                    //index: 0 = min, 1 = Max
                    float[] box1 = getMinMaxDP(myCorners, axis);
                    float[] box2 = getMinMaxDP(targetCorners, axis);

                    //Check for overlap
                    if (box1[1] < box2[0] || box1[0] > box2[1])                    
                        return false;
                    
                    checkedAxis.Add(axis);
                }
            }
            return true;
        }

        //Gets the Min and Max of a group of points on a specified axis
        //index: 0 = min, 1 = Max
        private float[] getMinMaxDP(Vector2[] points, Vector2 axis)
        {
            float[] minMax = new float[2];
            minMax[0] = minMax[1] = Vector2.Dot(points[0], axis);
            for (int i = 1; i < points.Length; i++)
            {
                float dotProduct = Vector2.Dot(points[i], axis);
                if (dotProduct < minMax[0])
                    minMax[0] = dotProduct;
                if (dotProduct > minMax[1])
                    minMax[1] = dotProduct;
            }
            return minMax;
        }
        
        //Returns a list of each rotational axis that needs to be checked
        private List<Vector2> getAxis(Vector2[] points)
        {
            List<Vector2> axis = new List<Vector2>();
            for (int i = 0; i < 2; i++)
            {
                Vector2 normL = points[(i + 1) % points.Length] - points[i];//nextPoint - curPoint
                normL = new Vector2(-normL.Y, normL.X);//left normal
                normL.Normalize();
                axis.Add(normL);
            }
            return axis;
        }

        private Vector2[] getCorners(RotatingSprite obj)
        {
            Vector2[] corners = new Vector2[4];

            //Get the Corners of the object's Rectangle
            int halfWidth, halfHeight;
            halfWidth = obj.Rec.Width / 2;
            halfHeight = obj.Rec.Height / 2;
            corners[0] = new Vector2(obj.Rec.X - halfWidth, obj.Rec.Y - halfHeight);
            corners[1] = new Vector2(obj.Rec.X + halfWidth, obj.Rec.Y - halfHeight);
            corners[2] = new Vector2(obj.Rec.X + halfWidth, obj.Rec.Y + halfHeight);
            corners[3] = new Vector2(obj.Rec.X - halfWidth, obj.Rec.Y + halfHeight);

            //Rotates the corners to their accurate position
            Vector2[] rotatedCorners = (Vector2[])corners.Clone();
            Matrix myRotationMatrix = Matrix.CreateRotationZ(obj.Rotation);
            for (int i = 0; i < rotatedCorners.Length; i++)
            {
                Vector2 rotatedVector = Vector2.Transform(rotatedCorners[i] - new Vector2(obj.Rec.X, obj.Rec.Y), myRotationMatrix);
                rotatedCorners[i] = rotatedVector + new Vector2(obj.Rec.X, obj.Rec.Y);
            }

            return rotatedCorners;
        }

        private Vector2[] getCorners(Rectangle inRec, float rotation)
        {
            Vector2[] corners = new Vector2[4];

            //Get the Corners of the object's Rectangle
            int halfWidth, halfHeight;
            halfWidth = inRec.Width / 2;
            halfHeight = inRec.Height / 2;
            corners[0] = new Vector2(inRec.X - halfWidth, inRec.Y - halfHeight);
            corners[1] = new Vector2(inRec.X + halfWidth, inRec.Y - halfHeight);
            corners[2] = new Vector2(inRec.X + halfWidth, inRec.Y + halfHeight);
            corners[3] = new Vector2(inRec.X - halfWidth, inRec.Y + halfHeight);

            //Rotates the corners to their accurate position
            Vector2[] rotatedCorners = (Vector2[])corners.Clone();
            Matrix myRotationMatrix = Matrix.CreateRotationZ(rotation);
            for (int i = 0; i < rotatedCorners.Length; i++)
            {
                Vector2 rotatedVector = Vector2.Transform(rotatedCorners[i] - new Vector2(inRec.X, inRec.Y), myRotationMatrix);
                rotatedCorners[i] = rotatedVector + new Vector2(inRec.X, inRec.Y);
            }

            return rotatedCorners;
        }

    }
}
