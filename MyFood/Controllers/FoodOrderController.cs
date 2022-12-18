using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodOrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public FoodOrderController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost("PlaceToCart")]
        public async Task<IActionResult> PlaceFoodToCart(FoodOrderModel model)
        {
            var food = await _db.Foods.FindAsync(model.FoodId);
            var user = await _userManager.GetUserAsync(User);
            var orderItem = new FoodOrder()
            {
                UserId = user.Id,
                FoodId = model.FoodId,
                Quantity = model.Quantity,
                Amount = model.Price * model.Quantity,
                OrderPlaced = false
            };
            await _db.AddAsync(orderItem);
            await _db.SaveChangesAsync();
            return Ok(model);
        }

        [HttpDelete("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var orderItem = await _db.FoodOrders.FindAsync(id);
            _db.Remove(orderItem);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
