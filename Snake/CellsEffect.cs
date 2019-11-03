using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snake
{
    class CellsEffect:Cells
    {
        Timer period_mix;
        Timer period_act;
        public string Txt { get; set; }

        private Cellkind init_kind;
        public CellsEffect(int x, int y, Cellkind kind):base(x, y)
        {
            Inaction = false;
            Txt = "";

            Kind = kind;
            init_kind = kind;

            period_mix = new Timer() { Interval = 100, Enabled = false };
            period_mix.Elapsed += Mixing;

            period_act = new Timer() { Interval = 100, Enabled = false };
            period_act.Elapsed += Act;
        }

        public override void Place(Snake obj)
        {
            base.Place(obj);
            Kind = Cellkind.Empty;
            Mixing_period = 0;
            Act_period = 0;
            period_mix.Enabled = true;
            period_mix.Start();
            period_act.Enabled = true;
            period_act.Start();
            Inaction = true;
            if (init_kind == Cellkind.Speed) Txt = "Многоножка";
            if (init_kind == Cellkind.Vision) Txt = "Прозрение";
            if (init_kind == Cellkind.BadVision) Txt = "Всевидящее око";
        }
        private void Mixing(object sender, ElapsedEventArgs e)
        {
            if (Mixing_period < 100) Mixing_period++;
            else
            {
                period_mix.Enabled = false;
                period_mix.Stop();
                Mixing_period = 0;
                Kind = init_kind;
                X = rnd.Next(10, 60) * 10;
                Y = rnd.Next(10, 35) * 10;
            }
        }

        private void Act(object sender, ElapsedEventArgs e)
        {
            if (Act_period < 50) Act_period++;
            else
            {
                period_act.Enabled = false;
                period_act.Stop();
                Act_period = 0;
                Inaction = false;
                Txt = "";
                X = rnd.Next(10, 60) * 10;
                Y = rnd.Next(10, 35) * 10;
            }
        }

    }
}
