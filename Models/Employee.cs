using System.Text.Json.Serialization;

public class Employee
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    [JsonIgnore] // я сюда потавил потому что когда создаётся маълумот автоматически нанимается на работу и создаётся дата
    public DateTime HireDate { get; set; } = DateTime.UtcNow;
    public string Position { get; set; } = null!;
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid ManagerId { get; set; }
    public bool IsActive { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class EmployeeForGet
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; } = DateTime.Now;
    public string Position { get; set; } = null!;
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid ManagerId { get; set; }
    public bool IsActive { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}