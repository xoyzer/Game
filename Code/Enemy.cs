using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    class Enemy
    {
        Vector2 Pos;
        Point Size;
        Color color = Color.White;
        public int LastFireTime { get; set; }

        public static Texture2D Texture2D { get; set; }

        public Vector2 GetPosForFire => new Vector2(Pos.X, Pos.Y+47);

        public Enemy(Vector2 pos)
        {
            this.Pos = pos;
            LastFireTime = 0;
        }
        public bool IsIntersect(Rectangle rectangle)
        {
            return rectangle.Intersects(new Rectangle((int)Pos.X+500, (int)Pos.Y, Size.X, Size.Y));
        }

        public void Draw()
        {
            Program.SpriteBatch.Draw(Texture2D, Pos, color);
        }
    }
}
