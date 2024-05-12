namespace smtAzal.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
    }
}