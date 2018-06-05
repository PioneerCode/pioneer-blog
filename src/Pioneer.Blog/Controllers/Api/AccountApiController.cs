using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Controllers.Api
{
    [Route("api/accounts")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public AccountApiController(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IPasswordHasher<UserEntity> passwordHasher,
            IOptions<AppConfiguration> appConfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _appConfiguration = appConfiguration;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRegisterAuthenticate model)
        {
            if (!ModelState.IsValid)
            {
                return
                    BadRequest(
                        ModelState.Values.SelectMany(v => v.Errors)
                            .Select(modelError => modelError.ErrorMessage)
                            .ToList());
            }

            var user = new UserEntity { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description).ToList());
            }

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRegisterAuthenticate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] UserRegisterAuthenticate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Success)
            {
                return BadRequest();
            }

            var token = await GetJwtSecurityToken(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        /// <summary>
        /// Generate JWT Token based on valid User
        /// </summary>
        private async Task<JwtSecurityToken> GetJwtSecurityToken(UserEntity user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            return new JwtSecurityToken(
                issuer: _appConfiguration.Value.SiteUrl,
                audience: _appConfiguration.Value.SiteUrl,
                claims: GetTokenClaims(user).Union(userClaims),
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfiguration.Value.Key)), SecurityAlgorithms.HmacSha256)
            );
        }

        /// <summary>
        /// Generate additional JWT Token Claims.
        /// Use to any additional claims you might need.
        /// https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html#rfc.section.4
        /// </summary>
        private static IEnumerable<Claim> GetTokenClaims(UserEntity user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
            };
        }
    }
}
