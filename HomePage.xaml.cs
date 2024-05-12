using Microsoft.EntityFrameworkCore;
using smtAzal.Domain;
using smtAzal.Utilities;
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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private ApplicationDbContext _context;

        public HomePage()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();

            LoadServices();
        }

        private void LoadServices()
        {
            cmbServices.ItemsSource = _context.Services.ToList();
            cmbServices.DisplayMemberPath = "Name";
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if (cmbServices.SelectedItem != null)
            {
                var selectedService = cmbServices.SelectedItem as Service;

                var salons = _context.Salons
                    .Include(s => s.Services)
                    .Where(s => s.Services.Any(serv => serv.Id == selectedService.Id))
                    .ToList();

                lstSalons.ItemsSource = salons;
                lstSalons.DisplayMemberPath = "Name";
            }
        }

        private void BookService_Click(object sender, RoutedEventArgs e)
        {
            if (cmbServices.SelectedItem != null && lstSalons.SelectedItem != null)
            {
                var selectedService = cmbServices.SelectedItem as Service;
                var selectedSalon = lstSalons.SelectedItem as Salon;

                var appointmentPage = new AppointmentPage(selectedService, selectedSalon);
                NavigationService.Navigate(appointmentPage);
            }
            else
            {
                MessageBox.Show("туда сюда делай да понял услуга и салон.");
            }
        }

        private void RateService_Click(object sender, RoutedEventArgs e)
        {
            // Перенаправление на страницу оценки
            RatingPage ratingPage = new RatingPage();
            NavigationService.Navigate(ratingPage);
        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            // Создаем страницу личного кабинета и передаем текущий Id пользователя в конструктор
            var userProfilePage = new UserProfilePage(CurrentUser.UserId);
            NavigationService.Navigate(userProfilePage);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
