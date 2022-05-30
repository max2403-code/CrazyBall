using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBall
{
    public class Level
    {
        public int LevelSpeedPlayerCorrector { get; }
        public double LevelSpeedBallCorrector { get; }

        public Level(int playerCorrector, double ballCorrector)
        {
            LevelSpeedPlayerCorrector = playerCorrector;
            LevelSpeedBallCorrector = ballCorrector >= 1 ? ballCorrector : 1;
        }
    }
}