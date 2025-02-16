//var builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//    WebRootPath = "htmls"
//});
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


//app.MapGet("/", async(context) =>
//{
//    context.Response.ContentType = "text/html";
//    await context.Response.SendFileAsync("wwwroot/index.html");
//});

app.Run();
