using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyBall
{
    public class Game
    {
        public int AreaWidth { get; }
        public int AreaHeight { get; }
        public Player Player { get; }
        public Player CompPlayer { get; }
        public Ball Ball { get; }
        public Level Level { get; private set; }

        public event Action Sound;


        public Game(Level level)
        {
            AreaWidth = ObjectImages.AreaWidth;
            AreaHeight = ObjectImages.AreaHeight;
            Player = new Player(new Point(500, 530), 17, ObjectImages.PlayerWidth / 2);
            CompPlayer = new Player(new Point(500, 33), 17, ObjectImages.PlayerWidth / 2);
            Ball = new Ball(new Point(500, Player.StartY - ObjectImages.BallDiameter / 2), true);
            InitializeLevel(level);
        }

        public void InitializeLevel(Level level)
        {
            Level = level;
            CompPlayer.ChangeLevelSpeedCorrector(Level.LevelSpeedPlayerCorrector);
            Ball.ChangeLevelSpeedCorrector(Level.LevelSpeedBallCorrector);
        }


        public void Start()
        {
            Player.ToStartPosition("first");
            CompPlayer.ToStartPosition("second");
            Ball.ToBallStartPosition("from first");
        }

        public void StartFromSecondPlayer()
        {
            Player.ToStartPosition("first");
            CompPlayer.ToStartPosition("second");
            Ball.ToBallStartPosition("from second");
        }

        public void ClearPlayerScore()
        {
            Player.ClearScore();
            CompPlayer.ClearScore();
        }

        public void MovePlayer(Direction dir, Player player)
        {
            if (AtMap(dir, player)) player.Move(dir);
        }



        private bool AtMap(Direction dir, Player player)
        {
            if (dir == Direction.Left)
            {
                var newPoint = new Point(player.Position.X - player.MoveSpeed, player.Position.Y);
                return newPoint.X - player.BoxRadius > 0;
            }
            if (dir == Direction.Right)
            {
                var newPoint = new Point(player.Position.X + player.MoveSpeed, player.Position.Y);
                return newPoint.X + player.BoxRadius < AreaWidth;
            }
            return false;
        }

        public void ChangeBallPosition()
        {
            var radius = Ball.BoxRadius;
            var x = Ball.Position.X;
            var y = Ball.Position.Y;
            var isTouchPlayer = Player.Points.Any(p => Math.Sqrt((p.X - x) * (p.X - x) + (p.Y - y) * (p.Y - y)) <= radius) && y < Player.Position.Y;
            var isTouchCompPlayer = CompPlayer.Points.Any(p => Math.Sqrt((p.X - x) * (p.X - x) + (p.Y - y) * (p.Y - y)) <= radius) && y > CompPlayer.Position.Y;
            if (isTouchCompPlayer || isTouchPlayer)
            {
                if (Sound != null) Sound();
            }

            if (Ball.MoveUp)
            {
                if (x <= radius || x + radius >= AreaWidth)
                {
                    Ball.ChangeDirection();
                    if (isTouchCompPlayer)
                        Ball.ChangeMove();
                }
                else if (isTouchCompPlayer)
                    Ball.ChangeMove(true);
            }
            else
            {
                if (x <= radius || x + radius >= AreaWidth)
                {
                    Ball.ChangeDirection();
                    if (isTouchPlayer)
                        Ball.ChangeMove();
                }
                else if (isTouchPlayer)
                    Ball.ChangeMove(true);
            }
            Ball.Move();
        }

        public bool OnScore()
        {
            var y = Ball.Position.Y;
            if (y < -100)
            {
                Player.ScoreUp();
                StartFromSecondPlayer();
                Thread.Sleep(200);

                return true;
            }

            if (y > AreaHeight + 100)
            {
                CompPlayer.ScoreUp();
                Start();
                Thread.Sleep(200);
                return true;
            }

            return false;
        }

        public Direction MoveByGameIntellect()
        {
            var deltaRight = 18;
            var deltaLeft = 0 - deltaRight;
            var dir = Direction.None;
            if (Ball.MoveUp)
            {
                if (Ball.Position.X - CompPlayer.Position.X > deltaRight)
                    dir = Direction.Right;
                else if (Ball.Position.X - CompPlayer.Position.X < deltaLeft)
                    dir = Direction.Left;
            }

            return dir;
        }
    }
}
