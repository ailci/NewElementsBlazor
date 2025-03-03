﻿using System.Threading.Tasks;
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
public class BrowserAllowedMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext)
    {
        var clientBrowser = IdentitfyBrowser(httpContext);


        await next(httpContext);
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
    public static IApplicationBuilder UseBrowserAllowedMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BrowserAllowedMiddleware>();
    }
}