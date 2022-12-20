using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyFood.Models.ResponseModels;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _configuration;

        public AccountController(
            ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (!result.Succeeded)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = "Invalid email / password."
                });
            }
            var token = await GenerateToken(user);
            return Ok(new ResponseModel<string>
            {
                Data = token,
                Message = "Login Successful",
            });
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            await _db.SaveChangesAsync();
            return Ok(user);
        }


        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var orders = await _db.Orders.Where(m => m.UserId == user.Id).ToListAsync();
            var foodOrders = await _db.FoodOrders.Where(m => m.UserId == user.Id).ToListAsync();
            _db.RemoveRange(orders);
            _db.RemoveRange(foodOrders);
            _db.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var claims = new Claim[]
            {
                new("Id", user.Id),
                new("Email", user.Email),
                new("Role", role)
            };

            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var sign = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow + TimeSpan.FromDays(7),
                signingCredentials: sign);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
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
