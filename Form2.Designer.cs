using System;
using System.Drawing;
using System.Windows.Forms;

namespace tets
{
    partial class Form2 : Form
    {
        Graphics g;
        Bitmap bmp;
        double ang = Math.PI; //угол относительно центра 
        double bang = 0; //угол для прорисовки внешних дуг
        double sang = 0; //угол для прорисовки внутренних дуг
        double da = (Math.PI) * 2 / 8;
        int x, y;

        public Form2(bool tet)
        {
            InitializeComponent();
        }

        void Circle(Color col, int x, int y, int r)
        {
            g.FillEllipse(new SolidBrush(col), x + r, y - r, 2 * r, 2 * r);
            g.DrawEllipse(new Pen(Color.Black), x + r, y - r, 2 * r, 2 * r);

        }
        //одна из внутренних дуг(часть окружности с центром в точке (х0,у0))
        //дуга - цепочка из 13 кругов
        void Small(Color col, int x0, int y0, double ang)
        {
            double da = (Math.PI) / 13;
            int x, y; //(x,y)- центр маленького круга 

            for (int i = 1; i <= 13; i++)
            {
                x = (int)(Math.Cos(ang) * 50 + x0);
                y = (int)(Math.Sin(ang) * 50 + y0);
                ang += da;//меняем угол для прорисовки следующего
                Circle(col, x, y, i / 2);
                if (col == Color.White) col = Color.Black;
                else col = Color.White;
            }


        }
        //одна из внешних дуг(часть окружности с центром в точке (х0,у0))
        //дуга - цепочка из 13 кругов
        void Big(Color col, int x0, int y0, double ang)
        {
            double da = (Math.PI) / 20;
            int x, y;

            for (int i = 1; i <= 13; i++)
            {
                //(x,y)- центр маленького круга
                x = (int)(Math.Cos(ang) * 90 + x0);
                y = (int)(Math.Sin(ang) * 90 + y0);
                ang += da;//меняем угол для прорисовки следующего
                Circle(col, x, y, i / 2);
                //чередуем цвета
                if (col == Color.White) col = Color.Black;
                else col = Color.White;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Создаем bitmap и графику из bitmap
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);

            sang += (Math.PI) * 2 / 32; //изменение угла поворота внутренней дуги          
            bang += (Math.PI) * 2 / 64;//изменение угла поворота внешней дуги
            //8 внутренних дуг
            for (int i = 0; i < 8; i++)
            {
                ang += da;//меняем угол для прорисовки каждой из 8 дуг
                sang += da;//меняем угол для поворота каждой дуги вокруг своего центра

                x = (int)(Math.Cos(ang) * 105 + pictureBox1.Width / 2);
                y = (int)(Math.Sin(ang) * 105 + pictureBox1.Height / 2);
                if (i % 2 == 0)
                    Small(Color.Black, x, y, sang);
                else
                    Small(Color.White, x, y, sang);
            }
            //8 внешних дуг
            for (int i = 0; i < 8; i++)
            {
                ang += da;
                bang += da;

                x = (int)(Math.Cos(ang + (Math.PI) / 8) * 120 + pictureBox1.Width / 2);
                y = (int)(Math.Sin(ang + (Math.PI) / 8) * 120 + pictureBox1.Height / 2);
                if (i % 2 == 0)
                    Big(Color.White, x, y, bang);
                else
                    Big(Color.Black, x, y, bang);
            }

            pictureBox1.Image = bmp;//отображаем bitmap на PictureBox 
        }

        private Timer timer1;
        private System.ComponentModel.IContainer components;
        private PictureBox pictureBox1;
    }
}