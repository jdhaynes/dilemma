using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DilemmaApp.IdentitySvc.Infrastructure.Crytography
{
    public class JwtAuthTokenService : IAuthTokenService
    {
        private byte[] _secret;

        public JwtAuthTokenService(string secret)
        {
            _secret = Encoding.UTF8.GetBytes(secret);
        }

        public string GenerateToken(string userId)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ClaimsIdentity claims = new ClaimsIdentity(new[] {new Claim("userId", userId)});
            SigningCredentials signature = new SigningCredentials(new SymmetricSecurityKey(_secret),
                SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signature
            };

            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public string ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            
            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);
            }
            catch (Exception e)
            {
                // Can't validate token, don't authenticate user.
                return null;
            }
            
            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            string userId = jwtToken.Claims.Single(x => x.Type == "userId").Value;

            return userId;
        }
    }
}