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
using API.Extensions;
using API.Token;
using Azure.Core;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public AccountController(StoreContext context, IConfiguration configuration, IRoleService roleService)
        {
            _context = context;
            _configuration = configuration;
            _roleService = roleService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDTO)
        {
            if (!string.IsNullOrEmpty(loginDTO.Email) && !string.IsNullOrEmpty(loginDTO.Password))
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDTO.Email && u.Password == loginDTO.Password);

                if (user != null)
                {
                    var roles = await _roleService.GetRolesOfUser(user.UserId);

                    var userClaims = new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.StreetAddress, user.Address)
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken
                    (
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        claims: userClaims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: creds
                    );

                    var refreshToken = new JwtSecurityToken
                    (
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        expires: DateTime.Now.AddDays(7), // Set a longer expiry time for the refresh token
                        signingCredentials: creds
                    );

                    var accessTokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                    var response = new AuthResponseDTO
                    {
                        token = accessTokenString,
                        RefreshToken = refreshTokenString
                    };

                    return Ok(response);
                }
            }

            return BadRequest();
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthResponseDTO>> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            try
            {
                var principal = tokenHandler.ValidateToken(refreshTokenDTO.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Issuer"],
                    ValidateLifetime = false 
                }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Unauthorized("Invalid token");
                }

                // Extract user claims from the refresh token
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRoles = principal.FindFirst(ClaimTypes.Role)?.Value;

                // Retrieve user information from the database or any other source
                var user = await _context.Users.FindAsync(int.Parse(userId));
                if (user == null)
                {
                    return Unauthorized("Invalid token");
                }

                // Generate a new access token with the user claims
                var userClaims = new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, userRoles),
            new Claim(ClaimTypes.StreetAddress, user.Address)
        };

                var accessToken = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: userClaims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                var response = new AuthResponseDTO
                {
                    token = accessTokenString,
                    RefreshToken = refreshTokenDTO.RefreshToken
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return Unauthorized("Invalid token");
            }
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
                    IsActive = true,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userDTO = toUserDTO(user);
                return Ok(userDTO);
            }

            return BadRequest();
        }

        [HttpGet("resetPassword")]
        public async Task<ActionResult<UserDTO>> GetForgetPasswordUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return Ok();
            }
            return NotFound();
        }

        public static UserDTO toUserDTO(User? user)
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
