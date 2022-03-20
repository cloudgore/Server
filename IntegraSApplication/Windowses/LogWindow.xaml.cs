using IntegraSApplication.DB;
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
using System.Windows.Shapes;

namespace IntegraSApplication.Windowses
{
    /// <summary>
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
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
    }
}
