using WebApplication1.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Dto;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IConfiguration config, IJwtGenerator jwtGenerator)
        {
            _roleManager=roleManager;
            _userManager=userManager;
            _config=config;
            _jwtGenerator=jwtGenerator;
        }

        

        [HttpGet("roles")]
        public async Task<IActionResult> Role()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name="Admin" });
                result =await _roleManager.CreateAsync(new IdentityRole { Name="Member" });
                result =await _roleManager.CreateAsync(new IdentityRole { Name="SuperAdmin" });

            return StatusCode(201);

        }

        [HttpPost("register")]

        public async Task<IActionResult>Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user!=null)
            {
                return BadRequest("already exist");
            }

            user=new AppUser
            {
                FullName=registerDto.FullName,
                UserName=registerDto.UserName,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201, user.FullName);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user==null)
            {
                return NotFound();
            };

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                token = _jwtGenerator.GenerateJwt(user, roles)
            }) ;

        }
    }
}
