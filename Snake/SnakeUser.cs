using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;

namespace Snake
{
    class SnakeUser:Snake
    {
        private Timer timer;
        public int Speed_period { get; set; }
        public SnakeUser(int x, int y):base(x,y)
        {
            timer = new Timer() { Interval = 100, Enabled = false };
            timer.Elapsed += TimerTick;
            snake = new List<Cells>();
            snake.Add(new Cells(x, y, Cellkind.Head));
        } 
        public override void Eat(Cellkind kind)
        {
            if (kind == Cellkind.Tail)
            {
                snake.RemoveRange(loopdelete, Length - loopdelete);
                scores = scores/2;
            }
                
            if (kind == Cellkind.Food)
            {
                snake.Add(new Cells(snake[Length - 1].X, snake[Length - 1].Y, Cellkind.Tail));
                scores += 10;
            }

            if (kind == Cellkind.Speed)
            {
                Speed = 10;
                Speed_period = 0;
                timer.Enabled = true;
                timer.Start();
            }
        }

        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            if (Speed_period < 50) Speed_period++;
            else 
            {
                Speed = 5;
                timer.Enabled = false;
                timer.Stop();
                Speed_period = 0;
            }
        }
    }
}
