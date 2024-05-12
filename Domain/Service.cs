namespace smtAzal.Domain
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ServiceTypeId { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}