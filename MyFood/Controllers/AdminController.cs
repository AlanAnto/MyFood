namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            if (_db.Users == null)
            {
                return NotFound();
            }
            var allUsers = await _db.Users.ToListAsync();
            var users = new List<UpdateModel>();
            foreach (var item in allUsers)
            {
                var user = new UpdateModel()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber
                };
                users.Append(user);
            }
            return Ok(allUsers);
        }

        [HttpPost("AddLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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