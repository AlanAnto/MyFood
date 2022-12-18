namespace MyFood.Models
{
    public class FoodOrder
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public Food Food { get; set; }

        [Required]
        [ForeignKey(nameof(Food))]
        public int FoodId { get; set;}

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public bool OrderPlaced { get; set; }

        public Order? Order { get; set; }

        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
    }
}
