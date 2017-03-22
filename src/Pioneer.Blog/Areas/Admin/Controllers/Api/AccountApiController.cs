#if (DEBUG)
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Areas.Admin.Controllers.Api
{
    [Route("api/accounts")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public AccountApiController(UserManager<UserEntity> userManager, 
            SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRegisterLogin model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList());
            }

            var user = new UserEntity { UserName = model.Email, Email = model.Email };
            var result =  await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description).ToList());
            }

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountRegisterLogin model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
#endif