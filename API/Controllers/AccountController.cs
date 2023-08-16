﻿using API.Dtos;
using API.Errors;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.IsCompletedSuccessfully)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {

                Email = user.Email,
                Token = "this is token",
                DisplayName = user.DisplayName
            };
        }
    }
}
