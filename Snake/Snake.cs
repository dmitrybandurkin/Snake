using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    abstract class Snake
    {
        protected List<Cells> snake;
        public int Speed { get; set; }
        public int scores { get; set; }
        public int Rad_vis { get; set; }
        public int X
        {
            get => snake[0].X;
            set => snake[0].X = value;
        }
        public int Y
        {
            get => snake[0].Y;
            set => snake[0].Y = value;
        }

        public Rectangle Rect => snake[0].Rect;
        public int Length => snake.Count;
        public List<Cells> Snake_list => snake;

        protected int loopdelete;
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
        public Snake(int x, int y)
        {
            scores = 0;
            Speed = 10;
            Rad_vis = 100;
        }
        public void Move(int shift_x, int shift_y)
        {
            shift_x = shift_x * Speed / 10;
            shift_y = shift_y * Speed / 10;

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
        virtual public void Eat(Cellkind kind) {}
        public void Draw(Graphics g)
        {
            foreach (Cells cell in snake)
            {
                cell.Draw(g);
            }

            g.DrawEllipse(new Pen(Color.Red), snake[0].X+5 - Rad_vis, snake[0].Y+5 - Rad_vis, 2* Rad_vis, 2* Rad_vis);
        }
    }
}
