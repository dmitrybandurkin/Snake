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
        public SnakeUser(int x, int y):base(x,y)
        {
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
        }
    }
}
