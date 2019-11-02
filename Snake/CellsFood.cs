using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class CellsFood:Cells
    {
        Random rnd;
        public CellsFood(int x, int y, Cellkind kind) :base(x,y,kind) 
        {
            rnd = new Random();
        }

        public override void Place(Snake obj)
        {
            base.Place(obj);
        }
    }

}
