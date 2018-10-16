using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MyGame
{
    public class GameState
    {
        public GameStage CurrentStage { get; set; }
        public Texture2D Background { get; set; }
        public Song Music { get; set; }
        public int DisplayWidth { get; set; }
        public int DisplayHeight { get; set; }
        public int ShootCount { get; set; }
        public int Score { get; set; }

        public GameState(GameStage stage, int width, int height)
        {
            CurrentStage = stage;
            DisplayWidth = width;
            DisplayHeight = height;
            Score = 0;
            ShootCount = 0;
        }
    }

    public enum GameStage
    {
        GameStart, LevelSelection, InGame, GameOver, GameWin
    }
}
