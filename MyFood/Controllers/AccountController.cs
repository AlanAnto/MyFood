﻿namespace MyFood.Controllers
{
    [Route("api/user")]
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

        /// <summary>
        /// Logs in a registered user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Returns a token if login is successful </returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Nullable),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Registers a user and add th euser to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return Ok();
            }
            return BadRequest(new ResponseModel<string>()
            {
                Success = false,
                Message = "Email already exists",
            });
        }

        /// <summary>
        /// Finds a user from the database.
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(UpdateModel),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var logedinuser = await _userManager.FindByNameAsync(userName);
            var user = new UpdateModel()
            {
                FirstName = logedinuser.FirstName,
                LastName = logedinuser.LastName,
                Email = logedinuser.Email,
                PhoneNumber = logedinuser.PhoneNumber
            };
            return Ok(new ResponseModel<UpdateModel>()
            {
                Data = user,
            });
        }

        /// <summary>
        /// Updates the master details of the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateModel model)
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userName);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            _userManager.SetEmailAsync(user, model.Email);
            await _db.SaveChangesAsync();
            return Ok(user);
        }

        /// <summary>
        /// Deletes the user from the database.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete()
        {
            var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userName);
            var orders = await _db.Orders.Where(m => m.UserId == user.Id).ToListAsync();
            var foodOrders = await _db.FoodOrders.Where(m => m.UserId == user.Id).ToListAsync();
            _db.RemoveRange(orders);
            _db.RemoveRange(foodOrders);
            _db.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Generates a token when a user is logging in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<string> GenerateToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role,role),
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

        /// <summary>
        /// Sets up the role and Admin details in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet("generate")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
            return Ok("Roles and Admin Generated");
        }
    }
}
