using Microsoft.EntityFrameworkCore;
using smtAzal.Domain;
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

namespace smtAzal
{
    /// <summary>
    /// Логика взаимодействия для AdminServicePage.xaml
    /// </summary>
    public partial class AdminServicePage : Page
    {
        private readonly ApplicationDbContext _context;
        public AdminServicePage()
        {
            InitializeComponent();
            LoadServices();
            _context = new ApplicationDbContext();
        }

        private void LoadServices()
        {
            using (var context = new ApplicationDbContext())
            {
                lvServices.ItemsSource = context.Services.ToList();
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddServicePage(_context));
            LoadServices(); // После добавления услуги обновляем список
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (lvServices.SelectedItem != null)
            {
                var selectedService = lvServices.SelectedItem as Service;
                using (var context = new ApplicationDbContext())
                {
                    var dbService = context.Services.Find(selectedService.Id);
                    if (dbService != null)
                    {
                        context.Services.Remove(dbService);
                        context.SaveChanges();
                        MessageBox.Show("Услуга успешно удалена. Вы бессердечны.");
                        LoadServices();
                    }
                }
            }
        }
    }
}
