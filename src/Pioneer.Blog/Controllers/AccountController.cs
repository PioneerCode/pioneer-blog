//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Pioneer.Blog.Entity;

//namespace Pioneer.Blog.Controllers
//{
//    [Route("[controller]/[action]")]
//    public class AccountController : Controller
//    {
//        private readonly SignInManager<UserEntity> _signInManager;
//        private readonly ILogger _logger;

//        public AccountController(SignInManager<UserEntity> signInManager, ILogger<AccountController> logger)
//        {
//            _signInManager = signInManager;
//            _logger = logger;
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            _logger.LogInformation("User logged out.");
//            return RedirectToPage("/Index");
//        }
//    }
//}
