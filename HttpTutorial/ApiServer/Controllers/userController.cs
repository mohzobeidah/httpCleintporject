using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class userController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public userController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {


            if (model.password == "123")
            {
                /*
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.username),
                    new Claim(ClaimTypes.Email, model.email),
                };

                ClaimsIdentity identity = new ClaimsIdentity();
                identity.AddClaims(claims);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                */

                /*await HttpContext.SignInAsync("JwtBearer", claimsPrincipal,
                    new AuthenticationProperties {IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddHours(3)});
                    */

                return await BuildToken(model);
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }


        private async Task<UserToken> BuildToken(UserInfo  model)
        {


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.username),
                new Claim(ClaimTypes.Email, model.email),
            };


            /*var identityUser = await _userManager.FindByEmailAsync(userInfo.EmailAddress);
            var claimsDB = await _userManager.GetClaimsAsync(identityUser);*/

            //claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTkey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }
    }
}