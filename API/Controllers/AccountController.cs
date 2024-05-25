using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;
        //private readonly UserManager<User> _userManager;

        public AccountController(StoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public class LoginResponse
        {
            public string Token { get; set; }
            public UserDTO User { get; set; }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            if (!string.IsNullOrEmpty(loginDTO.Email) && !string.IsNullOrEmpty(loginDTO.Password))
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

                var listUserDTO = new List<UserDTO>();
                if (user != null) //&& VerifyPassword(loginDTO.Password, user.Password))
                {

                    UserDTO userDTO = toUserDTO(user);
                    var token = GenerateToken(userDTO);

                    var loginResponse = new LoginResponse
                    {
                        Token = token,
                        User = userDTO
                    };
                    return Ok(loginResponse);
                }
            }

            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (!string.IsNullOrEmpty(registerDTO.Email) && !string.IsNullOrEmpty(registerDTO.Password))
            {

                var user = new User
                {
                    RoleId = 1,
                    Email = registerDTO.Email,
                    Password = registerDTO.Password,
                    
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userDTO = toUserDTO(user);
                return Ok(userDTO);
            }

            return BadRequest();
        }

        private string GenerateToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Lỗi Key generate vượt quá Kiểu Bytes
            if (securityKey.KeySize < 384)
            {
                var currentKey = securityKey.Key;
                var requiredSize = 384 / 8; // Convert bits to bytes
                var paddedKey = new byte[requiredSize];
                Array.Copy(currentKey, paddedKey, Math.Min(requiredSize, currentKey.Length));
                securityKey = new SymmetricSecurityKey(paddedKey);
            }

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                new[]
                {
            new Claim("email", user.Email),
            new Claim("roleId", user.RoleId.ToString())//Đặt chủ sở hữu Cookie
                },
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static UserDTO toUserDTO(User? user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserId = user.UserId;
            userDTO.RoleId = user.RoleId;
            user.Name = userDTO.Name;
            userDTO.Email = user.Email;
            userDTO.PhoneNumber = user.PhoneNumber;
            userDTO.Address = user.Address;

            return userDTO;
        }
    }
}
