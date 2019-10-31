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
        Snake current_snake;
        Cells food;
        Random rnd;
        int x=10, y;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += Movement;
            this.Paint += Form_Paint;

            rnd = new Random();
            food = new Cells(rnd.Next(10, 21) * 10, rnd.Next(10, 21) * 10, Cellkind.Food);

            current_snake = new Snake();

            timer = new Timer() { Interval = 100, Enabled = true };
            timer.Tick += TimerTick;
            timer.Start();

        }

        private void Movement(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
               case Keys.Left when x!=10:
               case Keys.Right when x!=-10:
                    x = (e.KeyCode == Keys.Left) ? -10 : 10;
                    y = 0;
                    break;
               case Keys.Up when y!=10:
               case Keys.Down when y!=-10:
                    y = (e.KeyCode == Keys.Up) ? -10 : 10;
                    x = 0;
                    break;
                case Keys.Space:
                    break;
            }
        }
        private void TimerTick(object sender, EventArgs e)
        {
            current_snake.Move(x, y);
            if (current_snake.X == food.X & current_snake.Y == food.Y)
            {
                current_snake.Eat(Cellkind.Food);
                RandomFood();
            }

            if (current_snake.Loop) current_snake.Eat(Cellkind.Tail);

            this.Refresh();
        }

        private void RandomFood() //Добавить проверку на непопадание в тело змейки
        {
            int rnd_x, rnd_y;
            
            rnd_x = rnd.Next(10, 21) * 10;
            rnd_y = rnd.Next(10, 21) * 10;

            food = new Cells(rnd_x, rnd_y, Cellkind.Food);
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            current_snake.Draw(g);
            food.Draw(g);
        }

    }
}
