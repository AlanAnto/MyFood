namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //Show Cart
        [HttpGet("GetCart")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<FoodOrder>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CartModel>>> GetCart()
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userName);
            var addedFood = await _db.FoodOrders.Where(m => (m.UserId == user.Id) && (!m.OrderPlaced)).ToListAsync();
            if (addedFood == null)
            {
                return BadRequest();
            }
            var cart = new List<CartModel>();
            foreach (var item in addedFood)
            {
                 var foodItem = await _db.Foods.FindAsync(item.FoodId);
                cart.Append(new CartModel()
                {
                    FoodName = foodItem.Name,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    OrderPlaced = item.OrderPlaced
                });
            }
            return Ok( new ResponseModel<IEnumerable<CartModel>>() 
            {
                Data = cart,
            });
        }

        //Place to Cart
        [HttpPost("PlaceToCart")]
        [Authorize]
        [ProducesResponseType(typeof(FoodOrder),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlaceFoodToCart(FoodOrderModel model)
        {
            var food = await _db.Foods.FindAsync(model.FoodId);
            if (food == null) 
            {
                return BadRequest();
            }
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userName);
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
            return Ok();
        }

        //Remove from Cart
        [HttpDelete("RemoveFromCart")]
        [Authorize]
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
