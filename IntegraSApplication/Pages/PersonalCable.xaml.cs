using IntegraSApplication.DB;
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

namespace IntegraSApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalCable.xaml
    /// </summary>
    public partial class PersonalCable : Page
    {
        public PersonalCable()
        {
            InitializeComponent();
            DataContext = User.userAunt;
            weather.Text = string.Format("Температура в Самаре {0} °C", GetAPIWeather.GetWeather().ToString());
        }

        private void EditDataClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPesonalData(User.userAunt));
        }
    }
}
