using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class Gun
    {
        public Vector2 Position { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GunSize Size { get; set; }
        public Texture2D Texture { get; set; }
        public float MoveSpeed { get; set; }
        public Rectangle Rect { get; set; }
        public Rectangle TargetRect { get; set; }

        public Gun(Vector2 position, int width, int height, GunSize size, float speed)
        {
            Position = position;
            Width = width;
            Height = height;
            Size = size;
            MoveSpeed = speed;
            Center = new Vector2(Position.X + width / 2, Position.Y + height / 2);
        }

        public void LoadTexture(Texture2D texture)
        {
            Texture = texture;
            Width = texture.Width;
            Height = texture.Height;
            Rect = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public void UpdateTarget()
        {
            Target = new Vector2(Position.X + Width / 2 - 3, Position.Y + Height / 2 - 3);
            TargetRect = new Rectangle((int)(Position.X + Width / 2 - 3 + 0.5f), (int)(Position.Y + Height / 2 - 3 + 0.5f), 6, 6);
        }
    }

    public enum GunSize
    {
        Small, Medium, Big
    }
}
