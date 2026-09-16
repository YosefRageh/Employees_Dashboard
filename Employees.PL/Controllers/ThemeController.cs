using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Employees.PL.Controllers
{
    public class ThemeController : Controller
    {
        private const string ThemeCookieName = "EmployeeAppTheme";

        [HttpPost]
        public IActionResult SetTheme(string theme, string? returnUrl)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1), // ~1 day, per assignment
                IsEssential = true
            };

            if (theme != "light" && theme != "dark")
            {
                theme = "light";
            }

            Response.Cookies.Append(
                ThemeCookieName,
                theme,
                options);

            if (!string.IsNullOrEmpty(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}