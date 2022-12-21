using Microsoft.AspNetCore.Authorization;

namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet("ViewUsers")]
        [ProducesResponseType(typeof(IEnumerable<ApplicationUser>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> ViewUsers()
        {
            if (_db.Foods == null)
            {
                return NotFound();
            }
            return await _db.Users.ToListAsync();
        }
        [HttpPost("AddLocation")]
        public async Task<IActionResult> AddLocation(string location)
        {
            await _db.Locations.AddAsync(new Location()
            {
                LocationName = location,
            });
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}