using IntegraSApplication.DB;
using System.Windows;
using System.Windows.Controls;

namespace IntegraSApplication.Windowses
{
    /// <summary>
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            if(!string.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.Language);
            }

            InitializeComponent();

            CbLanguage.ItemsSource = new System.Globalization.CultureInfo[]
            {
                System.Globalization.CultureInfo.GetCultureInfo("ru-RU"),
                System.Globalization.CultureInfo.GetCultureInfo("en"),
            };

            CbLanguage.SelectedValue = Properties.Settings.Default.Language;
            
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            if (User.AutroizationUser(loginTb.Text, passTb.Password) != null)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                Close(); 

            }
            else
                MessageBox.Show("Ошибка ! Неверный логин или пароль");
        }

        private void ChangedClick(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.Language = CbLanguage.SelectedValue.ToString();
            Close();
            
        }

        private void ClosedWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Language = CbLanguage.SelectedValue.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
