using AspNetWelcomeApp;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(JsonHandler);

async Task JsonHandler(HttpContext context)
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;

    if (path == "/info")
    {
        string message = "Incorrect data";
        try
        {
            var employee = await request.ReadFromJsonAsync<Employee>();
            if (employee is not null)
                message = $"Name: {employee.Name}, Age: {employee.Age}";
        }
        catch { }

        await response.WriteAsJsonAsync(new { text = message });
    }
    else
    {
        response.Headers.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("index.html");
    }
}


app.Run();


