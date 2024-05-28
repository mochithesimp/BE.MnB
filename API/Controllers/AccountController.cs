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
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(StoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            if (!string.IsNullOrEmpty(loginDTO.Email) && !string.IsNullOrEmpty(loginDTO.Password))
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDTO.Email && u.Password == loginDTO.Password);

                if (user != null) // && VerifyPassword(loginDTO.Password, user.Password))
                {
                    UserDTO userDTO = toUserDTO(user);
                    var token = GenerateToken(userDTO);

                    var loginResponse = new LoginResponse
                    {
                        Token = token,
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

        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim != null)
            {
                var userEmail = emailClaim.Value;
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user != null)
                {
                    var userDTO = toUserDTO(user);
                    return Ok(userDTO);
                }
            }

            return Unauthorized();
        }



        private string GenerateToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Ensure the security key size is adequate for the chosen algorithm
            if (securityKey.KeySize < 384)
            {
                var currentKey = securityKey.Key;
                var requiredSize = 384 / 8; // Convert bits to bytes
                var paddedKey = new byte[requiredSize];
                Array.Copy(currentKey, paddedKey, Math.Min(requiredSize, currentKey.Length));
                securityKey = new SymmetricSecurityKey(paddedKey);
            }

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);

            var claims = new[]
            {
            new Claim("userId", user.UserId.ToString()),
            new Claim("email", user.Email),
            new Claim("roleId", user.RoleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
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
