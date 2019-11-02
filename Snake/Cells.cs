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
        private Bitmap image_head = new Bitmap("head.png");
        private Bitmap image_tail = new Bitmap("tail.png");
        private Bitmap image_mouse = new Bitmap("mouse.png");
        private Bitmap image_speed = new Bitmap("speed.png");

        public Random rnd;

        /// <summary>
        /// время замеса новой фичи
        /// </summary>
        public int Mixing_period { get; set; }

        protected Point p;
        protected Size s;
        public int X { get; set; }
        public int Y { get; set; }
        public Cellkind Kind { get; set; }
        public Cells(int x, int y)
        {

            X = x;
            Y = y;
        }

        public Cells(int x, int y, Cellkind kind)
        {
            X = x;
            Y = y;
            Kind = kind;
        }

        public virtual void Place(Snake obj)
        {
            bool wrong_coord;
            rnd = new Random();

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

            if (this.Kind == Cellkind.Head) g.DrawImage(image_head, Rect);
            if (this.Kind == Cellkind.Tail) g.DrawImage(image_tail, Rect);
            if (this.Kind == Cellkind.Food) g.DrawImage(image_mouse, Rect);
            if (this.Kind == Cellkind.Speed) g.DrawImage(image_speed, Rect);
        }

        public virtual void TimeToMix() {}
    }
}
