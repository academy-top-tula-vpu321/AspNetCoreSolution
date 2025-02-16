using AspNetRoutingApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<EmployeeService>();

var app = builder.Build();

app.Map("/", () => "Main page");

app.Map("/about", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h2>О компании</h2>");
});

app.Map("/contact/{phone:regex(^7-\\d{{3}})}", (string phone) => "Contact page");


app.Map("/employee/{id:int?}", 
        async (HttpContext context, int? id, EmployeeService service) =>
{
    if (id is not null)
        await context.Response.WriteAsJsonAsync(service.Employee((int)id));
    else
        await context.Response.WriteAsJsonAsync(service.Employees());
});



app.Map("/product/{brand:alpha:minlength(3)}/{title=Computer}", ProductRequestHandler);

app.Map("/info/{**info}", async (HttpContext context, string info) =>
{
    string[] infos = info.Split('/');
    string responseStr = "";
    foreach(var i in infos) responseStr += i + "\n";
    await context.Response.WriteAsync($"{responseStr}");
});

app.Run();

string ProductRequestHandler(string brand, string? title)
{
    return $"Product: {brand} - {title}";
}
