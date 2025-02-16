using AspNetDependencyApp;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddTransient<DateService>();
builder.Services.AddDateService();


builder.Services.AddTransient<ITimeService, TimeLongService>();
builder.Services.AddTransient<ITimeService, TimeShortService>();
//builder.Services.AddTransient<ConsoleLogger>();

var app = builder.Build();

app.UseMiddleware<TimeMessageMiddleware>();


//Message message = new(app.Services.GetService<ConsoleLogger>());

//app.Run(async (context) =>
//{
//    //message.Text = "Hello world";
//    //message.ShowMessage();

//    //var dateService = app.Services.GetService<DateService>();
//    //var dateService = context.RequestServices.GetService<DateService>();

//    //var timeService = app.Services.GetService<ITimeService>();

//    //await context.Response.WriteAsync(dateService.GetDate());
//});

app.Run();

