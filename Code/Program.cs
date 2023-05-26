using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project
{
    class Program
    {
        public static int Width, Height;
        public static int KillsCount;
        public static int EnemyCount = 3;
        public static Random rnd = new Random();
        public static SpriteBatch SpriteBatch { get; set; }
        public static Hero Hero { get; set; }
        public static Enemy Enemy { get; set; }
        static List<Fire> heroFires = new List<Fire>();
        static List<Fire> enemyFires = new List<Fire>();
        static List <Enemy> enemies = new List<Enemy>();
        static List<Vector2> enemiesPos = new List<Vector2>();
        public static DateTime LastKeyPressTime = DateTime.MinValue;
        public static void Init(SpriteBatch SpriteBatch, int Width, int Height)
        {
            Program.SpriteBatch = SpriteBatch;
            Program.Width = Width;
            Program.Height = Height;
            Hero = new Hero(new Vector2(0, 650)); 
            var indent = 0;
            for (int i = 0; i < EnemyCount; i++)
            {
                enemies.Add(new Enemy(new Vector2(500 + indent, 635)));
                indent += rnd.Next(75, 250);
            }
        }

        public static void HeroFire()
        {
            if (Hero.Stand)
                heroFires.Add(new Fire(Hero.GetPosForFire));
        }

        public static void EnemyFire(GameTime gameTime)
        {
            GetEnemyPos();
            const int fireDelay = 1000;
            var currentTime = (int)gameTime.TotalGameTime.TotalMilliseconds;
            foreach (Enemy enemy in enemies)
            {
                if (currentTime - enemy.LastFireTime > fireDelay)
                {
                    foreach (var pos in enemiesPos)
                    {
                        if (Hero.Hidden) break;
                        enemyFires.Add(new Fire(pos, 0));
                    }
                    enemy.LastFireTime = currentTime;
                    if (Hero.Hidden) break;
                }
            }
        }

        public static void GetEnemyPos()
        {
            foreach (var enemy in enemies)
            {
                enemiesPos.Add(enemy.GetPosForFire);
            }
        }

        public static void UpdateHeroFires()
        {
            for (int i = 0; i < heroFires.Count; i++)
            {
                heroFires[i].Update();
                Enemy enemyKill = heroFires[i].EnemyIntersect(enemies);
                if (enemyKill != null)
                {
                    enemies.Remove(enemyKill);
                    KillsCount++;
                    GiveKillsCount();
                    heroFires.RemoveAt(i);
                    i--;
                    continue;
                }
                if (heroFires[i].Hidden)
                {
                    heroFires.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void UpdateEnemyFires()
        {
            for (int i = 0; i < enemyFires.Count; i++)
            {
                enemyFires[i].UpdateEnemyShots();
                Hero heroKill = enemyFires[i].HeroIntersect(Hero);
                
                if (heroKill != null && (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A)
                    || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right)))
                {
                    if (Hero.IsDead())
                    {
                        enemyFires.RemoveAt(i);
                        i--;
                        LastKeyPressTime = DateTime.Now;
                        continue;
                    }
                    else
                    {
                        enemyFires.RemoveAt(i);
                        Hero.Death();
                        i--;
                        continue;
                    }
                }
                else if (heroKill != null)
                {
                    enemyFires.RemoveAt(i);
                    Hero.Death();
                    i--;
                    continue;
                }
                if (enemyFires[i].Hidden)
                {
                    enemyFires.RemoveAt(i);
                    i--;
                }
            }
        }

        public static int GiveKillsCount()
        {
            return KillsCount;
        }

        public static void Draw(GameTime gameTime)
        {
            foreach (Fire fire in heroFires) fire.Draw();
            foreach (Fire fire in enemyFires) fire.Draw();
            foreach (var enemy in enemies) enemy.Draw();
            Hero.Draw(gameTime);
        }
        public static void Reset(GameTime gameTime)
        {
            KillsCount = 0;
            heroFires.Clear();
            enemyFires.Clear();
            enemies.Clear();
            enemiesPos.Clear();
            EnemyCount = 3;
            Hero.PosForReset();
            enemiesPos.Clear();
            foreach (var enemy in enemies)
                enemy.LastFireTime = 500;
            var indent = 0;
            for (int i = 0; i < EnemyCount; i++)
            {
                enemies.Add(new Enemy(new Vector2(500 + indent, 635)));
                indent += rnd.Next(75, 250);
            }
            gameTime.TotalGameTime = TimeSpan.Zero;
        }

        public static void ResetNextLevel(GameTime gameTime)
        {
            if (Hero.GetPosForFire.X >= Width - 150)
            {
                Hero.PosForNextLevel();
                foreach (var enemy in enemies)
                    enemy.LastFireTime = 500;
                heroFires.Clear();
                enemies.Clear();
                enemiesPos.Clear();
                EnemyCount++;
                var indent = 0;
                for (int i = 0; i < EnemyCount; i++)
                {
                    enemies.Add(new Enemy(new Vector2(500 + indent, 635)));
                    indent += rnd.Next(75, 200);
                }
                gameTime.TotalGameTime = TimeSpan.FromMilliseconds(500);
            }
        }
    }
}
