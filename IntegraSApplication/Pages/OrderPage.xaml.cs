﻿using IntegraSApplication.DB;
using IntegraSApplication.DB.Entitys;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        Order order;
        public OrderPage(Service service)
        {
            order = new Order { Service = service };
            InitializeComponent();
            DataContext = order;
        }

        private void OrderClick(object sender, RoutedEventArgs e)
        {
            order.User = User.userAunt;
            order.OrderDate = DateTime.Now;
            EFModel.Init().Orders.Add(order);
            EFModel.Init().SaveChanges();
        }
    }
}
