using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.Models;
using SmartAdmin.ViewModels;

namespace SmartAdmin.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> signInManager;
        
        public AuthController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");    
                    }

                    return Redirect(returnUrl);
                }
                
                ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            }
            
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}