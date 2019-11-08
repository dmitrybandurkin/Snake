using System;
using System.Drawing;
using System.Media;

namespace Snake
{
    internal enum Cellkind
    {
        Head,
        Tail,
        Food,
        Speed,
        Vision,
        BadVision,
        Meat,
        Empty
    }

    internal class Cells
    {
        public bool Inaction { get; set; }

        private const int cell_size = 10;
        private Rectangle rect;
        public Rectangle Rect
        {
            get => rect;
            set => rect = value;
        }

        private static Bitmap image_head = new Bitmap("head.png");
        private static Bitmap image_tail = new Bitmap("tail.png");
        private static Bitmap image_mouse = new Bitmap("mouse.png");
        private static Bitmap image_speed = new Bitmap("speed.png");
        private static Bitmap image_vision = new Bitmap("vision.png");
        private static Bitmap image_badvision = new Bitmap("badvision.png");
        private static Bitmap image_meat = new Bitmap("meat.png");

        private static SoundPlayer sp1 = new SoundPlayer("sound.wav");
        private static SoundPlayer sp2 = new SoundPlayer("drink.wav");

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
            if (Kind == Cellkind.Food) sp1.Play();
            else sp2.Play();

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

            if (Kind == Cellkind.Head) g.DrawImage(image_head, Rect);
            if (Kind == Cellkind.Tail) g.DrawImage(image_tail, Rect);
            if (Kind == Cellkind.Food) g.DrawImage(image_mouse, Rect);
            if (Kind == Cellkind.Speed) g.DrawImage(image_speed, Rect);
            if (Kind == Cellkind.Vision) g.DrawImage(image_vision, Rect);
            if (Kind == Cellkind.BadVision) g.DrawImage(image_badvision, Rect);
            if (Kind == Cellkind.Meat) g.DrawImage(image_meat, Rect);
        }
    }
}
