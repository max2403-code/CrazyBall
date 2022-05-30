using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBall
{
    public interface IGameObject
    {
        Point Position { get; }
        int BoxRadius { get; }
    }
}