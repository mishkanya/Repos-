using System.Collections.Generic;
using System.Windows;
using TulaEducation.Entitys;
using TulaEducation.Pages;

namespace TulaEducation
{
    public partial class MainWindow : Window
    {
    public static MainWorker MainWorker { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            MainWorker = new MainWorker(MainFrame);
            MainWorker.MainFrame.ViewNewPage(new ListPage());
        }
       
    }
}
