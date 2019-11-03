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
        Timer timer;
        Timer user_timer;

        SnakeUser user_snake;
        SnakeEvel evel;
        Cells[] poi;
        Random rnd;

        int x=10, y;
        string txt;
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

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.BackColor = Color.White;

            this.KeyDown += Movement;
            this.Paint += Form_Paint;
            this.DoubleBuffered = true;

            rnd = new Random();
            poi = new Cells[2];
            poi[0] = new CellsFood(rnd.Next(10, 60) * 10, rnd.Next(10, 35) * 10);
            poi[1] = new CellsSpeed(rnd.Next(10, 60) * 10, rnd.Next(10, 35) * 10);

            user_snake = new SnakeUser(100,250);
            evel = new SnakeEvel(600,250);

            InitTimer();
            InitUserTimer();
        }

        private void InitUserTimer()
        {
            user_timer = new Timer() { Interval = 100, Enabled = true };
            user_timer.Tick += UserTimerTick;
            user_timer.Start();
        }

        private void InitTimer()
        {
            timer = new Timer() { Interval = 100, Enabled = true };
            timer.Tick += TimerTick;
            timer.Start();
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

            if (user_snake.Speedup)
            {
                user_timer.Interval = 50;
                txt = "Многоножка!";
            }
            else
            {
                txt = "";
                user_timer.Interval = 100;
            }
        }
        private void Outofrange()
        {
            if (user_snake.X < 100) user_snake.X = 690;
            if (user_snake.X > 700) user_snake.X = 100;
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
            g.DrawRectangle(new Pen(Color.Black), 100, 50, 600, 350);
            user_snake.Draw(g);
            evel.Draw(g);
            foreach (Cells p in poi)
            {
                p.Draw(g);
            }
            g.DrawString($"Player: {user_snake.scores.ToString()}",new Font("Calibri",12),Brushes.Black,10,10);
            g.DrawString($"Computer: {evel.scores.ToString()}", new Font("Calibri", 12), Brushes.Black, 10, 30);
            g.DrawString($"Эффекты: {txt}", new Font("Calibri", 12), Brushes.Black, 150, 10);
            //g.DrawString($"до новой 100: {poi[1].Mixing_period.ToString()}", new Font("Calibri", 12), Brushes.Black, 150, 30);
            //g.DrawString($"fx: {poi[0].X.ToString()}, fy:{poi[0].Y.ToString()}", new Font("Calibri", 12), Brushes.Black, 300, 10);
            //g.DrawString($"sp: {poi[1].X.ToString()}, fy:{poi[1].Y.ToString()}", new Font("Calibri", 12), Brushes.Black, 300, 30);
            //g.DrawString($"скорость: {user_snake.Speed.ToString()}", new Font("Calibri", 12), Brushes.Black, 450, 10);
        }

    }
}
