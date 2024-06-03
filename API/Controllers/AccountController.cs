using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using API.Extensions;
using API.Token;
using System.Security.Cryptography;
using System.Data;

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
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO loginDTO)
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
                new Claim(ClaimTypes.Name, user?.Name),
                new Claim(ClaimTypes.Email, user?.Email),
                new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.StreetAddress, user?.Address)
            };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken
                    (
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        claims: userClaims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: creds
                    );

                    var refreshToken = new JwtSecurityToken
                    (
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        claims: new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        },
                        expires: DateTime.Now.AddDays(7), // Set a longer expiry time for the refresh token
                        signingCredentials: creds
                    );


                    var accessTokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                    TokenDTO tokenDTO = new TokenDTO()
                    {
                        token = accessTokenString,
                        RefreshToken = refreshTokenString
                    };

                    return Ok(tokenDTO);
                }
            }

            return BadRequest();
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<ResponseDTO>> RefreshToken(TokenDTO TokenDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Issuer"],
                ValidateLifetime = false
            };

            try
            {
                //check accessToken valid format and refresh token
                var principal = tokenHandler.ValidateToken(TokenDTO.token, tokenValidateParam, out SecurityToken validatedToken);
                var principalRefreshToken = tokenHandler.ValidateToken(TokenDTO.RefreshToken, tokenValidateParam, out SecurityToken validatedRefreshToken);

                //check accessToken alg
                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Unauthorized(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Invalid Token"
                    });
                }

                //check accessToken expire?
                var utcExpireDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return Unauthorized(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Access token has not yet expired"
                    });
                }

                //check refreshToken expired?
                utcExpireDate = long.Parse(principalRefreshToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate < DateTime.UtcNow)
                {
                    return Unauthorized(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Refresh token has expired"
                    });
                }

                // check accessToken userId == refreshtoken userId
                var accessTokenUserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var refreshTokenUserId = principalRefreshToken.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (accessTokenUserId != refreshTokenUserId)
                {
                    return Unauthorized(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "token doesn't match"
                    });
                }

                // Extract user claims from the refresh token
                var userRoles = principal.FindFirst(ClaimTypes.Role)?.Value;

                // Retrieve user information from the database or any other source
                var user = await _context.Users.FindAsync(int.Parse(accessTokenUserId));
                if (user == null)
                {
                    return Unauthorized("not find user");
                }

                // Generate a new access token with the user claims
                var userClaims = new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user?.Name),
                new Claim(ClaimTypes.Email, user?.Email),
                new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                new Claim(ClaimTypes.Role, userRoles),
                new Claim(ClaimTypes.StreetAddress, user?.Address)
            };

                var accessToken = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: userClaims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: creds
                );

                var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

                var response = new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "refresh token Successfully",
                    data = new TokenDTO
                    {
                        token = accessTokenString,
                        RefreshToken = TokenDTO.RefreshToken
                    }
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return Unauthorized(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "wrong format token"
                });
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            //check filled in
            if (string.IsNullOrEmpty(registerDTO.Email) && string.IsNullOrEmpty(registerDTO.Password) && string.IsNullOrEmpty(registerDTO.Name)
                && string.IsNullOrEmpty(registerDTO.Address) && string.IsNullOrEmpty(registerDTO.PhoneNumber))
            {
                return BadRequest(new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "Some boxes are not completely filled in"
                });
            }
            //check duplicate email
            var userEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(registerDTO.Email));
            if (userEmail != null)
            {
                return BadRequest(new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = "duplicate email"
                });
            }

            var user = new User
            {
                RoleId = 1,
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                Address = registerDTO.Address,
                PhoneNumber = registerDTO.PhoneNumber,
                IsActive = true,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDTO = toUserDTO(user);
            return Ok(new ResponseDTO()
            {
                IsSuccess = true,
                Message = "Create successfully"
            });
        }

        [HttpPost("checkMail")]
        public async Task<ActionResult<UserDTO>> checkMail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return Ok();
            }
            return BadRequest( new ResponseDTO
            {
                IsSuccess = false,
                Message = "this mail exist in data"
            });
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<UserDTO>> GetForgetPasswordUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult> ChangePassword(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                user.Password = password;
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }



        public static UserDTO toUserDTO(User? user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserId = user.UserId;
            userDTO.RoleId = user.RoleId;
            userDTO.Name = user.Name;
            userDTO.Email = user.Email;
            userDTO.PhoneNumber = user.PhoneNumber;
            userDTO.Address = user.Address;

            return userDTO;
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(utcExpireDate);
            return dateTimeOffset.UtcDateTime;
        }
    }
}
