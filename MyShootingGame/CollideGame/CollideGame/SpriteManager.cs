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


namespace CollideGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        MyBall player;
        List<BallBase> ballList = new List<BallBase>();
        GameState gameState;

        Texture2D startBG, gameBG, gameOverBG, gameWinBG;
        Rectangle backgroundRect;
        MouseState prevMouseState;
        Vector2 titleVector, countVector, scoreVector;

        SpriteFont font;

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            gameState = new GameState(GameStage.GameStart, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);
            gameState.ShootCount = 20;

            backgroundRect = new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height);

            titleVector = new Vector2(20, 30);
            countVector = new Vector2(Game.GraphicsDevice.Viewport.Width - 200, 30);
            scoreVector = new Vector2(Game.GraphicsDevice.Viewport.Width - 200, 60);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new MyBall(Game.Content.Load<Texture2D>("MovingShoot"),
                new Vector2(400,240), new Point(60, 60), 0, new Point(0, 0), new Point(5, 4), new Vector2(4, 4),16);

            ballList.Add(new MovingBall(Game.Content.Load<Texture2D>("AnimatedBall"), new Vector2(150, 150), new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), new Vector2(2,2), 16));
            ballList.Add(new MovingBall(Game.Content.Load<Texture2D>("AnimatedBall"), new Vector2(300, 150), new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), new Vector2(-2,2), 16));
            ballList.Add(new MovingBall(Game.Content.Load<Texture2D>("AnimatedBall"), new Vector2(150, 300), new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), new Vector2(-2,-2), 16));


            startBG = Game.Content.Load<Texture2D>("GameStartBG");
            gameState.Background = startBG;
            gameBG = Game.Content.Load<Texture2D>("GameBG");
            gameOverBG = Game.Content.Load<Texture2D>("GameOver");
            gameWinBG = Game.Content.Load<Texture2D>("GameWin");

            font = Game.Content.Load<SpriteFont>("MySpriteFont");
            gameState.Music = Game.Content.Load<Song>("Till I Collapse");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.Escape)) Game.Exit();

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



            if ((gameState.CurrentStage == GameStage.GameStart && keys.IsKeyDown(Keys.Space))
                || (gameState.CurrentStage == GameStage.GameOver && keys.IsKeyDown(Keys.Enter))
                || (gameState.CurrentStage == GameStage.GameWin && keys.IsKeyDown(Keys.Enter)))
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

            if (keys.IsKeyDown(Keys.P) == true)
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
                MouseState currMouseState = Mouse.GetState();
                if (currMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    gameState.ShootCount--;
                    foreach (MovingBall ball in ballList)
                    {
                        if (ball.collisionRect.Intersects(player.targetRect))
                        {
                            gameState.Score += 20;
                            ball.Update(gameTime, Game.Window.ClientBounds, true);
                        }
                    }
                }
                else
                {
                    foreach (MovingBall ball in ballList)
                    {
                        ball.Update(gameTime, Game.Window.ClientBounds, false);
                        foreach (MovingBall otherBall in ballList)
                        {
                            if (otherBall != ball && ball.collisionRect.Intersects(otherBall.collisionRect))
                            {
                                ball.ChangeDirection();
                                otherBall.ChangeDirection();
                            }
                        }
                    }
                }
                player.Update(gameTime, Game.Window.ClientBounds, currMouseState, keys);
                prevMouseState = currMouseState;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (gameState.CurrentStage == GameStage.GameStart)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(gameState.Background, backgroundRect, Color.White);
                spriteBatch.DrawString(font, "Shooting Game", titleVector , Color.Green);
                spriteBatch.End();
            }
            else if (gameState.CurrentStage == GameStage.InGame)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                spriteBatch.Draw(gameState.Background, Vector2.Zero, backgroundRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "Shooting Game", titleVector, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, String.Format("Shoot Count: {0}", gameState.ShootCount.ToString()), countVector, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(font, String.Format("Score: {0}", gameState.Score.ToString()), scoreVector, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0.5f);
                
                player.Draw(gameTime, spriteBatch);
                foreach (MovingBall ball in ballList)
                {
                    ball.Draw(gameTime, spriteBatch);
                }

                spriteBatch.End();  
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

            base.Draw(gameTime);
        }
    }
}
