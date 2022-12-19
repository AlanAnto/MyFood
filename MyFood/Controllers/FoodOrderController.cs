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

        [HttpGet("GetCart")]
        [ProducesResponseType(typeof(IEnumerable<FoodOrder>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FoodOrder>>> GetCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var addedFood = await _db.FoodOrders.Where(m => (m.UserId == user.Id) && (!m.OrderPlaced)).ToListAsync();
            if (addedFood == null)
            {
                return BadRequest();
            }
            return addedFood;
        }

        [HttpPost("PlaceToCart")]
        [ProducesResponseType(typeof(FoodOrder),StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var orderItem = await _db.FoodOrders.FindAsync(id);
            _db.Remove(orderItem);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
