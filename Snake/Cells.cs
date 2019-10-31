using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    enum Cellkind
    {
        Head,
        Tail,
        Food
    }
    class Cells
    {
        const int cell_size = 10;
        public int X { get; set; }
        public int Y { get; set; }
        public Cellkind Kind { get; set; }
        public Cells (int x, int y, Cellkind kind)
        {
            X = x;
            Y = y;
            Kind = kind;
        }
        public void Draw(Graphics g)
        {
            if (this.Kind == Cellkind.Head || this.Kind == Cellkind.Tail) g.FillRectangle(Brushes.Black, this.X, this.Y, cell_size, cell_size);
            else g.FillRectangle(Brushes.Red, this.X, this.Y, cell_size, cell_size);
        }
    }
}
