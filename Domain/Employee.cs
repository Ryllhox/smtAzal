namespace smtAzal.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SalonId { get; set; }

        public virtual Salon Salon { get; set; }
    }
}