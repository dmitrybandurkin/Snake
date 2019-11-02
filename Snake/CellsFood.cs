using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class CellsFood:Cells
    {
        public CellsFood(int x, int y):base(x,y)
        {
            Kind = Cellkind.Food;
        }
    }
}
