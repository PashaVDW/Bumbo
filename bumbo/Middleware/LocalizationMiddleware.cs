using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace bumbo.Middleware
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Equals("/SetLanguage", StringComparison.OrdinalIgnoreCase) &&
                context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(context.Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    var culture = body.Trim('"');

                    if (!string.IsNullOrEmpty(culture))
                    {
                        context.Response.Cookies.Append(
                            CookieRequestCultureProvider.DefaultCookieName,
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                            new CookieOptions
                            {
                                Expires = DateTimeOffset.UtcNow.AddYears(1),
                                IsEssential = true
                            }
                        );

                        context.Response.StatusCode = 200;
                        return;
                    }
                }

                context.Response.StatusCode = 400;
                return;
            }

            await _next(context);
        }
    }
}
