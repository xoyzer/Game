using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project
{
    static class SplashScreen
    {
        public static Texture2D Background { get; set; }
        public static Texture2D Screen { get; set; }
        public static Texture2D Skull { get; set; }
        static int timeCounter = 0;
        static Color color;
        public static SpriteFont Font { get; set; }
        public static SpriteFont SecondFont { get; set; }
        public static Texture2D Down { get; set; }

        public static void DrawMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Screen, new Rectangle(0, 0, 1700, 800), color);
            spriteBatch.DrawString(Font, "Нажмите \"enter\", чтобы продолжить", new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(SecondFont, "Цель игры - убить максимальное количество противников,", new Vector2(20, 100), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "до тех пор пока не убьют вашего героя.", new Vector2(85, 130), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "Двигаться можно с помощью клавиш \"A\", \"D\"" +
                " или \"стрелочек\".", new Vector2(10, 160), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "Чтобы уклоняться от вражеских выстрелов, необходимо в момент", new Vector2(10, 200), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "попадания снаряда в героя вовремя нажать клавишу движения", new Vector2(10, 230), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "Стрелять можно клавишей \"Space\".", new Vector2(120, 260), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "При достижении конца тоннеля, нажмите клавишу \"Enter\",", new Vector2(10, 300), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "чтобы перейти на следующий уровень, в котором", new Vector2(35, 330), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "количество противников увеличится на один.", new Vector2(50, 360), Color.Turquoise);
            spriteBatch.DrawString(SecondFont, "Хорошей игры!", new Vector2(200, 390), Color.Turquoise);
        }
        public static void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, 1700, 800), color);
            spriteBatch.DrawString(SecondFont, "Нажмите \"Escape\", чтобы перейти в меню", new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(SecondFont, "Нажмите \"R\" для перезапуска", new Vector2(0, 22), Color.White);
            spriteBatch.DrawString(Font, $"{ Program.GiveKillsCount() }", new Vector2(850, 0), color);
            spriteBatch.Draw(Skull, new Rectangle(790, 0, 200, 200), Color.Black);
        }

        public static void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 110);
            timeCounter++;
            //timeCounter%255
        }
    }
}
