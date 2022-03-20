using IntegraSApplication.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
    /// Логика взаимодействия для EditPesonalData.xaml
    /// </summary>
    public partial class EditPesonalData : Page
    {
        User users = new User();
        public EditPesonalData(User user)
        {
            InitializeComponent();
            users = user;
            DataContext = users;
        }

        private void SelectPicturClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                users.MainImage = File.ReadAllBytes(openFileDialog.FileName);

        }

        private void EditDataClick(object sender, RoutedEventArgs e)
        {
            try
            {
                EFModel.Init().SaveChanges();
                MessageBox.Show("Данные были успешно изменены!");

            }
            catch (DbEntityValidationException epx)
            {
                MessageBox.Show(string.Join(",", epx.EntityValidationErrors.Last().ValidationErrors.Select(u => u.ErrorMessage)));
            }
        }
    }
}
