using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    class SnakeEvel : Snake
    {
        Random rnd;
        int randompoint_x, randompoint_y;
        public SnakeEvel(int x, int y):base(x,y)
        {
            randompoint_x = 300;
            randompoint_y = 300;
            rnd = new Random();
            snake = new List<Cells>();
            snake.Add(new Cells(x, y, Cellkind.Head));
            snake.Add(new Cells(x, y, Cellkind.Tail));
            snake.Add(new Cells(x, y, Cellkind.Tail));
            snake.Add(new Cells(x, y, Cellkind.Tail));
        }
        public void AI(Cells obj)
        {
            int s = 10;

            AlgSecond(s);

            void AlgSecond(int speed)
            {
                if (Math.Abs(randompoint_x - snake[0].X) < 10 || Math.Abs(randompoint_y - snake[0].Y) < 10)
                {
                    randompoint_x = rnd.Next(100, 690);
                    randompoint_y = rnd.Next(50, 390);
                }

                if (Math.Abs(obj.X - snake[0].X) > rad_vis || Math.Abs(obj.Y - snake[0].Y) > rad_vis)
                {
                    if (snake[0].X < randompoint_x) Move(s, 0);
                    if (snake[0].X > randompoint_x) Move(-s, 0);
                    if (snake[0].Y < randompoint_y) Move(0, s);
                    if (snake[0].Y > randompoint_y) Move(0, -s);
                }
                else AlgFirst(s);
            }

            void AlgFirst(int speed)
            {
                if (snake[0].X < obj.X) Move(speed, 0);
                if (snake[0].X > obj.X) Move(-speed, 0);
                if (snake[0].Y < obj.Y) Move(0, speed);
                if (snake[0].Y > obj.Y) Move(0, -speed);
            }
        }
        public override void Eat(Cellkind kind) 
        {
            if (kind == Cellkind.Food) scores += 10;
        }
    }
}
