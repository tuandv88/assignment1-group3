using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Dto;

namespace WebAPI.Authentication {
    public class JWTService {
        public static string SECRET_KEY { get; set; } = "aa5d0a646ced270e4c625fc3f5362a577242402ceb238ef3a5025ac22cb27aa1eb170ac4f2c2dd3b804c5155b8b575506efc51621924f89b27d32f595e907925";

        public string GenerateToken(StaffDto staff) {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRET_KEY);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = GenerateClaims(staff),
                //Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(StaffDto staff) {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, staff.Name));
            claims.AddClaim(new Claim(ClaimTypes.Role, staff.Role));
            return claims;
        }
    }
}
