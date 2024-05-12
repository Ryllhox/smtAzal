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
    /// Логика взаимодействия для AdminSalonPage.xaml
    /// </summary>
    public partial class AdminSalonPage : Page
    {
        private readonly ApplicationDbContext _context;

        public AdminSalonPage()
        {
            InitializeComponent();
            LoadSalons();
            _context = new ApplicationDbContext();
        }

        private void LoadSalons()
        {
            using (var context = new ApplicationDbContext())
            {
                lvSalons.ItemsSource = context.Salons.ToList();
            }
        }

        private void AddSalon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSalonPage(_context));
            LoadSalons();
        }

        private void DeleteSalon_Click(object sender, RoutedEventArgs e)
        {
            if (lvSalons.SelectedItem != null)
            {
                var selectedSalon = lvSalons.SelectedItem as Salon;
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите поставить крест на этом салоне?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var dbSalon = context.Salons.Find(selectedSalon.Id);
                        if (dbSalon != null)
                        {
                            context.Salons.Remove(dbSalon);
                            context.SaveChanges();
                            MessageBox.Show("Салон успешно разрушен. Как и ваша совесть.");
                            LoadSalons();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите салон, работников которых вы хотите обречь на голодную смерть в случае закрытия их салона.");
            }
        }
    }
}
