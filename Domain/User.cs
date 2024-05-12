namespace smtAzal.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public int? DiscountId { get; set; }
        public int? SalonId { get; set; } // Добавляем ссылку на салон

        public virtual Role Role { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual Salon Salon { get; set; } // Добавляем навигационное свойство
    }
}