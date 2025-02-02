using AspNetRestApiApp;

List<Employee> employees = new()
{
    new(){ Id = Guid.NewGuid(), Name = "Tommy", Age = 26 },
    new(){ Id = Guid.NewGuid(), Name = "Jimmy", Age = 32 },
    new(){ Id = Guid.NewGuid(), Name = "Poppy", Age = 19 },
};


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var request = context.Request;
    var response = context.Response;
    var path = request.Path;


});

app.Run();
