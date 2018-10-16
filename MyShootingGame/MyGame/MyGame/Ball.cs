using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Ball
    {
        public Vector2 Position { get; set; }
        public Vector2 Center { get; set; }
        public float XSpeed { get; set; }
        public float YSpeed { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle Rect { get; set; }

        public Ball(Vector2 position, int width, int height, float xSpeed, float ySpeed)
        {
            Position = position;
            Width = width;
            Height = height;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
        }

        public void LoadTexture(Texture2D texture)
        {
            Texture = texture;
            Width = texture.Width;
            Height = texture.Height;
            Rect = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }
    }
}
