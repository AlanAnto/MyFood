namespace MyFood.Models
{
    public class FoodOrder
    {
        public int Id { get; set; }

        public Order ItemOrder { get; set; }

        [Required]
        [ForeignKey(nameof(ItemOrder))]
        public int OrderId { get; set; }

        public Food Food { get; set; }

        [Required]
        [ForeignKey(nameof(Food))]
        public int FoodId { get; set;}

        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public double Amount { get; set; }
    }
}
