


using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

[ApiController]
[Route("/api/employee/")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Create([FromBody] Employee employee)
    {
        if (employee == null)
        {
            return Results.BadRequest("Ваш Employee - null");
        }

        bool res = await employeeService.Create(employee);

        if (res == false)
        {
            return Results.BadRequest("Не удалось добавить!");
        }
        return Results.Ok("Успешно добавлено!");
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Update([FromBody] Employee employee, Guid id)
    {
        if (employee == null)
        {
            return Results.BadRequest("Ваш Employee - null");
        }

        bool res = await employeeService.Update(employee, id);

        if (res == false)
        {
            return Results.BadRequest("Не удалось обновить!");
        }
        return Results.Ok("Успешно обновлено!");
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Delete([FromRoute] Guid id)
    {
        bool res = await employeeService.Delete(id);

        if (res == false)
        {
            return Results.BadRequest("Не удалось удалить!");
        }
        return Results.Ok("Успешно удалено");
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetById([FromRoute] Guid id)
    {
        EmployeeForGet? employee = await employeeService.GetById(id);
        if (employee == null)
        {
            return Results.NotFound($"Не удалось найти employee по Id = {id}");
        }

        return Results.Ok(employee);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetAll()
    {
        IEnumerable<EmployeeForGet?> employees = await employeeService.GetAll();
        if (employees == null)
        {
            return Results.NotFound("Не удалось получить!");
        }
        return Results.Ok(employees);
    }


    // Query 1
    [HttpGet("IsActive={isActive}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GettingEmployeesByActivityStatus([FromRoute] bool isActive)
    {
        IEnumerable<GettingEmployeesByActivityStatus?> res = await employeeService.GettingEmployeesByActivityStatus(isActive);
        if (res == null)
        {
            return Results.NotFound("Не удалось получить employee!");
        }
        return Results.Ok(res);
    }

    // Query 2
    [HttpGet("department/{departmentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetReceivingEmployeesByDepartment([FromRoute] Guid departmentId)
    {
        IEnumerable<ReceivingEmployeesByDepartment?> employees = await employeeService.GetReceivingEmployeesByDepartment(departmentId);
        if (employees == null)
        {
            return Results.NotFound("Не удалось получить employees по depatmentId!");
        }
        return Results.Ok(employees);
    }


    // Query 3
    [HttpGet("salary-statistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetObtainingSalaryStatistics()
    {
        ObtainingSalaryStatistics? statistics = await employeeService.ObtainingSalaryStatistics();
        if (statistics == null)
        {
            return Results.NotFound("Не удалось получить employees по Statistike!");
        }
        return Results.Ok(statistics);
    }


    // Query4
    [HttpGet(@"startDate={startDate}&endDate={endDate}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetEmployeesBornInACertainPeriod(DateTime startDate, DateTime endDate)
    {
        IEnumerable<GettingEmployeesBornInACertainPeriod?> gettings = await employeeService.GettingEmployeesBornInACertainPeriod(startDate, endDate);
        if (gettings == null)
        {
            return Results.NotFound("Не удалось получить employees по Дате рождения!");
        }
        return Results.Ok(gettings);
    }
}
