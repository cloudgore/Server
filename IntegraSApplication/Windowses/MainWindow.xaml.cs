using IntegraSApplication.Pages;
using IntegraSApplication.WatherClasses;
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

namespace IntegraSApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            weather.Text = string.Format("Температура в Самаре {0} °C", GetAPIWeather.GetWeather().ToString());


        }

        private void serviceListBtnClick(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Navigate(new MainMenuPage());

        }

        private void chatBtnClick(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Navigate(new ChatPage());

        }

        private void personalCableClick(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Navigate(new PersonalCable());

        }

        private void MapCLick(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Navigate(new Map());

        }
    }
}
