using Application.Interface;
using Application.Request;
using Application.Respsone;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.username) ?? throw new Exception("user not found");

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.password)) throw new UnauthorizedAccessException("Invalid username or password.");

            var roles = await _userManager.GetRolesAsync(user);

            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            var credentials = new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),           
            ];

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var result = _mapper.Map<AuthResponse>(user);
            result.AccessToken = tokenString;
            return result;
        }
    }
}
