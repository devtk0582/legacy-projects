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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        MyBall player;
        List<BallBase> ballList = new List<BallBase>();

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

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            player = new MyBall(Game.Content.Load<Texture2D>("AnimatedBall"),
                Vector2.Zero, new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), new Vector2(4, 4));

            ballList.Add(new MovingBall(Game.Content.Load<Texture2D>("circle1"), new Vector2(150, 150), new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), Vector2.Zero));
            ballList.Add(new MovingBall(Game.Content.Load<Texture2D>("circle2"), new Vector2(300, 150), new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), Vector2.Zero));
            ballList.Add(new MovingBall(Game.Content.Load<Texture2D>("circle3"), new Vector2(150, 300), new Point(75, 75), 0, new Point(0, 0), new Point(4, 4), Vector2.Zero));

            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, Game.Window.ClientBounds);

            foreach (MovingBall ball in ballList)
            {
                ball.Update(gameTime, Game.Window.ClientBounds);

                if (ball.collisionRect.Intersects(player.collisionRect))
                    Game.Exit();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            player.Draw(gameTime, spriteBatch);

            foreach (MovingBall ball in ballList)
            {
                ball.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
