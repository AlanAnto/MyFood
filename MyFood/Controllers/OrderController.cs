using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext db,UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var addedFood = await _db.FoodOrders.Where(m => (m.UserId == user.Id)&&(!m.OrderPlaced)).ToListAsync();
            double totalPrice = 0; 
            foreach (var item in addedFood)
            {
                totalPrice += item.Amount;
                item.OrderPlaced = true;
            }
            var order = new Order()
            {
                UserId = user.Id,
                TotalPrice = totalPrice,
            };
            await _db.AddAsync(order);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
