using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace minha_app_asp_net
{
  public class MeuMiddleware
  {
    private readonly RequestDelegate _next;

    public MeuMiddleware(RequestDelegate next)
    {
      this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      Console.WriteLine("\n ----- Antes ----- \n");

      bool abort = context.Request.Path.Equals("/abort");

      if (abort)
      {
        context.Abort();
      }

      await this._next(context);

      Console.WriteLine("\n ----- Depois ----- \n");
    }
  }

  public static class MeuMiddlewareExtension
  {
    public static IApplicationBuilder UseMeuMiddleware(this IApplicationBuilder builder) 
    {
      return builder.UseMiddleware<MeuMiddleware>();
    }
  }
}