using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using News.Core.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using News.Core.Services;

namespace New.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public async Task<string> CreateToken(WebUser user , UserManager<WebUser> userManager)
        {
            // Private Claims
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            // Key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"]));

            // RegisterdClaims
            var token = new JwtSecurityToken(
                issuer : Configuration["JWT:ValidIssuer"],
                audience : Configuration["JWT:ValidAudience"],
                expires : DateTime.Now.AddDays(double.Parse( Configuration["JWT:DurationInDays"])),
                claims : authClaims,
                signingCredentials : new SigningCredentials(authKey , SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
