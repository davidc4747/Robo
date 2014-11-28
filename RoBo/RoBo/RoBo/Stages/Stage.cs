using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBo
{
    public interface IMap
    {
        Color Color { get; }
        Texture2D Background { get; }
        Rectangle Rec { get; }
        
        Character Character { get; }

        BaseSprite[] Everything { get; }
        Object[] Objects { get; }
        Enemy[] Enemies { get; }

        void update(GameTime gameTime);
        void draw(SpriteBatch spriteBatch);

    }

    public interface IStage : IMap
    {
        Item[] Items { get; }
    }

    public abstract class Stage : IStage
    {

        public Color Color { get; protected set; }
        public Texture2D Background { get; protected set; }
        public Rectangle Rec { get; protected set; }

        protected List<Item> items;
        protected List<Object> objs;
        protected List<Enemy> enemies;

        public Character Character
        {
            get;
            protected set;
        }

        public BaseSprite[] Everything
        {
            get 
            {
                List<RotatingSprite> everything = new List<RotatingSprite>();
                everything.AddRange(objs);
                everything.AddRange(enemies);
                return everything.ToArray(); 
            }
        }

        public Item[] Items
        {
            get { return items.ToArray(); }
        }

        public Object[] Objects
        {
            get { return objs.ToArray(); }
        }

        public Enemy[] Enemies
        {
            get { return enemies.ToArray(); }
        }

        public Stage()
        {
            Background = Image.Particle;
            Rec = new Rectangle(0, 0, Game1.View.Width*2, Game1.View.Height*2);
            Color = Color.White;

            items = new List<Item>();
            objs = new List<Object>();
            enemies = new List<Enemy>();

            Character = new Character();
        }

        public virtual void update(GameTime gameTime)
        {
            //update or remove Objects
            int i = 0;
            while (i < objs.Count)
            {
                if (!objs[i].IsDead)
                    objs[i].update(gameTime, this);
                else
                {
                    objs.RemoveAt(i);
                    i--;
                }
                i += 1;
            }

            //update or remove Items
            i = 0;
            while (i < items.Count)
            {
                if (!items[i].IsDead)
                    items[i].update(gameTime, this);
                else
                {
                    items.RemoveAt(i);
                    i--;
                }
                i += 1;
            }

            //update or remove Enemies
            i = 0;
            while (i < enemies.Count)
            {
                if (!enemies[i].IsDead)
                    enemies[i].update(gameTime, this);
                else
                {
                    items.Add(enemies[i].Drop);
                    enemies.RemoveAt(i);
                    i--;
                }
                i += 1;
            }

            //spawner(gameTime);
            Character.update(gameTime, this);
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
                spriteBatch.Draw(Background, Rec, new Color(0,0,0));

            foreach (Object obj in Objects)
                obj.draw(spriteBatch);

            foreach (Item item in items)
                item.draw(spriteBatch);

            foreach (Enemy ene in Enemies)
                ene.draw(spriteBatch);            

            Character.draw(spriteBatch);
        }


        Random rand;
        float spawnTimer;
        protected void spawner(GameTime gameTime)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer >= 3)
            {
                rand = new Random();
                enemies.Add(new Zombie(new Vector2(rand.Next(Rec.Width), rand.Next(Rec.Height))));
                spawnTimer = 0;
            }
        }


    }
}
