using IntegraSApplication.DB;
using IntegraSApplication.DB.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace IntegraSApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        public  MainMenuPage()
        {
            InitializeComponent();

            InitComboBox();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += timer_TickAsync;
            timer.Start(); 
        }
        
        private void InitComboBox()
        {
            List<Category> categories = EFModel.Init().Categories.ToList();
            CbCategory.ItemsSource = categories;
            CbCategory.SelectedIndex = 0;

            CbSort.Items.Add("По возрастанию");
            CbSort.Items.Add("По убыванию");
            CbSort.SelectedIndex = 0;

        }

        private void timer_TickAsync(object sender, EventArgs e)
        {
           AsyncUpdateData();
        }
        private void AsyncUpdateData()
        {
            IEnumerable<Service> services = EFModel.Init().Services.Where(u=> u.NameService.Contains(TbSearch.Text));
            if (Dispatcher.Invoke(() => CbCategory.SelectedIndex > 0))
                services = services.Where(u => u.CategoryID == (CbCategory.SelectedItem as Category).ID);

            switch (CbSort.SelectedIndex) 
            {
                case 0:
                    services = services.OrderBy(u => u.Cost);
                    break;
                case 1:
                    services = services.OrderByDescending(u => u.Cost);
                    break;
            }



            Dispatcher.Invoke(() => LVService.ItemsSource = services.ToList());
        }
        /*
        private async Task UpdateData()
        {
            await Task.Run(() => AsyncUpdateData());
           
        }
        */
        private void CbUpdateClick(object sender, SelectionChangedEventArgs e)
        {
            AsyncUpdateData();
        }

        private void OrderClick(object sender, RoutedEventArgs e)
        {
            Service service = (sender as Button).DataContext as Service;
            NavigationService.Navigate(new OrderPage(service));

        }

        private void ChangeTextClick(object sender, TextChangedEventArgs e)
        {
            AsyncUpdateData();
        }
    }
}
