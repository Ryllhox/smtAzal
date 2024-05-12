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
    /// Логика взаимодействия для AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        private int _currentUserId;
        private Service _selectedService;
        private Salon _selectedSalon;
        private ApplicationDbContext _context;

        public AppointmentPage(Service selectedService, Salon selectedSalon)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _selectedService = selectedService;
            _selectedSalon = selectedSalon;
            _currentUserId = CurrentUser.UserId;

            LoadTimeSlots(DateTime.Today);
        }

        private void LoadTimeSlots(DateTime date)
        {
            // Получаем доступные временные слоты для выбранной даты
            var availableTimeSlots = GetAvailableTimeSlots(date);

            cmbTimeSlots.ItemsSource = availableTimeSlots;
        }

        private List<string> GetAvailableTimeSlots(DateTime date)
        {
            // Получаем все услуги, предоставляемые выбранным салоном
            var salonServices = _selectedSalon.Services.Select(s => s.Id).ToList();

            // Получаем все записи на приемы для услуг, предоставляемых выбранным салоном на выбранную дату
            var appointmentsOnDate = _context.Appointments
                .Where(a => salonServices.Contains(a.ServiceId) && a.DateTime.Date == date.Date)
                .ToList();

            // Получаем все возможные временные слоты на выбранную дату
            var allTimeSlots = Enumerable.Range(9, 10) // 9:00 - 18:00
                .Select(hour => new DateTime(date.Year, date.Month, date.Day, hour, 0, 0))
                .ToList();

            // Удаляем временные слоты, которые уже заняты
            foreach (var appointment in appointmentsOnDate)
            {
                var timeSlot = allTimeSlots.FirstOrDefault(t => t == appointment.DateTime);
                if (timeSlot != default(DateTime))
                {
                    allTimeSlots.Remove(timeSlot);
                }
            }

            // Преобразуем оставшиеся временные слоты в строки времени для отображения в ComboBox
            var availableTimeSlots = allTimeSlots.Select(time => time.ToString("hh:mm tt")).ToList();

            return availableTimeSlots;
        }



        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDate = calendar.SelectedDate ?? DateTime.Today;
            LoadTimeSlots(selectedDate);
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTimeSlots.SelectedItem != null)
            {
                var selectedTime = cmbTimeSlots.SelectedItem.ToString();
                var selectedDate = calendar.SelectedDate ?? DateTime.Today;
                var appointmentDateTime = DateTime.Parse(selectedTime);

                // Получаем идентификатор текущего пользователя
                int userId = CurrentUser.UserId;

                // Создаем новую запись на услугу
                var newAppointment = new Appointment
                {
                    UserId = userId,
                    ServiceId = _selectedService.Id,
                    DateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day,
                                            appointmentDateTime.Hour, appointmentDateTime.Minute, 0),
                    Status = "Booked" // Измените этот статус, если нужно
                };

                _context.Appointments.Add(newAppointment);
                _context.SaveChanges();

                MessageBox.Show("Запись успешно забронирована!");
            }
            else
            {
                MessageBox.Show("пж нажми на этот дейтпикер и тыкни дату+время !!!!.");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }
    }
}
