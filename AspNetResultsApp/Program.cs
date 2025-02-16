var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "my_session";
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

string login = "user";
string password = "qwerty";

app.Run(async (context) =>
{
    // COOKIES
    //if(context.Request.Cookies.ContainsKey("login"))
    //{
    //    var loginCookie = context.Request.Cookies["login"];
    //    if (loginCookie == login)
    //        await context.Response.WriteAsync($"User {loginCookie} found!");
    //    else
    //        await context.Response.WriteAsync("User not found");
    //}
    //else
    //{
    //    context.Response.Cookies.Append("login", login);
    //    await context.Response.WriteAsync("Hello!");
    //}

    // SESSIONS
    if (context.Session.Keys.Contains("login"))
        await context.Response.WriteAsync($"{context.Session.GetString("login")}");
    else
    {
        context.Session.SetString("login", login);
        await context.Response.WriteAsync("Hello");
    }
        
        

});





//app.Map("/json", () => Results.Json(new { Name = "Bobby", Age = 29 }));
//app.Map("/old", () => Results.Redirect("/new"));
//app.Map("data", () => Results.NotFound("Data not found"));


app.Run();
