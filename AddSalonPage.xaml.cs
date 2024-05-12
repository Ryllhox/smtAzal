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
    /// Логика взаимодействия для AddSalonPage.xaml
    /// </summary>
    public partial class AddSalonPage : Page
    {
        private readonly ApplicationDbContext _context;

        public AddSalonPage(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Get values from text boxes
            string name = txtName.Text;
            string address = txtAddress.Text;
            double rating = double.Parse(txtRating.Text);
            string description = txtDescription.Text;
            string workingHours = txtWorkingHours.Text;

            // Create new salon
            var newSalon = new Salon
            {
                Name = name,
                Address = address,
                Rating = rating,
                Description = description,
                WorkingHours = workingHours
            };

            // Add the salon to the database
            _context.Salons.Add(newSalon);
            _context.SaveChanges();

            MessageBox.Show("Новая победа капитализма!");
            NavigationService.Navigate(new AdminPage());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }
    }
}
