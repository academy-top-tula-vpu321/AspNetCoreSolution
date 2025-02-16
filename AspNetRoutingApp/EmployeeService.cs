namespace AspNetRoutingApp
{
    public class EmployeeService
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee(){ Id = 1, Name = "Sammy", Age = 32  },
            new Employee(){ Id = 2, Name = "Henry", Age = 21  },
            new Employee(){ Id = 3, Name = "Mikky", Age = 36  },
        };

        public Employee? Employee(int id) => employees.FirstOrDefault(e => e.Id == id);
        public Employee[] Employees() => employees.ToArray();
    }
}
