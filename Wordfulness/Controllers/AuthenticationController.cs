using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wordfulness.Models;

namespace Wordfulness.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Register")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SubmitRegister(CredentialsViewModel credentials)
		{
			User user = new() { UserName = credentials.Username };
			IdentityResult? result = await _userManager.CreateAsync(user, credentials.Password);

			if (!result.Succeeded)
			{
				return View(credentials);
			}

			await _signInManager.SignInAsync(user, false);

			return RedirectToAction("Index", "Courses");
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Login")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SubmitLogin(CredentialsViewModel credentials)
		{
			User? user = await _userManager.FindByNameAsync(credentials.Username);

			if (user == null)
			{
				return NotFound();
			}

			bool valid = await _userManager.CheckPasswordAsync(user, credentials.Password);

			if (!valid)
			{
				return Unauthorized();
			}

			await _signInManager.SignInAsync(user, false);

			return RedirectToAction("Index", "Courses");
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Courses");
		}
	}
}