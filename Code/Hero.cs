using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Project
{
    class Hero
    {
        Vector2 Pos;
        Color color = Color.White;
        Point Size;
        public static SpriteBatch SpriteBatch { get; set; }
        public double Speed { get; set; } = 6.75;
        public static Texture2D Texture2D { get; set; }
        public static Texture2D RunHeroRight { get; set; }
        public static Texture2D RunHeroRight2 { get; set; }
        public static Texture2D RunHeroLeft { get; set; }
        public bool Hidden { get { return Pos.Y == -500; } }
        public bool Stand { get { return (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
                    || (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A)); } }
        private bool isRunningRight = false;
        private bool isRunningLeft = false;
        private DateTime lastHeroMovedTime = DateTime.MinValue;
        public Hero(Vector2 pos)
        {
            this.Pos = pos;
        }
        public Vector2 GetPosForFire => new Vector2(Pos.X + 10, Pos.Y + 95);

        public void Right()
        {
            if (this.Pos.X < Program.Width - 100)
            {
                if (Program.Hero.Stand == false)
                {
                    this.Pos.X += (float)Speed;
                    isRunningRight = true;
                    lastHeroMovedTime = DateTime.Now;
                }
                else if (Program.Hero.Stand)
                {
                    isRunningRight = false;
                    isRunningLeft = false;
                }
            }
        }

        public void PosForNextLevel()
        {
            PosForReset();
        }

        public void PosForReset()
        {
            this.Pos.X = 0; this.Pos.Y = 650;
        }

        public void Left()
        {
            if (this.Pos.X > 0)
            {
                if (Program.Hero.Stand == false)
                {
                    this.Pos.X -= (float)Speed;
                    isRunningLeft = true;
                    lastHeroMovedTime = DateTime.Now;
                }
                else if (Program.Hero.Stand)
                {
                    isRunningRight = false;
                    isRunningLeft = false;
                }
            }
        }
        public bool IsDead()
        {
            return (DateTime.Now - lastHeroMovedTime).TotalSeconds < 1;
        }

        public void Death()
        {
            this.Pos.Y = -500;
        }

        public bool IsIntersect(Rectangle rectangle)
        {
            return rectangle.Intersects(new Rectangle((int)Pos.X, (int)Pos.Y, Size.X, Size.Y));
        }

        public void Draw(GameTime gameTime)
        {
            if (isRunningRight)
            {
                Program.SpriteBatch.Draw(RunHeroRight2, Pos, color);
                isRunningRight = false;
                isRunningLeft = false;
            }
            else if (isRunningLeft)
            {
                Program.SpriteBatch.Draw(RunHeroLeft, Pos, color);
                isRunningRight = false;
                isRunningLeft = false;
            }
            else
                Program.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }
}
