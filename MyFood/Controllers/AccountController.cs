using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
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

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
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
            if (user == null) return BadRequest();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (!result.Succeeded)
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid email / password."
                });

            var token = GenerateToken(user);

            return Ok(new
            {
                Data = token,
                Message = "Login Successful"
            });
        }

        private string GenerateToken(ApplicationUser user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role,_userManager.GetRolesAsync(user).Result.First()),
            new Claim("Date", DateTime.Now.ToString()),
            new Claim(ClaimTypes.NameIdentifier,user.Id)
        };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,    //null original value
                expires: DateTime.Now.AddMinutes(120),

                //notBefore:
                signingCredentials: credentials);

            var data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
            return data;
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


        
        [HttpGet("UserProfile")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> GetOne()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(userId);

            var user = await _userManager.FindByIdAsync(userId);
            Console.WriteLine(user.Email);


            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPut("UpdateUser")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> Update(UpdateModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(userId);

            var user = await _userManager.FindByIdAsync(userId);
            Console.WriteLine(user.FirstName);
            
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
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete()

        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(userId);

            var user = await _userManager.FindByIdAsync(userId);
            Console.WriteLine(user.FirstName);
            if (user == null)
            {
                return NotFound();
            }

            _db.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("Getroles")]
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
