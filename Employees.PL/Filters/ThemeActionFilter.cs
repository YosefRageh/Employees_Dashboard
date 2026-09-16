using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employees.PL.Filters
{
    public class ThemeActionFilter : IActionFilter
    {
        private const string ThemeCookieName = "EmployeeAppTheme";
        private const string DefaultTheme = "light";

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var theme = context.HttpContext.Request.Cookies[ThemeCookieName] ?? DefaultTheme;

            if (context.Controller is Controller controller)
            {
                controller.ViewBag.CurrentTheme = theme;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // no-op
        }
    }
}