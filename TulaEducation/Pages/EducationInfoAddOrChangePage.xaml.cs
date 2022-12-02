using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TulaEducation.Entitys;

namespace TulaEducation.Pages
{
    /// <summary>
    /// Логика взаимодействия для EducationInfoAddOrChangePage.xaml
    /// </summary>
    public partial class EducationInfoAddOrChangePage : Page
    {
        private EducationInfo _info;
        public EducationInfoAddOrChangePage()
        {
            InitializeComponent();
            _info = new EducationInfo()
            {
                Id = default
            };
        }
        public EducationInfoAddOrChangePage(EducationInfo info)
        {
            InitializeComponent();
            _info = info;
            Name.Text = info.Name;
            Phone.Text = info.PhoneNubber;
            Email.Text = info.Emain;
            Location.Text = info.Location;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWorker.MainFrame.FrameGoBack();
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (Phone.Text == "" || Email.Text == "" || Name.Text == "" || Location.Text == "")
            {
                MessageBox.Show("Не все данные введены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if(Email.Text.Contains("@") == false || Email.Text.Contains(".") == false)
            {
                MessageBox.Show("Почта введена не правильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            _info.Emain = Email.Text;
            _info.PhoneNubber = Phone.Text;
            _info.Location = Location.Text;
            _info.Name = Name.Text;
            if (_info.Id == default)
                MainWindow.MainWorker.DataBase.Insert(_info, out string m);
            else
                MainWindow.MainWorker.DataBase.ChangeData<EducationInfo>(_info);
            MainWindow.MainWorker.MainFrame.FrameGoBack();
        }

        private void PhoneInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+()-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
