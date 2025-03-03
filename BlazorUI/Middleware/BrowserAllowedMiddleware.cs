using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BlazorUI.Middleware;

public enum Browser
{
    InternetExplorer,
    Chrome,
    Firefox,
    Edge,
    Opera,
    SomethingElse
}

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class BrowserAllowedMiddleware(RequestDelegate next, IEnumerable<Browser> browserAllowedList)
{
    public async Task Invoke(HttpContext httpContext)
    {
        var clientBrowser = IdentitfyBrowser(httpContext);

        if (browserAllowedList.Any(browser => browser == clientBrowser))
        {
            await next(httpContext); //Positiv-Fall
        }
        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            httpContext.Response.ContentType = "text/html";
            await httpContext.Response.WriteAsync($"Browser {clientBrowser} wird nicht unterstuetzt. " +
                                                  $"<a href=\"https://browsehappy.com\">BrowseHappy</a>", Encoding.UTF8);
        }
    }

    private Browser IdentitfyBrowser(HttpContext httpContext)
    {
        var userAgent = httpContext.Request.Headers["User-Agent"][0]?.ToLower();
        Browser browser;

        if (userAgent.Contains("chrome") &&
            !(userAgent.Contains("edge") || userAgent.Contains("edg") || userAgent.Contains("opr")))
        {
            browser = Browser.Chrome;
        }
        else if (userAgent.Contains("firefox"))
        {
            browser = Browser.Firefox;
        }
        else if (userAgent.Contains("trident"))
        {
            browser = Browser.InternetExplorer;
        }
        else if (userAgent.Contains("edge") || userAgent.Contains("edg"))
        {
            browser = Browser.Edge;
        }
        else if (userAgent.Contains("opr"))
        {
            browser = Browser.Opera;
        }
        else
        {
            browser = Browser.SomethingElse;
        }

        return browser;
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class BrowserAllowedMiddlewareExtensions
{
    public static IApplicationBuilder UseBrowserAllowedMiddleware(this IApplicationBuilder builder, params IEnumerable<Browser> browserAllowedList)
    {
        return builder.UseMiddleware<BrowserAllowedMiddleware>(browserAllowedList);
    }
}