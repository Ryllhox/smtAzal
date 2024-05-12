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
    /// Логика взаимодействия для AdminAppointmentPage.xaml
    /// </summary>
    public partial class AdminAppointmentPage : Page
    {
        public AdminAppointmentPage()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            using (var context = new ApplicationDbContext())
            {
                lvAppointments.ItemsSource = context.Appointments.Include("User").Include("Service").ToList();
            }
        }

        private void SetDone_Click(object sender, RoutedEventArgs e)
        {
            if (lvAppointments.SelectedItem != null)
            {
                var appointment = lvAppointments.SelectedItem as Appointment;
                if (appointment.Status != "Done")
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var dbAppointment = context.Appointments.Find(appointment.Id);
                        if (dbAppointment != null)
                        {
                            dbAppointment.Status = "Done";
                            context.SaveChanges();
                            MessageBox.Show("Статус изменен до выполненного");
                            LoadAppointments();
                        }
                    }
                }
            }
        }

        private void SetRejected_Click(object sender, RoutedEventArgs e)
        {
            if (lvAppointments.SelectedItem != null)
            {
                var appointment = lvAppointments.SelectedItem as Appointment;
                if (appointment.Status != "Rejected")
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var dbAppointment = context.Appointments.Find(appointment.Id);
                        if (dbAppointment != null)
                        {
                            dbAppointment.Status = "Rejected";
                            context.SaveChanges();
                            MessageBox.Show("Статус изменен на отклоненный");
                            LoadAppointments();
                        }
                    }
                }
            }
        }
    }
}
