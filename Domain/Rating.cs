namespace smtAzal.Domain
{
    public class Rating
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}