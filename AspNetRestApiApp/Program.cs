using AspNetRestApiApp;
using System.Text.RegularExpressions;

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

    string guidPath = @"^/api/employees/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";

    if (path == "/api/employees" && request.Method == "GET")
    {
        await GetAllEmployeesAsync(response);
    }
    else if(Regex.IsMatch(path, guidPath) && request.Method == "GET")
    {
        string? id = path.Value?.Split("/")[3];
        await GetEmployee(id, response);
    }
    else if(path == "/api/employees" && request.Method == "POST")
    {
        await AddEmployee(request, response);
    }
    else if (path == "/api/employees" && request.Method == "PUT")
    {
        await EditEmployee(request, response);
    }
    else if(Regex.IsMatch(path, guidPath) && request.Method == "DELETE")
    {
        string? id = path.Value?.Split("/")[3];
        await DeleteEmployee(id, response);
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("wwwroot/index.html");
    }
});

app.Run();





async Task GetAllEmployeesAsync(HttpResponse response)
{
    await response.WriteAsJsonAsync(employees);
}



async Task GetEmployee(string? id, HttpResponse response)
{
    Employee? employee = employees.FirstOrDefault(e => e.Id.ToString() == id);
    if(employee is not null)
        await response.WriteAsJsonAsync(employee);
    else
    {
        response.StatusCode = StatusCodes.Status404NotFound;
        await response.WriteAsJsonAsync(new { message = "Employee not found" });
    }
}



async Task AddEmployee(HttpRequest request, HttpResponse response)
{
    try
    {
        Employee? employee = await request.ReadFromJsonAsync<Employee>();
        if(employee is not null)
        {
            employee.Id = Guid.NewGuid();
            employees.Add(employee);
            await response.WriteAsJsonAsync(employee);
        }
        else
        {
            throw new Exception("Incorrect data");
        }
    }
    catch (Exception ex)
    {
        response.StatusCode = StatusCodes.Status400BadRequest;
        await response.WriteAsJsonAsync(new { message = ex.Message });
    }
}


async Task EditEmployee(HttpRequest request, HttpResponse response)
{
    try
    {
        Employee? employeeData = await request.ReadFromJsonAsync<Employee>();

        if (employeeData is not null)
        {
            Employee? employee = employees.FirstOrDefault(e => e.Id == employeeData.Id);
            if (employee is not null)
            {
                employee.Name = employeeData.Name;
                employee.Age = employeeData.Age;

                await response.WriteAsJsonAsync(employee);
            }
            else
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                await response.WriteAsJsonAsync(new { message = "Employee not found" });
            }
        }
        else
            throw new Exception("Incorrect data");
    }
    catch(Exception ex)
    {
        response.StatusCode = StatusCodes.Status400BadRequest;
        await response.WriteAsJsonAsync(new { message = ex.Message });
    }
    
}

async Task DeleteEmployee(string id, HttpResponse response)
{
    Employee? employee = employees.FirstOrDefault(e => e.Id.ToString() == id);
    if(employee is not null)
    {
        employees.Remove(employee);
        await response.WriteAsJsonAsync(employee);
    }
    else
    {
        response.StatusCode = StatusCodes.Status404NotFound;
        await response.WriteAsJsonAsync(new { message = "Employee not found" });
    }
}
