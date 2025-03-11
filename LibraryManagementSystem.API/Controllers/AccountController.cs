using AutoMapper;
using Ecom.API.Extensions;
using LibraryManagementSystem.API.Errors;
using LibraryManagementSystem.Core.Dtos;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Net;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenServices tokenServices;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices tokenServices, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
            this.mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    return Unauthorized("not authorize");
                }

                var res = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (res is null || res.Succeeded == false)
                {
                    return Unauthorized("not authorize");
                }

                return Ok(new UserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = tokenServices.CreateToken(user),
                });

            }
            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!CheckEmailExist(dto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[]
                    {
                        "This Email is Already Token"
                    }
                });
            }
            var user = new AppUser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.Email,
            };
            var res = await userManager.CreateAsync(user, dto.Password);
            if (res.Succeeded is false)
            {
                BadRequest();
            }
            return Ok(new UserDto
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                Token = tokenServices.CreateToken(user),
            });
        }

        [Authorize]
        [HttpGet("hi")]
        public async Task<string> hi()
        {
            return "hi";
        }

        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await userManager.FindEmailByClaimPrincipal(HttpContext.User);
            return Ok(new UserDto
            {
                DisplayName = user?.DisplayName,
                Email = user?.Email,
                Token = tokenServices.CreateToken(user)
            });
        }

        //[Authorize]
        [HttpGet("check-email-exist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            return Ok(await userManager.FindByEmailAsync(email) != null);
        }
    }
}
