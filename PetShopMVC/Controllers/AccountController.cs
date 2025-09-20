using Mailing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PetShopMVC.DataContext.Entities;
using PetShopMVC.Models;
using System.Configuration;

namespace PetShopMVC.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mailService = mailService;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);//eger meselen parol ust uste dusmurse onda seni atmir home sehfesine ve gosterirki parol sehvdir
            }
            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result= await _userManager.CreateAsync(user, model.Password);//baza doldurur bu kod

            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            //var adminRoleResult = await _roleManager.CreateAsync(new IdentityRole { Name="Admin"});
            //if (adminRoleResult.Succeeded)
            //{
            //    await _userManager.AddToRoleAsync(user, "Admin");
            //}
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(model);
            }

            var result= await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", $"You'r time is not finished for entering wrong password.{user.LockoutEnd.Value.AddHours(4).ToString()}");
                return View(model);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(model);
            }

            if(!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }


        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var username =User.Identity!.Name?? "";
            var user=await _userManager.FindByNameAsync(username);

            if(user== null)
            {
                return BadRequest();
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);
            }
            return RedirectToAction(nameof(Login));

        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email required");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email not found");
                return View();
            }

            var resetPasswordToken=await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Account", new { email, resetPasswordToken }, Request.Scheme,Request.Host.ToString());
            _mailService.SendMail(new Mail { ToEmail = "aazimli23797@ada.edu.az" ,Subject="ResetPassword",TextBody=resetLink});

            return View("EmailSimulation",resetLink);
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.ResetPasswordToken,
                model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                    return View(model);
                }
            }

            return RedirectToAction(nameof(Login));

        }


    }
}
