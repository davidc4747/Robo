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
        Floor[] Floors { get; }
    }

    public abstract class Stage : IStage
    {

        public Color Color { get; protected set; }
        public Texture2D Background { get; protected set; }
        public Rectangle Rec { get; protected set; }
        
        protected List<Floor> floors;
        protected List<Item> items;
        protected List<Object> objs;
        protected List<Enemy> enemies;

        private static List<Message> messages;

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

        public Floor[] Floors
        {
            get { return floors.ToArray(); }
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
            Rec = new Rectangle(0, 0, Game1.View.Width*10, Game1.View.Height*10);
            Color = Color.Black;

            floors = new List<Floor>();
            items = new List<Item>();
            objs = new List<Object>();
            enemies = new List<Enemy>();

            messages = new List<Message>();

            Character = new Character();

            generateDungeon();
        }

        public virtual void update(GameTime gameTime)
        {
            //update or remove messages
            int i = 0;
            while (i < messages.Count)
            {
                if (!messages[i].IsDead)
                    messages[i].update(gameTime);
                else
                {
                    messages.RemoveAt(i);
                    i--;
                }
                i += 1;
            }

            //update or remove Objects
            i = 0;
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
                    if (enemies[i].Drop != null)
                        items.Add(enemies[i].Drop);
                    Character.kill(enemies[i]);
                    enemies.RemoveAt(i);
                    i--;
                }
                i += 1;
            }

            spawner(gameTime);
            Character.update(gameTime, this);
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if (Background != null)
                //spriteBatch.Draw(Background, Rec, Color.Purple);

            foreach (Floor floor in floors)
                floor.draw(spriteBatch);

            foreach (Object obj in Objects)
                obj.draw(spriteBatch);

            foreach (Item item in items)
                item.draw(spriteBatch);

            foreach (Enemy ene in Enemies)
                ene.draw(spriteBatch);

            foreach (Message mess in messages)
                mess.draw(spriteBatch);   

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
        
        private void generateDungeon()
        {
            List<Floor> corridors = new List<Floor>();
            floors.Add(new Room(this));
            for (int i = 1; i < 20; i++)
            {
                bool isIntersecting;
                Room room;
                do
                {
                    room = new Room(this);
                    isIntersecting = false;
                    foreach (Floor floor in floors)
                        if (room.isColliding(floor))
                            isIntersecting = true;
                }
                while (isIntersecting);

                floors.Add(room);
                corridors.Add(new Corridor(this, floors[i - 1], floors[i]));
            }

            floors.AddRange(corridors);
        }

        public static void showMessage(Vector2 pos, string mess, Color inColor)
        {
            messages.Add(new Message(pos, mess, inColor));
        }

        public static void showMessage(Vector2 pos, string mess)
        {
            messages.Add(new Message(pos, mess, Color.White));
        }
    }
}
