using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TulaEducation.Controllers;
using TulaEducation.Entitys;

namespace TulaEducation.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        private IEnumerable<EducationInfoController> _educationInfoControllers;

        public IEnumerable<EducationInfoController> EducationInfoControllers { get => EducationInfoControllers1; set => EducationInfoControllers1 = value; }
        public IEnumerable<EducationInfoController> EducationInfoControllers1 { get => _educationInfoControllers; set => _educationInfoControllers = value; }

        public ListPage()
        {
            InitializeComponent();
            Load();
            MainWindow.MainWorker.MainFrame.Update += Load;
        }
        private void Load()
        {
            Container.Children.Clear();
            List<EducationInfoController> educationInfoControllers = new List<EducationInfoController>();
            foreach (var item in MainWindow.MainWorker.DataBase.GetEntitys<EducationInfo>())
            {
                educationInfoControllers.Add(new EducationInfoController(item));
            }
            EducationInfoControllers1 = educationInfoControllers;
            foreach (var item in educationInfoControllers)
            {
                Container.Children.Add(item);
            }
        }
        private void Search(string t)
        {
            foreach (var item in EducationInfoControllers1)
            {
                    item.Visibility = item.GetAllText(t) ? Visibility.Visible: Visibility.Collapsed;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search((sender as TextBox).Text);
        }

        private void AddNewInfo(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWorker.MainFrame.ViewNewPage(new EducationInfoAddOrChangePage());
        }
    }
}
