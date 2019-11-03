using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        static Bitmap back = new Bitmap("field2.png");

        Timer game_timer;
        Timer user_timer;
        int player_speed, game_speed;

        SnakeUser user_snake;
        SnakeEvel evel;
        Cells[] poi;
        Random rnd;

        int x=10, y;
        string txt;
        int width, height;
        /// <summary>
        /// разрешение поворот змейки: true - присутствует
        /// </summary>
        bool step;

        /// <summary>
        /// наличие ускорителя в игре
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            this.BackgroundImage = back;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.KeyDown += Movement;
            this.Paint += Form_Paint;
            this.DoubleBuffered = true;

            width = 60;
            height=35;
            player_speed = 100;
            game_speed = 100;

            rnd = new Random();
            poi = new Cells[4];
            poi[0] = new CellsFood(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10);
            poi[1] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10,Cellkind.Speed);
            poi[2] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10, Cellkind.Vision);
            poi[3] = new CellsEffect(rnd.Next(10, width) * 10, rnd.Next(10, height) * 10, Cellkind.BadVision);

            user_snake = new SnakeUser(100,250);
            evel = new SnakeEvel(600,250);

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

            this.Refresh();
        }

        private void Effects()
        {
            if (user_snake.Loop) user_snake.Eat(Cellkind.Tail);

            txt = "";
            user_timer.Interval = 100;
            user_snake.Rad_vis = 100;
            evel.Rad_vis = 100;

            if (poi[1].Inaction)
            {
                user_timer.Interval = 50;
                txt = txt + ((CellsEffect)poi[1]).Txt + "\n";
            }

            if (poi[2].Inaction)
            {
                user_snake.Rad_vis = 1000;
                txt = txt + ((CellsEffect)poi[2]).Txt + "\n";
            }

            if (poi[3].Inaction)
            {
                evel.Rad_vis = 1000;
                user_snake.Rad_vis = 20;
                txt = txt + ((CellsEffect)poi[3]).Txt + "\n";
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
            this.Refresh();
        }
        private void EatFood(Snake snk)
        {
            for (int i = 0; i < poi.Length; i++)
            {
                if (snk.Rect.IntersectsWith(poi[i].Rect))
                {
                    snk.Eat(poi[i].Kind);
                    poi[i].Place(snk);
                }
            }
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(new Pen(Color.Black), 100, 50, width*10, height*10);
            user_snake.Draw(g);
            evel.Draw(g);
            foreach (Cells p in poi)
            {
                //if (Math.Sqrt(Math.Pow((user_snake.X - p.X),2) + Math.Pow((user_snake.Y - p.Y),2))< user_snake.Rad_vis) p.Draw(g);
                p.Draw(g);
            }


            g.DrawString($"Player: {user_snake.scores.ToString()}",new Font("Calibri",14,FontStyle.Bold),Brushes.White,10,5);
            g.DrawString($"Computer: {evel.scores.ToString()}", new Font("Calibri", 14, FontStyle.Bold), Brushes.White, 10, 30);
            g.DrawString($"Эффекты: {txt}", new Font("Calibri", 14, FontStyle.Bold), Brushes.White, 150, 5);

            //g.DrawString($"SPact: {poi[1].Act_period.ToString()}", new Font("Calibri", 14, FontStyle.Bold), Brushes.White, 400, 5);
            //g.DrawString($"VSact: {poi[2].Act_period.ToString()}", new Font("Calibri", 14, FontStyle.Bold), Brushes.White, 400, 30);

            //g.DrawString($"Sp: {user_timer.Interval.ToString()}", new Font("Calibri", 14, FontStyle.Bold), Brushes.White, 600, 5);
            //g.DrawString($"Rad: {user_snake.Rad_vis.ToString()}", new Font("Calibri", 14, FontStyle.Bold), Brushes.White, 600, 30);
        }

    }
}
