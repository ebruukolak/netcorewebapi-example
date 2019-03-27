using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Commons.Helper;
using CoreWebApi.Helpers;
using Entity;
using Manager.Abstract;
using Manager.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreWebApi.Controllers
{
    //[Authorize]
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        IUserManager usermanager;
        private IMapper mapper;
        private readonly AppSettings appSettings;
        public UsersController(IUserManager _usermanager,IMapper _mapper,IOptions<AppSettings> _appSettings)
        {
            usermanager=_usermanager;
            mapper=_mapper;
            appSettings=_appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromForm] UserDTO userDTO)
        {
            var user =usermanager.Authenticate(userDTO.UserName,userDTO.Password);
             if(user==null)
                return BadRequest(new{ message = "Username or password is incorrect" });
             var tokenHandler=new JwtSecurityTokenHandler();
             var key =Encoding.ASCII.GetBytes(appSettings.Secret);
             var tokenDescriptor=new SecurityTokenDescriptor
             {
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.ID.ToString())
                }),
                Expires=DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
             };
             var token=tokenHandler.CreateToken(tokenDescriptor);
             var tokenString=tokenHandler.WriteToken(token);

             return Ok(new{
                    ID=user.ID,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenString
             }) ;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromForm] UserDTO userDTO)
        {
            var user=mapper.Map<Users>(userDTO);
            try
            {
                usermanager.Add(user,userDTO.Password);
                return Ok();
            }
            catch (CustomException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }

    }
  
}