using Application.Dtos.User;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(loginDto.UserName);
                if (user == null)
                    return BadRequest();

                var token = JwtSecurityTokenCreator.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var user = _mapper.Map<ApplicationUser>(registerDto);

            var result = await _userManager.CreateAsync(user,registerDto.Password);

            if(result.Succeeded)
            {
                return Ok(user.Id);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
    }
}
