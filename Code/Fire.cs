using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project
{
    class Fire
    {
        const int speed = 20;
        Vector2 Pos;
        Vector2 Dir;
        Color color = Color.White;
        public static Texture2D Texture2D { get; set; }
        public Fire(Vector2 pos)
        {
            this.Pos = new Vector2(pos.X + Hero.Texture2D.Width - 40, pos.Y - Hero.Texture2D.Height - 53);
            this.Dir = new Vector2(speed, 0);
        }
        public Fire(Vector2 pos, int v)
        {
            this.Pos = new Vector2(pos.X + Enemy.Texture2D.Width - 200, pos.Y - Enemy.Texture2D.Height + 7);
            this.Dir = new Vector2(speed, 0);
        }

        public Enemy EnemyIntersect(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
                if (enemy.IsIntersect(new Rectangle((int)Pos.X, (int)Pos.Y, Texture2D.Width, Texture2D.Height))) return enemy;
            return null;
        }
        public Hero HeroIntersect(Hero hero)
        {
            if (hero.IsIntersect(new Rectangle((int)Pos.X, (int)Pos.Y, Texture2D.Width, Texture2D.Height))) return hero;
            return null;
        }

        public bool Hidden
        {
            get { return Pos.X > Program.Width; }
        }

        public void Update()
        {
            if (Pos.X <= Program.Width)
                Pos += Dir;
        }

        public void UpdateEnemyShots()
        {
            if (Pos.X <= Program.Width)
                Pos -= Dir;
        }

        public void Draw()
        {
            Program.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }
}
