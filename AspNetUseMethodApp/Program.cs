var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string date = "";
string time = "";

app.Use(async (context, next) =>
{
    // before next
    Console.WriteLine("Date component start");
    date = DateTime.Now.ToShortDateString();

    await next.Invoke();

    Console.WriteLine("Date component finish");
    // after next
});

app.Use(async (context, next) =>
{
    // before next
    Console.WriteLine("Time component start");
    time = DateTime.Now.ToShortTimeString();

    await next.Invoke();

    Console.WriteLine("Time component finish");
    // after next
});

app.Run(async context => {
    Console.WriteLine("Terminal component start");
    await context.Response.WriteAsync($"Date: {date}");
    Console.WriteLine("Terminal component finish");
});

app.Run();
