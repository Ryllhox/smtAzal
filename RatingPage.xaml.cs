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
    /// Логика взаимодействия для RatingPage.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        private ApplicationDbContext _context;

        public RatingPage()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadServicesForRating();
        }

        private void LoadServicesForRating()
        {
            var completedAppointments = _context.Appointments
                .Where(a => a.UserId == CurrentUser.UserId && (a.Status == "Done" || a.Status == "Rejected"))
                .Select(a => a.Service)
                .Distinct()
                .ToList();

            cmbServices.ItemsSource = completedAppointments;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (cmbServices.SelectedItem != null && !string.IsNullOrEmpty(txtComment.Text))
            {
                var selectedService = cmbServices.SelectedItem as Service;

                var appointment = _context.Appointments
                    .FirstOrDefault(a => a.ServiceId == selectedService.Id && a.UserId == CurrentUser.UserId &&
                                         (a.Status == "Done" || a.Status == "Rejected"));

                if (appointment != null)
                {
                    var ratingValue = (int)sliderRating.Value;

                    var newRating = new Rating
                    {
                        AppointmentId = appointment.Id,
                        RatingValue = ratingValue,
                        Comment = txtComment.Text
                    };

                    _context.Ratings.Add(newRating);
                    _context.SaveChanges();

                    MessageBox.Show("спс за фидбек!");
                }
                else
                {
                    MessageBox.Show("вы можете оценить только отклоненные и выполненные услуги.");
                }
            }
            else
            {
                MessageBox.Show("пж выберите услугу и оставьте коммент.");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }
    }
}
