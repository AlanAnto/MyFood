namespace MyFood.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public FoodController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// This method is used to display the list of all food items from the database.
        /// </summary>
        /// <returns>Returns a list of all food items</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Food>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Food>>> Foods()
        {
          
            if (_db.Foods == null)
            {
                 return NotFound();
            }
            var foods =  await _db.Foods.ToListAsync();
            return Ok(new ResponseModel<IEnumerable<Food>>()
            {
                Data = foods
            });
        }


        /// <summary>
        /// This method is used to get the master details of food items by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an object of Food</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
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


        /// <summary>
        ///  This method is used to get the details of food items when searched by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns an object of Food</returns>
        [HttpGet("name/{name}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Food>),StatusCodes.Status200OK)]
        public async Task<ActionResult<Food>> SearchFood(string name)
        {
            if (_db.Foods == null)
            {
                return NoContent();
            }
            var food = await _db.Foods.Where(m => (m.Name.Contains(name))).ToListAsync();

            if (food == null)
            {
                return NotFound(new ResponseModel<string>()
                {
                    Success = false,
                    Message = "No such food item is Found",
                });
            }
            return Ok(new ResponseModel<IEnumerable<Food>>()
            {
                Data = food,
            });
        }

        
        /// <summary>
        /// This method is used to update food details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="food"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Foods(int id, Food food)
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

            return Ok();
        }

        
        /// <summary>
        /// This method is for adding a food to the database.
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Food>> Foods(Food food)
        {
          if (_db.Foods == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
          }
            _db.Foods.Add(food);
            await _db.SaveChangesAsync();
            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        
        /// <summary>
        ///  This method is for deleting a food item from the database by the id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            return Ok();
        }


        private bool FoodExists(int id)
        {
            return (_db.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
