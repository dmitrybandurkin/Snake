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
        SnakeUser current_snake;
        SnakeEvel evel;
        Cells[] poi;

        Random rnd;
        int x=10, y;

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
            this.KeyDown += Movement;
            this.Paint += Form_Paint;
            this.DoubleBuffered = true;

            poi = new Cells[2];
            rnd = new Random();
            poi[0] = new CellsFood(rnd.Next(10, 21) * 10, rnd.Next(10, 21) * 10, Cellkind.Food);
            poi[1] = new CellsSpeed(rnd.Next(10, 21) * 10, rnd.Next(10, 21) * 10, Cellkind.Speed);

            current_snake = new SnakeUser();
            evel = new SnakeEvel();

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
        private void TimerTick(object sender, EventArgs e)
        {
            step = true;

            current_snake.Move(x, y);
            evel.AI(poi[0]);

            EatFood(current_snake);
            EatFood(evel);

            if (current_snake.Loop) current_snake.Eat(Cellkind.Tail);

            this.Refresh();

            void EatFood(Snake snk)
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
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(new Pen(Color.Black), 100, 50, 600, 350);
            current_snake.Draw(g);
            evel.Draw(g);
            foreach (Cells p in poi)
            {
                p.Draw(g);
            }
            g.DrawString($"player: {current_snake.scores.ToString()}",new Font("Calibri",12),Brushes.Black,10,10);
            g.DrawString($"computer: {evel.scores.ToString()}", new Font("Calibri", 12), Brushes.Black, 10, 30);
            g.DrawString($"T: {current_snake.Speed_period.ToString()}", new Font("Calibri", 12), Brushes.Black, 100, 10);
            g.DrawString($"TT: {poi[1].Mixing_period.ToString()}", new Font("Calibri", 12), Brushes.Black, 100, 30);
        }

    }
}
