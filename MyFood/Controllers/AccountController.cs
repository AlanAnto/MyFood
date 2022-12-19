using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace MyFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not Registered.");
                return NotFound();
            }
            var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (!res.Succeeded)
            {
                return BadRequest();
            }
            return Ok("Login Successful");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.FirstName + model.LastName,
            };
            var res = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            if (res.Succeeded)
            {
                return Ok(model);
            }
            return BadRequest();
        }


        
        [HttpGet("userprofile")]
        
        public async Task<IActionResult> GetOne()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user==null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("updateuser")]

        public async Task<IActionResult> Update(ApplicationUser model)
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
        [HttpDelete("deleteuser")]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            _db.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("getroles")]
        public async Task<IActionResult> GenerateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            var user = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "",
                Email = "admin@admin.com",
                PhoneNumber = "123456789",
                UserName = "admin",
            };
            var res = await _userManager.CreateAsync(user, "Admin@123");
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok("Data generated");
        }

    }
}
