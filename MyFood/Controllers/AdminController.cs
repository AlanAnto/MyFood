using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFood.Data;

namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("AdminProfile")]

        public async Task<IActionResult> GetOne()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("UpdateAdmin")]

        public async Task<IActionResult> Update(RegisterModel model)
        {
            var res = await _userManager.GetUserAsync(User);
            if (res == null)
                return NotFound();
            res.FirstName = model.FirstName;
            res.LastName = model.LastName;
            res.Email = model.Email;
            res.PhoneNumber = model.PhoneNumber;


            await _db.SaveChangesAsync();
            return Ok(res);
        }
        [HttpGet("ViewUsers")]
        public async Task<IActionResult> ViewUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return Ok(users);
        }
        [HttpPost("locationadd")]
        public async Task<IActionResult> Location(string location)
        {
            await _db.Locations.AddAsync(new Location()
            {

                LocationName = location,
            });

            await _db.SaveChangesAsync();
            return Ok();
        }
        //[HttpPost("ViewTransaction")]
        //public async Task<IActionResult> ViewTransaction()
        //{

        //}

    
    }

    }
