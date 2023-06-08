using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using project_hotel.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;

namespace project_hotel.Api.Core
{
    public class JwtManager
    {
        private readonly HotelContext _context;
        private readonly JwtSettings _settings;
        private readonly ITokenStorage _storage;

        public JwtManager(HotelContext context, JwtSettings settings, ITokenStorage storage)
        {
            _context = context;
            _settings = settings;
            _storage = storage;
        }

        public string MakeToken(string email, string password)
        {
            var user = _context.Users.Include(x => x.UseCases).FirstOrDefault(x => x.Email == email && x.IsActive);

            if(user == null)
            {
                throw new UnauthorizedAccessException();
            }

            //var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!valid)
            {
                throw new UnauthorizedAccessException();
            }

            var actor = new JwtUser
            {
                Id = user.Id,
                UseCaseIds = user.UseCases.Select(x => x.UseCaseId).ToList(),
                Identity = user.Username,
                Email = user.Email,
                Username = user.Username
            };

            var tokenId = Guid.NewGuid().ToString();

            _storage.AddToken(tokenId);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("UseCases", JsonConvert.SerializeObject(actor.UseCaseIds)),
                new Claim("Email", user.Email),
                new Claim("Username", user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
