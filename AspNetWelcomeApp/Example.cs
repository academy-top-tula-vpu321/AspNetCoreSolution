using Microsoft.Extensions.FileProviders;

namespace AspNetWelcomeApp
{
    public static class Example
    {
        public static void SimpleMapGet(WebApplication app)
        {
            app.UseWelcomePage();
            app.MapGet("/", () => "Hello World!");
            app.MapGet("/about", () => "About Page");
        }

        public static async Task TerminalMiddleware(WebApplication app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Terminal middlware");
            });
            app.Run(HandleRequest);
        }

        public static async Task TerminalMiddlewareLife(WebApplication app)
        {
            int count = 1;

            app.Run(async (context) =>
            {
                count *= 2;
                await context.Response.WriteAsync($"Count: {count}");
            });
        }

        public static async Task ResponseObject(WebApplication app)
        {
            app.Run(async (context) =>
            {
                var response = context.Response;
                response.StatusCode = StatusCodes.Status200OK;

                response.Headers.ContentLanguage = "ru-Ru";
                response.Headers.ContentType = "text/html; charset=utf8";
                response.Headers.Append("myheader", "Max");

                await response.WriteAsync("<h1>Hello world</h1>");
            });
        }

        public static async Task RequestObject(WebApplication app)
        {
            app.Run(async (context) =>
            {
                var request = context.Request;
                var response = context.Response;
                response.Headers.ContentLanguage = "ru-Ru";
                response.Headers.ContentType = "text/plain; charset=utf-8";
                string responseText = "";

                foreach (var item in request.Query)
                {
                    responseText += item.ToString() + '\n';
                }

                responseText += $"<h1>Request path: {request.Path}</h1><br>";

                if (request.Path == "/date")
                {
                    responseText += $"<h1>Date: {DateTime.Now.ToLongDateString()}</h1><br>";
                }
                else if (request.Path == "/time")
                {
                    responseText += $"<h1>Time: {DateTime.Now.ToLongTimeString()}</h1><br>";
                }
                else
                {
                    responseText += "<h1>unknow path</h1>";
                }

                await response.WriteAsync(responseText);
            });
        }

        public static async Task SendFileToClient(WebApplication app)
        {
            app.Run(async (context) =>
            {
                var provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                var file = provider.GetFileInfo("ada.jpg");
                await context.Response.SendFileAsync(file);

                //context.Response.Headers.ContentDisposition = "attachment; filename=photo.jpg";
                //await context.Response.SendFileAsync("ada.jpg");

                //context.Response.Headers.ContentType = "text/html; charset=utf-8";
                //await context.Response.SendFileAsync("index.html");
            });
        }

        public static async Task FormRead(WebApplication app)
        {
            app.Run(async (context) =>
            {
                var response = context.Response;
                response.Headers.ContentType = "text/html; charset=utf-8";

                var request = context.Request;

                if (request.Path == "/info")
                {
                    var form = request.Form;
                    string? name = form["name"];
                    string? age = form["age"];
                    string[]? langs = form["langs"];

                    string langsStr = "<p>Languages: ";
                    foreach (var l in langs)
                        langsStr += l + ", ";
                    langsStr += "</p>";

                    string responseHtml = $"<div><p>Name: {name}</p><p>Age: {age}</p>{langsStr}</div>";

                    await response.WriteAsync(responseHtml);
                }
                else
                {
                    await response.SendFileAsync("index.html");
                }

            });
        }

        public static async Task RedirectExample(WebApplication app)
        {
            app.Run(async (context) =>
            {
                if (context.Request.Path == "/old")
                {
                    //await context.Response.WriteAsync("Old page");
                    context.Response.Redirect("/new");
                }
                else if (context.Request.Path == "/new")
                {
                    await context.Response.WriteAsync("New page");
                }
                else
                {
                    await context.Response.WriteAsync("Main page");
                }
            });
        }

        public static async Task WriteJsonToClient(WebApplication app)
        {
            app.Run(async (context) =>
            {
                Employee bobby = new() { Name = "Bobby", Age = 32 };
                await context.Response.WriteAsJsonAsync(bobby);
            });
        }

        static async Task HandleRequest(HttpContext context)
        {
            await context.Response.WriteAsync("Terminal middlware");
        }
    }
}
