﻿namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public FoodController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET : Foods
        [HttpGet("Menu")]
        [ProducesResponseType(typeof(IEnumerable<Food>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
          
            if (_db.Foods == null)
            {
                 return NotFound();
            }
            var menu =  await _db.Foods.ToListAsync();
            return Ok(new ResponseModel<IEnumerable<Food>>()
            {
                Data = menu
            });
        }

        // GET : Food/id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Food),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
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

            return Ok(new ResponseModel<Food>()
            {
                Success = true,
                Data = food,
            });
        }

        // GET : Food/Name
        [HttpGet("{Name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Food>> FoodSearch(string name)
        {
            if (_db.Foods == null)
            {
                return NotFound();
            }
            var food = await _db.Foods.Where(m => (m.Name.Contains(name))).ToListAsync();

            if (food == null)
            {
                return NotFound();
            }
            return Ok(new ResponseModel<IEnumerable<Food>>()
            {
                Data = food,
            });
        }

        // PUT: api/Foods/5

        // PUT: Food/id
        [HttpPut("Edit/{id}")]
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
        [HttpPost("AddFood")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Food>> AddFood(Food food)
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
