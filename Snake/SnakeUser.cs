using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    class SnakeUser : Snake
    {
        private Timer timer;
        public int Speed_period { get; set; }
        public bool Speedup {get;set;}
        public SnakeUser(int x, int y):base(x,y)
        {
            Speedup = false;
            timer = new Timer() { Interval = 100, Enabled = false };
            timer.Tick += TimerTick;
            snake = new List<Cells>();
            snake.Add(new Cells(x, y, Cellkind.Head));
        } 
        public override void Eat(Cellkind kind)
        {
            if (kind == Cellkind.Tail)
            {
                snake.RemoveRange(loopdelete, Length - loopdelete);
                scores = scores - (Length - loopdelete)*10 / 2;
            }
                
            if (kind == Cellkind.Food)
            {
                snake.Add(new Cells(snake[Length - 1].X, snake[Length - 1].Y, Cellkind.Tail));
                scores += 10;
            }

            if (kind == Cellkind.Speed)
            {
                Speedup = true;
                Speed_period = 0;
                timer.Enabled = true;
                timer.Start();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (Speed_period < 50) Speed_period++;
            else 
            {
                Speedup = false;
                timer.Enabled = false;
                timer.Stop();
                Speed_period = 0;
            }
        }
    }
}
