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
	}
}