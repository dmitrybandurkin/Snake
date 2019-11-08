using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form2 : Form
    {
        Button nb;
        Label lb;
        Font fnt;
        public Form2()
        {
            InitializeComponent();

            fnt = new Font("Calibri", 12, FontStyle.Bold);

            nb = new Button();
            nb.Text = "ОК";
            Controls.Add(nb);
            nb.Click += Close;

            lb = new Label();
            lb.SetBounds(50, 50, 650, 350);
            lb.Font = fnt;
            lb.Text = "Вас приветствует игра \"Боевая змейка\". Для продолжения нажмите кнопку \"ОК\" в верхнем правом углу\n\n" +
                "Ваша задача - набрать как можно больше очков, поедая мышей. На пути к успеху Вы встретите множество волшебных элексиров, а также сразитесь со злобным змием\n\n" +
                "- Красный круг обозначает зону видимости. Вне этого круга Вам не видны эликсиры и пища.\n\n" +
                "- Эликсиры обладают в одно и то же время как полезными, так и вредными свойствами: увеличивая скорость перемещения, Вы теряете в обзоре и т.п.\n\n" +
                "- Кусать самого себя нельзя - за это Вы потеряете часть очков.\n\n" +
                "- Иногда Вы сможете встретить кусочки мяса злобной змейки. Они приносят больше очков, чем обычная пища, однако сильно замедляют скорость.";
            Controls.Add(lb);
        }

        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
