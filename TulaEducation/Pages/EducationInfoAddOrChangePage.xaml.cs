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

        public EducationInfo Info { get => Info1; set => Info1 = value; }
        public EducationInfo Info1 { get => _info; set => _info = value; }

        public EducationInfoAddOrChangePage()
        {
            InitializeComponent();
            Info1 = new EducationInfo()
            {
                Id = default
            };
        }
        public EducationInfoAddOrChangePage(EducationInfo info)
        {
            InitializeComponent();
            Info1 = info;
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
            Info1.Emain = Email.Text;
            Info1.PhoneNubber = Phone.Text;
            Info1.Location = Location.Text;
            Info1.Name = Name.Text;
            if (Info1.Id == default)
                MainWindow.MainWorker.DataBase.Insert(Info1, out string m);
            else
                MainWindow.MainWorker.DataBase.ChangeData<EducationInfo>(Info1);
            MainWindow.MainWorker.MainFrame.FrameGoBack();
        }

        private void PhoneInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+()-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
