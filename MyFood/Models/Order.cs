namespace MyFood.Models
{
    public class Order
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [StringLength(100)]
        public string? Address { get; set; } = string.Empty;
    }
}
