namespace MyFood.Models
{
    public enum FoodTypes
    {
        [Display(Name = "Vegetarian")]
        Veg,

        [Display(Name = "Non-Vegetarian")]
        NonVeg,

        [Display(Name = "Drinks")]
        Beverages
    }
    public class Food
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public FoodTypes FoodType { get; set;}

        [Required]
        public double Price { get; set; }

        [Required]
        public bool Availability { get; set; }
    }
}
