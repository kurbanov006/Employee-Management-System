


using Dapper;
using Npgsql;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext appDbContext;
    public EmployeeService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<bool> Create(Employee employee)
    {
        try
        {
            if (employee == null)
            {
                System.Console.WriteLine($"Не удалось добавить заполните Employee!");
                return false;
            }
            await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> Update(Employee employee, Guid id)
    {
        try
        {
            if (employee == null)
            {
                System.Console.WriteLine($"Не удалось обновить заполните Employee!");
                return false;
            }
            Employee? employee1 = await appDbContext.Employees.FindAsync(id);
            if (employee1 == null)
            {
                System.Console.WriteLine($"Не удалось найти employee!");
                return false;
            }
            employee1.FirstName = employee.FirstName;
            employee1.LastName = employee.LastName;
            employee1.Email = employee.Email;
            employee1.Phone = employee.Phone;
            employee1.DateOfBirth = employee.DateOfBirth;
            employee1.HireDate = employee.HireDate;
            employee1.Position = employee.Position;
            employee1.Salary = employee.Salary;
            employee1.DepartmentId = employee.DepartmentId;
            employee1.ManagerId = employee.ManagerId;
            employee1.IsActive = employee.IsActive;
            employee1.Address = employee.Address;
            employee1.City = employee.City;
            employee1.Country = employee.Country;
            employee1.CreatedAt = employee.CreatedAt;
            employee1.UpdatedAt = employee.UpdatedAt;

            await appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            Employee? employee = await appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                System.Console.WriteLine($"Не удалось удалить по указанному Id = {id}");
                return false;
            }
            appDbContext.Remove(employee);
            await appDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<EmployeeForGet?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<EmployeeForGet>(SqlCommand.SqlGetAllEmployees);
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<EmployeeForGet?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<EmployeeForGet>(SqlCommand.SqlGetEmployeeById, new { Id = id });
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<GettingEmployeesByActivityStatus?>> GettingEmployeesByActivityStatus(bool isActive)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GettingEmployeesByActivityStatus>
                (SqlCommand.SqlGettingEmployeesByActivityStatus, new { IsActive = isActive });
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<ReceivingEmployeesByDepartment?>> GetReceivingEmployeesByDepartment(Guid departmentId)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<ReceivingEmployeesByDepartment>(SqlCommand.SqlReceivingEmployeesByDepartment, new { DepartmentId = departmentId });
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<ObtainingSalaryStatistics?> ObtainingSalaryStatistics()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<ObtainingSalaryStatistics>(SqlCommand.SqlObtainingSalaryStatistics);
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<GettingEmployeesBornInACertainPeriod?>> GettingEmployeesBornInACertainPeriod(DateTime startDate, DateTime endDate)
    {
        try
        {
            using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.SqlConnnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GettingEmployeesBornInACertainPeriod>(SqlCommand.GettingEmployeesBornInACertainPeriod, new {StartDate = startDate, EndDate = endDate});
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}


file class SqlCommand
{
    public const string SqlConnnectionString = "Server=localhost; Port=5432; Database=employee_db; User Id=postgres; Password=12345";
    public const string SqlGetEmployeeById = @"select * from ""Employees"" where ""Id"" = @id";
    public const string SqlGetAllEmployees = @"select * from ""Employees""";
    // 1
    public const string SqlGettingEmployeesByActivityStatus = @"select ""Id"", ""FirstName"", ""LastName"", ""Email"", ""Phone"",
                                                            ""IsActive""
                                                            from ""Employees""
                                                            where ""IsActive"" = @IsActive";

    // 2
    public const string SqlReceivingEmployeesByDepartment = @"select ""Id"", ""FirstName"", ""LastName"", ""Email"", ""Phone"",
                                                            ""IsActive""
                                                            from ""Employees""
                                                            where ""DepartmentId"" = @departmentId";

    // 3
    public const string SqlObtainingSalaryStatistics = @"
                                                            select avg(""Salary"") as avgsalary
                                                            from ""Employees""";

    // 4
    public const string GettingEmployeesBornInACertainPeriod = @"select ""Id"", ""FirstName"", ""LastName"", ""Email"", ""Phone"", ""IsActive""
    from ""Employees""
    where ""DateOfBirth"" between @StartDate and @EndDate";
}