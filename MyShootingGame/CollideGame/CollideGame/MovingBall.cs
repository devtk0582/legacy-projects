using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CollideGame
{
    public class MovingBall : BallBase
    {
        public MovingBall(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, 0.5f)
        {
        }
        public MovingBall(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, 0.5f, millisecondsPerFrame)
        {
        }

        public override Vector2 direction
        {
            get { return speed; }
            set {speed = value;}
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;
            base.Update(gameTime, clientBounds);
        }

        public void Update(GameTime gameTime, Rectangle clientBounds, bool IsStart)
        {
            if (IsStart)
            {
                position = new Vector2((new Random()).Next(0, clientBounds.Width - collisionRect.Width), (new Random()).Next(0, clientBounds.Height - collisionRect.Height));
                direction = new Vector2((float)(new Random()).NextDouble() * 2.0f - 1.0f, (float)(new Random()).NextDouble() * 2.0f - 1.0f);
                base.Update(gameTime, clientBounds);
            }
            else
            {
                Update(gameTime, clientBounds);
            }
        }

        public void ChangeDirection()
        {
            speed.X *= -1;
            speed.Y *= -1;
        }
    }
}
