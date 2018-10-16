using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MyGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteManager spriteManager;
        Gun shootGun;
        Ball smallBall, mediumBall, bigBall, humanBall;
        SpriteFont font;
        GameState gameState;
        Texture2D startBG, gameBG, gameOverBG, gameWinBG;

        Rectangle backgroundRect;
        MouseState previousMouseState;
        Vector2 newPosition, titleVector, timeVector;
        Texture2D animatedBall;

        Point frameSize = new Point(75, 75);
        Point currentFrame = new Point(0, 0);
        Point sheetSize = new Point(4, 4);

        int humanBallHideTime = 0;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = new GameState(GameStage.GameStart, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            gameState.ShootCount = 20;
            previousMouseState = Mouse.GetState();
            humanBall = new Ball(new Vector2(400, 100), 75, 75, 1.0f, 1.0f);
            humanBall.Rect = new Rectangle((int)(humanBall.Position.X), (int)humanBall.Position.Y, 75, 75);
            smallBall = new Ball(new Vector2(50, 60), 0, 0, 4.0f, 4.0f);
            mediumBall = new Ball(new Vector2(250,200), 0, 0, 2.0f, 2.0f);
            bigBall = new Ball(new Vector2(500, 200), 0, 0, 1.0f, 1.0f);
            shootGun = new Gun(new Vector2(gameState.DisplayWidth / 2, gameState.DisplayHeight / 2), 0, 0, GunSize.Small, 6.0f);
            shootGun.UpdateTarget();
            backgroundRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            titleVector = new Vector2(20, 30);
            timeVector = new Vector2(GraphicsDevice.Viewport.Width - 100, 30);

            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startBG = this.Content.Load<Texture2D>("GameStartBG");
            gameState.Background = startBG;
            gameBG = this.Content.Load<Texture2D>("GameBG");
            gameOverBG = this.Content.Load<Texture2D>("GameOver");
            gameWinBG = this.Content.Load<Texture2D>("GameWin");
            smallBall.LoadTexture(this.Content.Load<Texture2D>("circle1"));
            mediumBall.LoadTexture(this.Content.Load<Texture2D>("circle2"));
            bigBall.LoadTexture(this.Content.Load<Texture2D>("circle3"));

            shootGun.LoadTexture(this.Content.Load<Texture2D>("shoot"));
            animatedBall = this.Content.Load<Texture2D>("AnimatedBall");
            font = this.Content.Load<SpriteFont>("MySpriteFont");
            gameState.Music = Content.Load<Song>("Till I Collapse");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

           

            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.Escape)) this.Exit();

            if (keys.IsKeyDown(Keys.Home))
            {
                gameState.CurrentStage = GameStage.GameStart;
                gameState.Background = startBG;
            }

            if (gameState.ShootCount == 0)
            {
                gameState.CurrentStage = GameStage.GameOver;
                gameState.Background = gameOverBG;
            }
            else if (gameState.Score >= 250)
            {
                gameState.CurrentStage = GameStage.GameWin;
                gameState.Background = gameWinBG;
            }

            

            if (gameState.CurrentStage == GameStage.GameStart && keys.IsKeyDown(Keys.Space)) 
            {
                gameState.CurrentStage = GameStage.InGame;
                gameState.Background = gameBG;
                gameState.Score = 0;
                gameState.ShootCount = 20;
            }

            if (gameState.CurrentStage == GameStage.GameOver && keys.IsKeyDown(Keys.Enter))
            {
                gameState.CurrentStage = GameStage.InGame;
                gameState.Background = gameBG;
                gameState.Score = 0;
                gameState.ShootCount = 20;
            }

            if (gameState.CurrentStage == GameStage.GameWin && keys.IsKeyDown(Keys.Enter))
            {
                gameState.CurrentStage = GameStage.InGame;
                gameState.Background = gameBG;
                gameState.Score = 0;
                gameState.ShootCount = 20;
            }

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(gameState.Music);
            }
            
            if(keys.IsKeyDown(Keys.P) == true)
            {
                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Resume();
                }
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
            }

            if (gameState.CurrentStage == GameStage.InGame)
            {
                gameState.Background = gameBG;
                MouseState mouse = Mouse.GetState();

                if (mouse.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    gameState.ShootCount--;
                    //if (gameState.ShootCount > 10) gameState.CurrentStage = GameStage.GameOver;

                    if (shootGun.TargetRect.Intersects(mediumBall.Rect))
                    {
                        gameState.Score += 10;
                        mediumBall.Position = new Vector2((new Random()).Next(0, Window.ClientBounds.Width - mediumBall.Rect.Width), (new Random()).Next(0, Window.ClientBounds.Height - mediumBall.Rect.Height));
                        mediumBall.XSpeed = (float)(new Random()).NextDouble() * 4.0f - 2.0f;
                        mediumBall.YSpeed = (float)(new Random()).NextDouble() * 4.0f + 2.0f;
                    }
                    else if (shootGun.TargetRect.Intersects(bigBall.Rect))
                    {
                        gameState.Score += 5;
                        bigBall.Position = new Vector2((new Random()).Next(0, Window.ClientBounds.Width - bigBall.Rect.Width),(new Random()).Next(0, Window.ClientBounds.Height - bigBall.Rect.Height));
                        bigBall.XSpeed = (float)(new Random()).NextDouble() * 2.0f - 1.0f;
                        bigBall.YSpeed = (float)(new Random()).NextDouble() * 2.0f + 1.0f;
                    }
                    else if (shootGun.TargetRect.Intersects(smallBall.Rect))
                    {
                        gameState.Score += 20;
                        smallBall.Position = new Vector2((new Random()).Next(0, Window.ClientBounds.Width - smallBall.Rect.Width), (new Random()).Next(0, Window.ClientBounds.Height - smallBall.Rect.Height));
                        smallBall.XSpeed = (float)(new Random()).NextDouble() * 8.0f - 4.0f;
                        smallBall.YSpeed = (float)(new Random()).NextDouble() * 8.0f + 4.0f;
                    }
                    else if (shootGun.TargetRect.Intersects(humanBall.Rect))
                    {
                        gameState.Score += 12;
                        humanBallHideTime = 120;
                        humanBall.Position = new Vector2((new Random()).Next(0, Window.ClientBounds.Width - humanBall.Rect.Width), (new Random()).Next(0, Window.ClientBounds.Height - humanBall.Rect.Height));
                        humanBall.XSpeed = (float)(new Random()).NextDouble() * 2.0f - 1.0f;
                        humanBall.YSpeed = (float)(new Random()).NextDouble() * 2.0f + 1.0f;
                    }
                }


                if (mouse.X != previousMouseState.X || mouse.Y != previousMouseState.Y)
                {
                    shootGun.Position = new Vector2(mouse.X, mouse.Y);
                    shootGun.Center = new Vector2(mouse.X + shootGun.Width / 2, mouse.Y + shootGun.Height / 2);
                    
                }
                previousMouseState = mouse;

                if (keys.IsKeyDown(Keys.Up))
                    shootGun.Position -= new Vector2(0, shootGun.MoveSpeed);

                if (keys.IsKeyDown(Keys.Down))
                    shootGun.Position += new Vector2(0, shootGun.MoveSpeed);

                if (keys.IsKeyDown(Keys.Left))
                    shootGun.Position -= new Vector2(shootGun.MoveSpeed, 0);

                if (keys.IsKeyDown(Keys.Right))
                    shootGun.Position += new Vector2(shootGun.MoveSpeed, 0);

                if (shootGun.Position.X < 0)
                    shootGun.Position = new Vector2(0, shootGun.Position.Y);

                if (shootGun.Position.Y < 0)
                    shootGun.Position = new Vector2(shootGun.Position.X, 0);

                if (shootGun.Position.X > Window.ClientBounds.Width - shootGun.Width)
                    shootGun.Position = new Vector2(Window.ClientBounds.Width - shootGun.Width, shootGun.Position.Y);

                if (shootGun.Position.Y > Window.ClientBounds.Height - shootGun.Height)
                    shootGun.Position = new Vector2(shootGun.Position.X, Window.ClientBounds.Height - shootGun.Height);


                if (smallBall.Position.X + smallBall.Width >= gameState.DisplayWidth || smallBall.Position.X <= 0)
                    smallBall.XSpeed *= -1;

                if (smallBall.Position.Y + smallBall.Height >= gameState.DisplayHeight || smallBall.Position.Y <= 0)
                    smallBall.YSpeed *= -1;

                smallBall.Position += new Vector2(smallBall.XSpeed, smallBall.YSpeed);
                smallBall.Rect = new Rectangle((int)(smallBall.Position.X + 0.5f), (int)(smallBall.Position.Y + 0.5f), smallBall.Width, smallBall.Height);

                if (mediumBall.Position.X + mediumBall.Width >= gameState.DisplayWidth || mediumBall.Position.X <= 0)
                    mediumBall.XSpeed *= -1;

                if (mediumBall.Position.Y + mediumBall.Height >= gameState.DisplayHeight || mediumBall.Position.Y <= 0)
                    mediumBall.YSpeed *= -1;

                mediumBall.Position += new Vector2(mediumBall.XSpeed, mediumBall.YSpeed);
                mediumBall.Rect = new Rectangle((int)(mediumBall.Position.X + 0.5f), (int)(mediumBall.Position.Y + 0.5f), mediumBall.Width, mediumBall.Height);
                

                if (bigBall.Position.X + bigBall.Width >= gameState.DisplayWidth || bigBall.Position.X <= 0)
                    bigBall.XSpeed *= -1;

                if (bigBall.Position.Y + bigBall.Height >= gameState.DisplayHeight || bigBall.Position.Y <= 0)
                    bigBall.YSpeed *= -1;

                bigBall.Position += new Vector2(bigBall.XSpeed, bigBall.YSpeed);
                bigBall.Rect = new Rectangle((int)(bigBall.Position.X + 0.5f), (int)(bigBall.Position.Y + 0.5f), bigBall.Width, bigBall.Height);

                if (humanBall.Position.X + humanBall.Width >= gameState.DisplayWidth || humanBall.Position.X <= 0)
                    humanBall.XSpeed *= -1;

                if (humanBall.Position.Y + humanBall.Height >= gameState.DisplayHeight || humanBall.Position.Y <= 0)
                    humanBall.YSpeed *= -1;

                humanBall.Position += new Vector2(humanBall.XSpeed, humanBall.YSpeed);
                humanBall.Rect = new Rectangle((int)(humanBall.Position.X + 0.5f), (int)(humanBall.Position.Y + 0.5f), humanBall.Width, humanBall.Height);

                shootGun.Rect = new Rectangle((int)(shootGun.Position.X + 0.5f), (int)(shootGun.Position.Y + 0.5f), shootGun.Width, shootGun.Height);
                shootGun.UpdateTarget();

                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }

                if (humanBallHideTime > 0)
                {
                    humanBallHideTime--;
                }


            }

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //bgColor = new Color(redIntensity, greenIntensity, blueIntensity);
            GraphicsDevice.Clear(Color.LightBlue);

            if (gameState.CurrentStage == GameStage.GameStart)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(gameState.Background, backgroundRect, Color.White);
                spriteBatch.DrawString(font, "My Game", titleVector, Color.Green);
                spriteBatch.DrawString(font, String.Format("{0:hh:mm:ss}", DateTime.Now), timeVector, Color.Orange);
                
                spriteBatch.End();
            }
            else if (gameState.CurrentStage == GameStage.InGame)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                spriteBatch.Draw(gameState.Background, Vector2.Zero, backgroundRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "My Game", titleVector, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                spriteBatch.DrawString(font, String.Format("{0:hh:mm:ss}", DateTime.Now), timeVector, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);

                spriteBatch.DrawString(font, String.Format("Shoot Count: {0}", gameState.ShootCount.ToString()), timeVector + new Vector2(-100, 10), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, String.Format("Score: {0}", gameState.Score.ToString()), timeVector + new Vector2(-100, 30), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
                spriteBatch.Draw(smallBall.Texture, smallBall.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                spriteBatch.Draw(mediumBall.Texture, mediumBall.Position, null,  Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                spriteBatch.Draw(bigBall.Texture, bigBall.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
                spriteBatch.Draw(shootGun.Texture, shootGun.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1); 
                spriteBatch.End();

                if (humanBallHideTime == 0)
                {
                    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    spriteBatch.Draw(animatedBall, humanBall.Position,
                    new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y),
                    Color.White, 0, Vector2.Zero,
                    1, SpriteEffects.None, 1);
                    spriteBatch.End();
                }
            }
            else if (gameState.CurrentStage == GameStage.GameOver)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(gameState.Background, backgroundRect, Color.White);
                spriteBatch.End();
            }
            else if (gameState.CurrentStage == GameStage.GameWin)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(gameState.Background, backgroundRect, Color.White);
                spriteBatch.End();
            }
            

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
