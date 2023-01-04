namespace MyFood.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string? LastName { get; set; }

        public IEnumerable<FoodOrder> FoodItemOrders { get; set; }
    }
}
