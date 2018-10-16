using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollideGame
{
    public class MyBall : BallBase
    {
        private MouseState prevMouseState;

        public Rectangle targetRect
        {
            get
            {
                return new Rectangle((int)(position.X + frameSize.X / 2 - 3 + 0.5f), (int)(position.Y + frameSize.Y / 2 - 3 + 0.5f), 6, 6);
            }
        }

        public MyBall(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, 1.0f)
        {
        }
        public MyBall(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, 1.0f, millisecondsPerFrame)
        {
        }

        public override Vector2 direction
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            

            //GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            //if (gamepadState.ThumbSticks.Left.X != 0)
            //    inputDirection.X += gamepadState.ThumbSticks.Left.X;
            //if (gamepadState.ThumbSticks.Left.Y != 0)
            //    inputDirection.Y += gamepadState.ThumbSticks.Left.Y;


            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Width - frameSize.X;

            base.Update(gameTime, clientBounds);
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, MouseState currMouseState, KeyboardState keys)
        {

            if (currMouseState.X != prevMouseState.X ||
                currMouseState.Y != prevMouseState.Y)
            {
                position = new Vector2(currMouseState.X, currMouseState.Y);
            }
            

            if (keys.IsKeyDown(Keys.Up))
                position.Y -= speed.Y;

            if (keys.IsKeyDown(Keys.Down))
                position.Y += speed.Y;

            if (keys.IsKeyDown(Keys.Left))
                position.X -= speed.X;

            if (keys.IsKeyDown(Keys.Right))
                position.X += speed.X;

            Update(gameTime, clientBounds);
        }
    }
}
