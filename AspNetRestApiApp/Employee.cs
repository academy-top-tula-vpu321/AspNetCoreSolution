namespace AspNetRestApiApp
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }

        public override string ToString()
        {
            return $"id: {Id}, name: {Name}, age: {Age}";
        }
    }
}
