using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    enum Effect
    {
        Food,
        Speed,
        Vision,
        Badvision,
        Meat
    }
    public partial class Form1 : Form
    {
        private static Bitmap back = new Bitmap("field2.png");
        private Timer game_timer;
        private Timer user_timer;
        private int player_speed, game_speed;
        private SnakeUser user_snake;
        private SnakeEvel evel;
        private Cells[] poi;
        private Random rnd;
        private int x = 10, y;
        private string txt;
        private int width, height;
        private Font fnt;

        Pen border;

        /// <summary>
        /// разрешение поворот змейки: true - присутствует
        /// </summary>
        private bool step;

        Form2 tutorial;

        /// <summary>
        /// наличие ускорителя в игре
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            border = new Pen(Color.Black, 2);
            fnt = new Font("Calibri", 14, FontStyle.Bold);

            tutorial = new Form2();
            Application.Run(tutorial);
            
            BackgroundImage = back;
            FormBorderStyle = FormBorderStyle.Fixed3D;

            KeyDown += Movement;
            Paint += Form_Paint;
            DoubleBuffered = true;

            width = 60;
            height = 35;

            player_speed = 100;
            game_speed = 100;

            rnd = new Random();
            poi = new Cells[5];
            poi[(int)Effect.Food] = new CellsFood(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10);
            poi[(int)Effect.Speed] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10, Cellkind.Speed);
            poi[(int)Effect.Vision] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10, Cellkind.Vision);
            poi[(int)Effect.Badvision] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10, Cellkind.BadVision);
            poi[(int)Effect.Meat] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10, Cellkind.Empty);

            user_snake = new SnakeUser(100, 250);
            evel = new SnakeEvel(600, 250);

            InitTimer();
            InitUserTimer();
        }

        private void InitUserTimer()
        {
            user_timer = new Timer() { Interval = player_speed, Enabled = true };
            user_timer.Tick += UserTimerTick;
            user_timer.Start();
        }
        private void InitTimer()
        {
            game_timer = new Timer() { Interval = game_speed, Enabled = true };
            game_timer.Tick += TimerTick;
            game_timer.Start();
        }
        private void Movement(object sender, KeyEventArgs e)
        {
            if (step)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left when x != 10:
                    case Keys.Right when x != -10:
                        x = (e.KeyCode == Keys.Left) ? -10 : 10;
                        y = 0;
                        break;
                    case Keys.Up when y != 10:
                    case Keys.Down when y != -10:
                        y = (e.KeyCode == Keys.Up) ? -10 : 10;
                        x = 0;
                        break;
                    case Keys.Space:
                        break;
                }
                step = false;
            }
        }
        private void UserTimerTick(object sender, EventArgs e)
        {
            step = true;
            user_snake.Move(x, y);
            Outofrange();

            EatFood(user_snake);
            Effects();

            Refresh();
        }
        private void Effects()
        {
            txt = "";
            user_timer.Interval = 100;
            game_timer.Interval = 100;
            user_snake.Rad_vis = 100;
            evel.Rad_vis = 100;

            if (poi[(int)Effect.Speed].Inaction)
            {
                user_timer.Interval = user_timer.Interval - 30;
                user_snake.Rad_vis = user_snake.Rad_vis - 30;
                txt = txt + ((CellsEffect)poi[1]).Txt + "\n";
            }

            if (poi[(int)Effect.Vision].Inaction)
            {
                user_snake.Rad_vis = 1000;
                user_timer.Interval = user_timer.Interval + 50;
                game_timer.Interval = game_timer.Interval - 50;
                txt = txt + ((CellsEffect)poi[2]).Txt + "\n";
            }

            if (poi[(int)Effect.Badvision].Inaction)
            {
                evel.Rad_vis = 1000;
                user_snake.Rad_vis = user_snake.Rad_vis - 50;
                user_timer.Interval = user_timer.Interval - 50;
                txt = txt + ((CellsEffect)poi[3]).Txt + "\n";
            }

            if (poi[(int)Effect.Meat].Inaction)
            {
                user_timer.Interval = user_timer.Interval + 100;
                txt = txt + ((CellsEffect)poi[4]).Txt + "\n";
            }

        }
        private void Outofrange()
        {
            if (user_snake.X < 100) user_snake.X = 690;
            if (user_snake.X > 690) user_snake.X = 100;
            if (user_snake.Y < 50) user_snake.Y = 390;
            if (user_snake.Y > 390) user_snake.Y = 50;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            evel.AI(poi[0]);
            EatFood(evel);
            Refresh();
        }
        private void EatFood(Snake snk)
        {
            if (snk.Loop)
            {
                snk.Eat(Cellkind.Tail);
                if (snk is SnakeEvel)
                {
                    poi[4] = new CellsEffect(snk.Rect.X, snk.Rect.Y, Cellkind.Meat);
                }
            }

            for (int i = 0; i < poi.Length-1; i++)
            {
                if (snk.Rect.IntersectsWith(poi[i].Rect))
                {
                    snk.Eat(poi[i].Kind);
                    poi[i].Place(snk);
                }
            }

            if (snk is SnakeUser)
            {
                if (snk.Rect.IntersectsWith(poi[4].Rect))
                {
                    snk.Eat(poi[4].Kind);
                    poi[4].Place(snk);
                }
            }

        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(border, 100, 50, width * 10, height * 10);
            user_snake.Draw(g);
            evel.Draw(g);
            foreach (Cells p in poi)
            {
                if (Math.Sqrt(Math.Pow((user_snake.X - p.X), 2) + Math.Pow((user_snake.Y - p.Y), 2)) < user_snake.Rad_vis) p.Draw(g);
                //p.Draw(g);
            }

            g.DrawString($"Player: {user_snake.scores.ToString()}", fnt, Brushes.White, 10, 5);
            g.DrawString($"Computer: {evel.scores.ToString()}", fnt, Brushes.White, 10, 30);
            g.DrawString($"Эффекты: {txt}", fnt, Brushes.White, 150, 5);
        }

    }
}
