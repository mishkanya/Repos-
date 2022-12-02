using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tets
{
    public partial class Form1 : Form
    {
        //Инициализация bitmap и графики
        Bitmap bmp;
        Graphics g;

        int param = 0;
        Point[] center = new Point[8];//координаты центров
        Point[] newpoint = new Point[8];//координаты изменяющейся точки на окружности
        int s = 150;//диаметр окружности
        double alpha = Math.PI / 4;//угол на который изменяется центр каждой окружности
        public Form1(bool tet)
        {
            InitializeComponent();
        }
        //функция вращения точки вокруг точки p
        private Point rotate(Point p, int R)
        {
            Point res = Point.Empty;

            res.X = (int)(p.X - R * Math.Cos(alpha));
            res.Y = (int)(p.Y + R * Math.Sin(alpha));
            return res;
        }
        //функция прорисовки
        private void drawing(int j)
        {
            Point p = Point.Empty;
            //Создаем bitmap и графику из bitmap
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            //Создаем объекты для закрашивания фигур
            SolidBrush black = new SolidBrush(Color.Black);
            SolidBrush white = new SolidBrush(Color.White);
            Pen myPen = new Pen(Color.White, 2);
            Point a = Point.Empty;

            //координаты центра
            p.X = pictureBox1.Width / 2 - s / 2;
            p.Y = pictureBox1.Height / 2 - s / 2;
            //заливка фона
            g.FillRectangle(black, 0, 0, pictureBox1.Width, pictureBox1.Height);
            //задание радиуса для построения всех окружностей
            int R = pictureBox1.Width / 2 - s / 2;
            for (int i = 0; i < 8; i++)
            {
                alpha = i * Math.PI / 4;//увелечение угла вращения относительно начала
                a = rotate(p, R);//получение новых координат
                //задание центров
                center[i].X = a.X + s / 2;
                center[i].Y = a.Y + s / 2;
                //рисование окружности
                g.DrawEllipse(myPen, a.X, a.Y, s, s);
            }
            //цикл для движения точки по каждой окружности
            for (int i = 0; i < 8; i++)
            {
                alpha = ((i % 4) * 20 + j) * Math.PI / 60;//изменение угла вращения для изменения скорости каждой точки
                a = rotate(center[i], s / 2);//вращение точки по окружности
                newpoint[i] = a;//запоминание новых координат точки на окружности
                g.FillEllipse(white, a.X - 3, a.Y - 3, 6, 6);//рисование точки на окружности
                                                             //рисование линий между всеми парами точек
                for (int p1 = 0; p1 < 8; p1++)
                {
                    for (int p2 = p1; p2 < 8; p2++)
                    {
                        g.DrawLine(myPen, newpoint[p1], newpoint[p2]);
                    }
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //запуск таймера
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //изменение параметра
            param++;
            param %= 120;
            drawing(param);//рисование
            //отображаем bitmap на PictureBox
            pictureBox1.Image = bmp;
        }

        private PictureBox pictureBox1;
        private Timer timer1;
        private IContainer components;
    }
}