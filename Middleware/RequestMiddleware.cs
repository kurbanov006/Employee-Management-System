public class RequestMiddleware
{
    private readonly RequestDelegate _next;

    public RequestMiddleware(RequestDelegate next)
    {
        _next = next; 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // просто выводит инфу про то что произошло на сайте какой метод использован и в каком месте
        System.Console.WriteLine($"Метод {context.Request.Method}; Маршрут {context.Request.Path}");

        await _next(context);
    }
}