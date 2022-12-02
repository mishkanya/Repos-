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
using TulaEducation.Entitys;
using TulaEducation.Pages;

namespace TulaEducation.Controllers
{
    public partial class EducationInfoController : UserControl
    {
        private EducationInfo _info;
        public EducationInfoController(EducationInfo info)
        {
            InitializeComponent();
            _info = info;
            Name.Text = info.Name;
            Phone.Text = $"Номер телефона: {info.PhoneNubber}";
            Email.Text = $"Email: {info.Emain}";
            Location.Text = info.Location;
        }

        public bool GetAllText(string text)
        {
            var allText = (_info.Name + _info.Emain + _info.Location + _info.PhoneNubber).ToLower() ;

            var res = allText.Contains(text.ToLower());

            return res;
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWorker.MainFrame.ViewNewPage(new EducationInfoAddOrChangePage(_info));
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите удалить эту запись?", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(res == MessageBoxResult.Yes)
            {
                MainWindow.MainWorker.DataBase.Delete<EducationInfo>(_info.Id);
                MainWindow.MainWorker.MainFrame.FrameGoBack();
            }
        }

    }
}
