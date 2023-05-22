using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneDepot.Data.Static;
using PhoneDepot.Data;
using PhoneDepot.Models;
using System.Threading.Tasks;
using PhoneDepot.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace PhoneDepot.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        //public async Task<IActionResult> Users()
        //{
        //    var users = await _context.Users.ToListAsync();
        //    return View(users);
        //}


        public IActionResult SignIn() => View(new SignInVM());

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM signInVM)
        {
            if (!ModelState.IsValid) return View(signInVM);

            var user = await _userManager.FindByEmailAsync(signInVM .EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, signInVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, signInVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Phone");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(signInVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(signInVM);
        }


        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult SignUp() => View(new SignUpVM());

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM signupVM)
        {
            if (!ModelState.IsValid) return View(signupVM);

            var user = await _userManager.FindByEmailAsync(signupVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(signupVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = signupVM.FullName,
                Email = signupVM.EmailAddress,
                UserName = signupVM.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, signupVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost] 
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Phone");
        }

        public IActionResult NoAccess(string ReturnUrl)
        {
            return View();
        }

       

    }
}