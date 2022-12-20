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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> ShowOrder(int id)
        {
            if (_db.Orders == null)
            {
                return NotFound();
            }
            var order = await _db.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        //[HttpGet]
        //public async Task<IActionResult> ViewOrders()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var orders = await _db.Orders.Where(m => m.UserId == user.Id).ToListAsync();
        //    foreach (var item in orders)
        //    {

        //    }
            
        //}

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderModel model)
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
                OrderDate = DateTime.Now,
                Date = model.Date,
                OrderLocation = model.OrderLocation
            };
            await _db.AddAsync(order);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
