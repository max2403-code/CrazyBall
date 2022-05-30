using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBall
{
    public class Ball : IGameObject
    {
        public Point Position { get; private set; }
        public int BoxRadius { get; }
        public int Speed { get; private set; }
        public bool MoveUp { get; private set; }
        public Direction Direction { get; private set; }
        private int MoveChanger { get; set; }
        private Random Rnd { get; } = new Random();
        private double LevelSpeedCorrector { get; set; } = 1.0;

        public Ball(Point position, bool moveUp)
        {
            BoxRadius = ObjectImages.ballImage.Width / 2;
            Position = position;
            MoveUp = moveUp;
            RandomAction();
        }

        public void Move()
        {
            if (MoveUp)
            {
                if (Direction == Direction.Left) Position = new Point(Position.X - (int)((Speed - MoveChanger) / LevelSpeedCorrector), Position.Y - (int)(Speed / LevelSpeedCorrector));
                if (Direction == Direction.Right) Position = new Point(Position.X + (int)((Speed - MoveChanger) / LevelSpeedCorrector), Position.Y - (int)(Speed / LevelSpeedCorrector));
            }
            else
            {
                if (Direction == Direction.Left) Position = new Point(Position.X - (int)((Speed - MoveChanger) / LevelSpeedCorrector), Position.Y + (int)(Speed / LevelSpeedCorrector));
                if (Direction == Direction.Right) Position = new Point(Position.X + (int)((Speed - MoveChanger) / LevelSpeedCorrector), Position.Y + (int)(Speed / LevelSpeedCorrector));
            }
        }

        public void ChangeMove(bool IsPlayer = false)
        {
            if (IsPlayer)
                RandomAction();
            MoveUp = !MoveUp;
        }

        public void ChangeDirection()
        {
            Direction = Direction == Direction.Left ? Direction.Right : Direction.Left;
        }

        private void RandomAction()
        {
            var rnd = Rnd.Next(1, 10) % 3;
            if (rnd == 0)
            {
                Speed = 12;
                MoveChanger = 0;
            }
            else if (rnd == 2)
            {
                Speed = 13;
                MoveChanger = 9;
            }
            else
            {
                Speed = 16;
                MoveChanger = 9;
            }

            rnd = Rnd.Next(1, 5);
            if (rnd == 3)
                ChangeDirection();
        }

        public void ToBallStartPosition(string pl)
        {
            Position = pl == "from first" ? new Point(500, 530 - BoxRadius) : new Point(500, 33 + BoxRadius);
            MoveUp = pl == "from first";
            RandomAction();
        }

        public void ChangeLevelSpeedCorrector(double newCorrector)
        {
            LevelSpeedCorrector = newCorrector;
        }
    }
}
