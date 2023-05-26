using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Project
{
    enum State
    {
        SplashScreen,
        Game,
        Final,
        Pause
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        State State = State.SplashScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1700;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Screen = Content.Load<Texture2D>("screen");
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.Font = Content.Load<SpriteFont>("SplashFont");
            SplashScreen.SecondFont = Content.Load<SpriteFont>("SecondFont");
            SplashScreen.Skull = Content.Load<Texture2D>("Skull");
            SplashScreen.Down = Content.Load<Texture2D>("Down");
            Program.Init(_spriteBatch, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Hero.Texture2D = Content.Load<Texture2D>("hero");
            Hero.RunHeroRight = Content.Load<Texture2D>("RunHero");
            Hero.RunHeroRight2 = Content.Load<Texture2D>("RunHero2");
            Hero.RunHeroLeft = Content.Load<Texture2D>("RunHeroBack");
            Fire.Texture2D = Content.Load<Texture2D>("shot");
            Enemy.Texture2D = Content.Load<Texture2D>("enemy");
            // TODO: use this.Content to load your game content here
        }
        KeyboardState oldKeyboardState;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            switch(State)
            {
                case State.SplashScreen:
                    //GraphicsDevice.Clear(Color.Black);
                    SplashScreen.Update();
                    if (keyboardState.IsKeyDown(Keys.Enter)) State = State.Game;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed) State = State.Game;
                    gameTime.TotalGameTime = TimeSpan.Zero;
                    break;
                case State.Game:
                    Program.UpdateHeroFires();
                    Program.UpdateEnemyFires();
                    SplashScreen.Update();
                    if (keyboardState.IsKeyDown(Keys.Escape)) State = State.SplashScreen;
                    if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) Program.Hero.Right();
                    if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) Program.Hero.Left();
                    if (keyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space)) Program.HeroFire();
                        //if (keyboardState.IsKeyDown(Keys.LeftControl) && oldKeyboardState.IsKeyUp(Keys.LeftControl)) Program.EnemyFire(gameTime);
                    Program.EnemyFire(gameTime);
                    if (keyboardState.IsKeyDown(Keys.R) && oldKeyboardState.IsKeyUp(Keys.R)) Program.Reset(gameTime);
                    if (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)) Program.ResetNextLevel(gameTime);
                    break;
            }
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape)) Exit();
            SplashScreen.Update();
            oldKeyboardState = keyboardState;
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            
            switch(State)
            {
                case State.SplashScreen:
                    SplashScreen.DrawMenu(_spriteBatch);
                    break;

                case State.Game:
                    SplashScreen.DrawBackground(_spriteBatch);
                    Program.Draw(gameTime);
                    break;
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
