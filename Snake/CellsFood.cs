namespace Snake
{
    internal class CellsFood : Cells
    {
        public CellsFood(int x, int y) : base(x, y)
        {
            Kind = Cellkind.Food;
        }
    }
}
