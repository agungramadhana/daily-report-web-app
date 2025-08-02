using DailyReport.Application;
using DailyReport.Application.Interfaces;
using DailyReport.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DailyReport.WebApp.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IClaimToken _claimToken;
        public AuthenticationController(ILogger<AuthenticationController> logger, IClaimToken claimToken, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _logger = logger;
            _claimToken = claimToken;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            try
            {
                var result = await Mediator.Send(new LoginQuery
                {
                    Email = email,
                    Password = password
                });

                if (result is not null)
                {
                    var claimIdentity = await _claimToken.SetClaimFromToken(result);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["FailedLogin"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
