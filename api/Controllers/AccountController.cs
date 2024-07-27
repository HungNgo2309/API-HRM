using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinM;

        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _tokenService =tokenService;
            _userManager=userManager;
            _signinM = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDTO)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser= new AppUser{
                    UserName= registerDTO.UserName,
                    Email=registerDTO.Email
                };
                var createUser=await _userManager.CreateAsync(appUser,registerDTO.Password);
                if(createUser.Succeeded)
                {
                    var roleResult=await _userManager.AddToRoleAsync(appUser,"User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto{
                                UserName=appUser.UserName,
                                Email =appUser.Email,
                                Token=_tokenService.CreateToken(appUser)
                            }
                        );
                    }else{
                        return StatusCode(500,roleResult.Errors);
                    }
                }else{
                    return StatusCode(500,createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login (LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var user= await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.UserName.ToLower());
            if(user==null) return Unauthorized("Invalid UserName!");
            var result= await _signinM.CheckPasswordSignInAsync(user,loginDto.Password,false);
            if(!result.Succeeded)
                return Unauthorized("UserName not found and/ or password");
            return Ok(new NewUserDto{
                UserName=user.UserName,
                Email=user.Email,
                Token=_tokenService.CreateToken(user)
            });
        }
    }
}