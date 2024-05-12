namespace smtAzal.Domain
{
    public class Salon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public string WorkingHours { get; set; }

        public virtual ICollection<User> Employees { get; set; } // Связь с работниками салона
        public virtual ICollection<Service> Services { get; set; } // Связь с услугами салона
    }
}