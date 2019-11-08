using System.Collections.Generic;

namespace Snake
{
    internal class SnakeUser : Snake
    {
        public SnakeUser(int x, int y) : base(x, y) {}

        public override void Eat(Cellkind kind)
        {
            if (kind == Cellkind.Tail)
            {
                snake.RemoveRange(loopdelete, Length - loopdelete);
                scores = scores - (Length - loopdelete) * 10 / 2;
            }

            if (kind == Cellkind.Food)
            {
                snake.Add(new Cells(snake[Length - 1].X, snake[Length - 1].Y, Cellkind.Tail));
                scores += 10;
            }

            if (kind == Cellkind.Meat)
            {
                scores += 100;
            }
        }
    }
}
