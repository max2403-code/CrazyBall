using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBall
{
    public class Player : IGameObject
    {
        public List<Point> Points { get; private set; }
        public Point Position { get; private set; }
        public int BoxRadius { get; }
        public int MoveSpeed { get; }
        public int StartX { get; }
        public int StartY { get; }
        public int Score { get; private set; }
        private int LevelSpeedCorrector { get; set; }

        public Player(Point startPosition, int moveSpeed, int boxRadius)
        {
            Position = startPosition;
            StartX = startPosition.X;
            StartY = startPosition.Y;
            BoxRadius = boxRadius;
            MoveSpeed = moveSpeed;
            GetPoints();
        }

        public void Move(Direction dir)
        {
            if (dir == Direction.Left)
                Position = new Point(Position.X - (MoveSpeed - LevelSpeedCorrector), Position.Y);
            if (dir == Direction.Right)
                Position = new Point(Position.X + (MoveSpeed - LevelSpeedCorrector), Position.Y);
            if (dir != Direction.None) GetPoints();
        }

        public void GetPoints()
        {
            Points = Enumerable.Range(0, ObjectImages.playerImage.Width).Select(x => new Point(Position.X - BoxRadius + x, StartY)).ToList();
        }

        public void ScoreUp()
        {
            Score++;
        }

        public void ToStartPosition(string pl)
        {
            Position = pl == "first" ? new Point(500, 530) : new Point(500, 33);
            GetPoints();
        }

        public void ClearScore()
        {
            Score = 0;
        }

        public void ChangeLevelSpeedCorrector(int newCorrector)
        {
            LevelSpeedCorrector = newCorrector;
        }
    }
}
