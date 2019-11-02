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
        Food,
        Speed,
        Empty
    } 
    class Cells
    {
        const int cell_size = 10;
        public Rectangle Rect { get; set; }

        private Random rnd;

        /// <summary>
        /// время замеса новой фичи
        /// </summary>
        public int Mixing_period { get; set; }

        protected Point p;
        protected Size s;
        public int X { get; set; }
        public int Y { get; set; }
        public Cellkind Kind { get; set; }
        public Cells (int x, int y, Cellkind kind)
        {
            X = x;
            Y = y;
            rnd = new Random();
            Kind = kind;
        }

        public virtual void Place(Snake obj)
        {
            bool wrong_coord;

            do
            {
                wrong_coord = false;
                X = rnd.Next(10, 60) * 10;
                Y = rnd.Next(10, 35) * 10;
                p = new Point(X, Y);
                Rect = new Rectangle(p, s);

                for (int i = 0; i < obj.Length; i++)
                {
                    if (obj.Snake_list[i].Rect.IntersectsWith(Rect)) wrong_coord = true;
                }
            }
            while (wrong_coord);
        }
        public void Draw(Graphics g)
        {
            p = new Point(X, Y);
            s = new Size(cell_size, cell_size);
            Rect = new Rectangle(p, s);
            if (this.Kind == Cellkind.Head || this.Kind == Cellkind.Tail) g.FillRectangle(Brushes.Black, Rect);
            if (this.Kind == Cellkind.Food) g.FillRectangle(Brushes.Red, Rect);
            if (this.Kind == Cellkind.Speed) g.FillRectangle(Brushes.Green, Rect);
        }

        public virtual void TimeToMix() {}
    }
}
