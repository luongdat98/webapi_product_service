using APIWeb.Dtos;
using APIWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI2.Dtos;
using WebAPI2.Helpers;
using WebAPI2.Models;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly IUserRepository _userRepository;
        private readonly JWTSettings _jwtSettings;

        // Moi them
        //private readonly TokenValidationParameters _tokenValidationParameters;
        //-------------


        public UserController(EcommerceContext ecommerceDBContext, IUserRepository userRepository,
                              IOptions<JWTSettings> jwtSettings)
        {
            _context = ecommerceDBContext;
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
            //_tokenValidationParameters = tokenValidationParameters;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (!(string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Password)))
            {
                var userWithToken = _userRepository.LoginUser(user);
                if (userWithToken == null)
                    return Unauthorized();
                return new JsonResult(userWithToken);
            }
            return NotFound(@"Login fail");
        }


        [Route("RefreshToken")]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] TokenRequestDto refreshRequest)
        {
            User user = GetUserFromAccessToken(refreshRequest.AccessToken);
            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = GenerateAccessToken(user);

                return new JsonResult(userWithToken.AccessToken);
            }
            return NotFound(@"Refresh token fail");
        }

        private User GetUserFromAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken securityToken;

            var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            //if ((jwtSecurityToken.Header != null) && (jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase)))
            //{
                var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                return _context.Users.Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            //}
            //return null;

        }

        private bool ValidateRefreshToken(User user, string refreshToken)
        {
            RefreshToken refreshTokenUser = _context.RefreshTokens
                .Where(rt => rt.Token == refreshToken)
                .OrderByDescending(rt => rt.ExpiryDate)
                .FirstOrDefault();

            if (refreshTokenUser != null && refreshTokenUser.UserId == user.UserId
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }

        private string GenerateAccessToken(User userLogin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userLogin.UserId)),
                    new Claim(ClaimTypes.Name, userLogin.Email),
                    new Claim(ClaimTypes.Name, userLogin.Fullname),
                    new Claim(ClaimTypes.Name, userLogin.IsActive.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserModel model)
        {
            var token = _userRepository.RegisterUser(model);
            if (token == null)
                return Unauthorized();
            return new JsonResult("Register successful");
        }
    }
}
