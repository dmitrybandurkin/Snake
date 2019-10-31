using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        List<Cells> snake;
        public int X => snake[0].X;
        public int Y => snake[0].Y;
        public int Length => snake.Count;

        private int loopdelete;
        public bool Loop
        {
            get
            {
                if (Length > 4)
                {
                    for (int i = 4; i < Length; i++)
                    {
                        if (snake[0].X == snake[i].X && snake[0].Y == snake[i].Y)
                        {
                            loopdelete = i;
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public Snake()
        {
            snake = new List<Cells>();
            snake.Add(new Cells(100, 100, Cellkind.Head));
        }
        public void Move(int shift_x, int shift_y)
        {
            int[,] coord = new int[snake.Count, 2];

            for (int i = 0; i < snake.Count; i++)
            {
                coord[i, 0] = snake[i].X;
                coord[i, 1] = snake[i].Y;
            }

            snake[0].X += shift_x;
            snake[0].Y += shift_y;

            for (int i = 1; i < snake.Count; i++)
            {
                snake[i].X = coord[i - 1, 0];
                snake[i].Y = coord[i - 1, 1];
            }
        }
        public void Eat(Cellkind kind)
        {
            if (kind == Cellkind.Food) snake.Add(new Cells(snake[Length-1].X, snake[Length - 1].Y, Cellkind.Tail));
            if (kind == Cellkind.Tail) snake.RemoveRange(loopdelete, Length - loopdelete);
        }
        public void Draw(Graphics g)
        {
            foreach (Cells cell in snake)
            {
                cell.Draw(g);
            }
        }
    }
}
