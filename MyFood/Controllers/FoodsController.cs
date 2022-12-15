namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public FoodsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
          if (_db.Foods == null)
          {
              return NotFound();
          }
            return await _db.Foods.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
          if (_db.Foods == null)
          {
              return NotFound();
          }
            var food = await _db.Foods.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // PUT: api/Foods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            _db.Entry(food).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Foods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
          if (_db.Foods == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
          }
            _db.Foods.Add(food);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (_db.Foods == null)
            {
                return NotFound();
            }
            var food = await _db.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _db.Foods.Remove(food);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodExists(int id)
        {
            return (_db.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
