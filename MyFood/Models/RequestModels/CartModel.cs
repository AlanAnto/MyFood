namespace MyFood.Models.RequestModels
{
    public class CartModel
    {
        public string FoodName { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public bool OrderPlaced { get; set; }
    }
}
