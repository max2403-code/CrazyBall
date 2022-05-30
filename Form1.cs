using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using Timer = System.Windows.Forms.Timer;

namespace CrazyBall
{
    public partial class Form1 : Form
    {
        private SoundPlayer sp = new SoundPlayer(Resource1.sound2);
        private bool SoundOn = true;

        private Direction Direction { get; set; }
        private Direction CompDirection { get; set; }

        private Bitmap bacground = ObjectImages.bacground;
        private Bitmap playerImage = ObjectImages.playerImage;
        private Bitmap compPlayerImage = ObjectImages.compImage;
        private Bitmap ballImage = ObjectImages.ballImage;

        private Timer Timer { get; }
        private Timer MoveTimer { get; }
        private Timer CompPlayerTimer { get; }
        private Timer CompMovePlayerTimer { get; }
        private Timer GamePaintTimer { get; }

        public Level SingleEasy = new Level(5, 1.3);
        public Level SingleHard = new Level(0, 1.0);
        public Level Multi = new Level(0, 1.2);

        private Game Game { get; }

        private Button buttonSingle = new Button();
        private Button buttonMulti = new Button();

        private Button buttonSingleEasy = new Button();
        private Button buttonSingleHard = new Button();

        private Button buttonControl = new Button();
        private Button buttonTheme = new Button();

        private const string ControlText = "Q - quit game\nP - pause\nM - mute\nA - move player 1 left\nD - move player 1 right\nLeft button - move player 2 left\nRight button - move player 2 right";

        private bool SingleMode { get; set; }
        private bool MultiMode { get; set; }
        private bool PauseOn { get; set; } = true;




        public Form1()
        {
            BackgroundImage = bacground;
            ClientSize = new Size(bacground.Width + 200, bacground.Height);
            DoubleBuffered = true;

            Paint += Form1_Paint;

            KeyDown += Form1_KeyDown;
            KeyDown += Form1_KeyDown1;
            KeyDown += Form1_KeyDown2;
            KeyDown += Form1_KeyDown3; ;


            KeyUp += Form1_KeyUp;
            KeyUp += Form1_KeyUp1;

            Timer = new Timer();
            Timer.Interval = 5;
            Timer.Tick += Timer_Tick;

            MoveTimer = new Timer();
            MoveTimer.Interval = 5;
            MoveTimer.Tick += MoveTimer_Tick;

            CompMovePlayerTimer = new Timer();
            CompMovePlayerTimer.Interval = 5;
            CompMovePlayerTimer.Tick += CompMovePlayerTimer_Tick;

            CompPlayerTimer = new Timer();
            CompPlayerTimer.Interval = 5;
            CompPlayerTimer.Tick += CompPlayerTimer_Tick;

            GamePaintTimer = new Timer();
            GamePaintTimer.Interval = 5;
            GamePaintTimer.Tick += GamePaintTimer_Tick;

            buttonSingle.Size = new Size(150, 40);
            buttonSingle.Location = new Point(bacground.Width + 25, 50);
            buttonSingle.Text = "Single game";
            buttonSingle.Click += ButtonSingle_Click;
            Controls.Add(buttonSingle);

            buttonMulti.Size = new Size(150, 40);
            buttonMulti.Location = new Point(bacground.Width + 25, 120);
            buttonMulti.Text = "Two players";
            buttonMulti.Click += ButtonMulti_Click;
            Controls.Add(buttonMulti);

            buttonSingleEasy.Size = new Size(150, 40);
            buttonSingleEasy.Location = new Point(bacground.Width + 25, 50);
            buttonSingleEasy.Text = "Amateur";
            buttonSingleEasy.Click += ButtonSingleEasy_Click;
            Controls.Add(buttonSingleEasy);

            buttonSingleHard.Size = new Size(150, 40);
            buttonSingleHard.Location = new Point(bacground.Width + 25, 120);
            buttonSingleHard.Text = "Expert";
            buttonSingleHard.Click += ButtonSingleHard_Click;
            Controls.Add(buttonSingleHard);

            buttonControl.Size = new Size(150, 40);
            buttonControl.Location = new Point(bacground.Width + 25, 430);
            buttonControl.Text = "Control";
            buttonControl.Click += ButtonControl_Click;
            Controls.Add(buttonControl);

            buttonTheme.Size = new Size(150, 40);
            buttonTheme.Location = new Point(bacground.Width + 25, 500);
            buttonTheme.Text = "Theme";
            buttonTheme.Click += ButtonTheme_Click;
            Controls.Add(buttonTheme);

            buttonSingleEasy.Visible = false;
            buttonSingleHard.Visible = false;

            Game = new Game(SingleEasy);
            sp.Load();
            Game.Sound += Game_Sound;

            KeyPreview = true;

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void ButtonControl_Click(object sender, EventArgs e)
        {
            if (!PauseOn) return;

            var mb = MessageBox.Show(ControlText, "Control", MessageBoxButtons.OK);
        }

        private void Game_Sound()
        {
            sp.Play();
        }

        private void ButtonTheme_Click(object sender, EventArgs e)
        {
            if (!PauseOn) return;
            if (BackgroundImage == ObjectImages.bacground)
            {
                BackgroundImage = ObjectImages.bacground2;
                ballImage = ObjectImages.ballImage2;
                compPlayerImage = ObjectImages.compImage2;
                playerImage = ObjectImages.playerImage2;
            }
            else
            {
                BackgroundImage = ObjectImages.bacground;
                ballImage = ObjectImages.ballImage;
                compPlayerImage = ObjectImages.compImage;
                playerImage = ObjectImages.playerImage;
            }
            Refresh();
        }

        private void ButtonSingleHard_Click(object sender, EventArgs e)
        {
            if (!PauseOn) return;
            if (PauseOn) PauseOn = false;
            if (!SingleMode) SingleMode = true;
            if (MultiMode) MultiMode = false;
            if (Game.Level != SingleHard) Game.InitializeLevel(SingleHard);

            Game.ClearPlayerScore();
            GamePaintTimer.Start();
            Timer.Start();
            CompPlayerTimer.Start();
            CompMovePlayerTimer.Start();
            Game.Start();

            buttonSingleEasy.Visible = false;
            buttonSingleHard.Visible = false;
            buttonTheme.Visible = false;
            buttonControl.Visible = false;
        }

        private void ButtonSingleEasy_Click(object sender, EventArgs e)
        {
            if (!PauseOn) return;

            if (PauseOn) PauseOn = false;
            if (!SingleMode) SingleMode = true;
            if (MultiMode) MultiMode = false;
            if (Game.Level != SingleEasy) Game.InitializeLevel(SingleEasy);

            Game.ClearPlayerScore();
            GamePaintTimer.Start();
            Timer.Start();
            CompPlayerTimer.Start();
            CompMovePlayerTimer.Start();
            Game.Start();

            buttonSingleEasy.Visible = false;
            buttonSingleHard.Visible = false;
            buttonTheme.Visible = false;
            buttonControl.Visible = false;
        }

        private void ButtonSingle_Click(object sender, EventArgs e)
        {
            if (!PauseOn) return;

            buttonSingleEasy.Visible = true;
            buttonSingleHard.Visible = true;

            buttonSingle.Visible = false;
            buttonMulti.Visible = false;
        }

        private void ButtonMulti_Click(object sender, EventArgs e)
        {
            if (!PauseOn) return;

            if (PauseOn) PauseOn = false;
            if (SingleMode) SingleMode = false;
            if (!MultiMode) MultiMode = true;
            if (Game.Level != Multi) Game.InitializeLevel(Multi);

            Game.ClearPlayerScore();
            GamePaintTimer.Start();
            Timer.Start();
            Game.Start();

            buttonSingle.Visible = false;
            buttonMulti.Visible = false;
            buttonTheme.Visible = false;
            buttonControl.Visible = false;
        }

        private void Form1_KeyDown3(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.P || !SingleMode && !MultiMode) return;
            if (!PauseOn)
            {
                PauseOn = true;
                Timer.Stop();
                CompMovePlayerTimer.Stop();
                MoveTimer.Stop();
                CompPlayerTimer.Stop();
                GamePaintTimer.Stop();
                buttonSingle.Visible = true;
                buttonMulti.Visible = true;
                buttonTheme.Visible = true;
                buttonControl.Visible = true;
            }
            else
            {
                PauseOn = false;
                GamePaintTimer.Start();
                Timer.Start();
                if (SingleMode)
                {
                    CompPlayerTimer.Start();
                    CompMovePlayerTimer.Start();
                }
                buttonSingle.Visible = false;
                buttonMulti.Visible = false;
                buttonSingleEasy.Visible = false;
                buttonSingleHard.Visible = false;
                buttonTheme.Visible = false;
                buttonControl.Visible = false;
            }
        }

        private void Form1_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
            {
                if (SoundOn)
                {
                    SoundOn = false;
                    Game.Sound -= Game_Sound;
                }
                else
                {
                    SoundOn = true;
                    Game.Sound += Game_Sound;
                }
            }

            if (e.KeyCode == Keys.Q)
            {
                Close();
                //Log.Write();
            }
        }

        private void Form1_KeyDown1(object sender, KeyEventArgs e)
        {
            if (PauseOn || SingleMode) return;
            if (e.KeyCode == Keys.Left)
            {
                CompDirection = Direction.Left;
                CompMovePlayerTimer.Start();
            }
            else if (e.KeyCode == Keys.Right)
            {
                CompDirection = Direction.Right;
                CompMovePlayerTimer.Start();
            }
        }

        private void Form1_KeyUp1(object sender, KeyEventArgs e)
        {
            if (PauseOn || SingleMode) return;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                CompMovePlayerTimer.Stop();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (PauseOn) return;
            if (e.KeyCode == Keys.A)
            {
                Direction = Direction.Left;
                MoveTimer.Start();
            }
            if (e.KeyCode == Keys.D)
            {
                Direction = Direction.Right;
                MoveTimer.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (PauseOn) return;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
                MoveTimer.Stop();
        }

        private void GamePaintTimer_Tick(object sender, EventArgs e)
        {

            //Log.LogBuilder.Append($"\nstart GamePaintTimer_Tick {DateTime.Now.Millisecond}\n");
            Refresh();
            //Log.LogBuilder.Append($"end GamePaintTimer_Tick {DateTime.Now.Millisecond}\n");

        }

        private void CompPlayerTimer_Tick(object sender, EventArgs e)
        {
            CompDirection = Game.MoveByGameIntellect();
        }

        private void CompMovePlayerTimer_Tick(object sender, EventArgs e)
        {
            Game.MovePlayer(CompDirection, Game.CompPlayer);
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            Game.MovePlayer(Direction, Game.Player);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!Game.OnScore())
                Game.ChangeBallPosition();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var player = Game.Player;
            var compPlayer = Game.CompPlayer;
            var ball = Game.Ball;
            var lineColor = BackgroundImage == ObjectImages.bacground ? Color.Aqua : Color.Black;
            var brush = BackgroundImage == ObjectImages.bacground ? Brushes.White : Brushes.Black;

            g.DrawLine(new Pen(lineColor), new Point(bacground.Width, 0), new Point(bacground.Width, bacground.Height));

            g.DrawImage(playerImage, new Rectangle(player.Position.X - player.BoxRadius, player.Position.Y, playerImage.Width, playerImage.Height));
            g.DrawImage(compPlayerImage, new Rectangle(compPlayer.Position.X - compPlayer.BoxRadius, compPlayer.Position.Y - compPlayerImage.Height, compPlayerImage.Width, compPlayerImage.Height));
            g.DrawImage(ballImage, new Rectangle(ball.Position.X - ball.BoxRadius, ball.Position.Y - ball.BoxRadius, ballImage.Width, ballImage.Height));

            g.DrawString(compPlayer.Score.ToString(), new Font("Helvetica", 35), brush, new PointF(1075, 170));
            g.DrawString(player.Score.ToString(), new Font("Helvetica", 35), Brushes.White, new PointF(1075, 361));

            g.DrawString("Player 2", new Font("Helvetica", 15), brush, new PointF(1060, 140));
            g.DrawString("Player 1", new Font("Helvetica", 15), Brushes.White, new PointF(1060, 331));
        }
    }
}
