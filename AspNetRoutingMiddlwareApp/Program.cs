var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string responseMessage = "";

app.Use(async (context, next) =>
{
    responseMessage = "First middlware start\n";
    Console.WriteLine("First middlware start");

    await next.Invoke();

    responseMessage += "First middlware finish\n";

    await context.Response.WriteAsync(responseMessage);
});

app.Use(async (context, next) =>
{
    responseMessage += "Second middlware start\n";

    await next.Invoke();

    responseMessage += "Second middlware finish\n";
});

app.Map("/", () => { responseMessage += "Main path\n"; });
app.Map("/about", () => { responseMessage += "About path\n"; });


app.Run();
