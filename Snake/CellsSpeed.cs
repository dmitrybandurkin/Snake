using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snake
{
    class CellsSpeed:Cells
    {
        Timer to_next_speed;
        Random rnd;
        public CellsSpeed(int x, int y, Cellkind kind) : base(x, y, kind) 
        {
            rnd = new Random();
            to_next_speed = new Timer() { Interval = 100, Enabled = false };
            to_next_speed.Elapsed += Mixing;
        }

        public override void Place(Snake obj)
        {
            base.Place(obj);
            Kind = Cellkind.Empty;
            Mixing_period = 0;
            to_next_speed.Enabled = true;
            to_next_speed.Start();
        }

        private void Mixing(object sender, ElapsedEventArgs e)
        {
            if (Mixing_period < 10) Mixing_period++;
            else
            {
                to_next_speed.Enabled = false;
                to_next_speed.Stop();
                Mixing_period = 0;
                Kind = Cellkind.Speed;
                X = rnd.Next(10, 60) * 10;
                Y = rnd.Next(10, 35) * 10;
            }
        }

    }
}
