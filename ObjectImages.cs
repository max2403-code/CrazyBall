using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyBall
{
    public static class ObjectImages
    {
        public const int AreaWidth = 1000;
        public const int AreaHeight = 563;
        public const int BallDiameter = 50;
        public const int PlayerWidth = 148;
        public const int PlayerHeight = 13;

        public static Bitmap bacground = Resource1.background;
        public static Bitmap bacground2 = Resource1.background2;

        public static Bitmap playerImage = Resource1.player;
        public static Bitmap playerImage2 = Resource1.player2;


        public static Bitmap ballImage = Resource1.ball;
        public static Bitmap ballImage2 = Resource1.ball2;

        public static Bitmap compImage = Resource1.comp;
        public static Bitmap compImage2 = Resource1.comp2;


        //public static Bitmap bacground = new(Image.FromFile("images\\background.png"));
        //public static Bitmap bacground2 = new(Image.FromFile("images\\background2.png"));

        //public static Bitmap playerImage = new(Image.FromFile("images\\player.png"));
        //public static Bitmap playerImage2 = new(Image.FromFile("images\\player2.png"));


        //public static Bitmap ballImage = new(Image.FromFile("images\\ball.png"));
        //public static Bitmap ballImage2 = new(Image.FromFile("images\\ball2.png"));

        //public static Bitmap compImage = new(Image.FromFile("images\\comp.png"));
        //public static Bitmap compImage2 = new(Image.FromFile("images\\comp2.png"));


    }
}
