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
        Vision,
        BadVision,
        Empty
    } 
    class Cells
    {
        public bool Inaction { get; set; }

        const int cell_size = 10;
        private Rectangle rect;
        public Rectangle Rect
        { 
            get => rect; 
            set => rect = value; 
        }
        static Bitmap image_head = new Bitmap("head.png");
        static Bitmap image_tail = new Bitmap("tail.png");
        static Bitmap image_mouse = new Bitmap("mouse.png");
        static Bitmap image_speed = new Bitmap("speed.png");
        static Bitmap image_vision = new Bitmap("vision.png");
        static Bitmap image_badvision = new Bitmap("badvision.png");

        protected Random rnd;

        /// <summary>
        /// время замеса новой фичи
        /// </summary>
        public int Mixing_period { get; set; }
        public int Act_period { get; set; }

        protected static Size s = new Size(cell_size, cell_size);
        public int X { get; set; }
        public int Y { get; set; }
        public Cellkind Kind { get; set; }
        public Cells(int x, int y)
        {

            X = x;
            Y = y;
            rect = new Rectangle(X, Y, cell_size, cell_size);
        }

        public Cells(int x, int y, Cellkind kind)
        {
            X = x;
            Y = y;
            Kind = kind;
            rect = new Rectangle(X, Y, cell_size, cell_size);

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
                rect.X = X;
                rect.Y = Y;

                for (int i = 0; i < obj.Length; i++)
                {
                    if (obj.Snake_list[i].Rect.IntersectsWith(Rect)) wrong_coord = true;
                }
            }
            while (wrong_coord);
        }
        public void Draw(Graphics g)
        {
            rect.X = X;
            rect.Y = Y;

            if (this.Kind == Cellkind.Head) g.DrawImage(image_head, Rect);
            if (this.Kind == Cellkind.Tail) g.DrawImage(image_tail, Rect);
            if (this.Kind == Cellkind.Food) g.DrawImage(image_mouse, Rect);
            if (this.Kind == Cellkind.Speed) g.DrawImage(image_speed, Rect);
            if (this.Kind == Cellkind.Vision) g.DrawImage(image_vision, Rect);
            if (this.Kind == Cellkind.BadVision) g.DrawImage(image_badvision, Rect);
        }

        public virtual void TimeToMix() {}
    }
}
