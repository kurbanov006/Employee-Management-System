public interface IEmployeeService
{
    Task<bool> Create(Employee employee);
    Task<bool> Update(Employee employee, Guid id);
    Task<bool> Delete(Guid id);
    Task<EmployeeForGet?> GetById(Guid id);
    Task<IEnumerable<EmployeeForGet?>> GetAll();
    Task<IEnumerable<GettingEmployeesByActivityStatus?>> GettingEmployeesByActivityStatus(bool isActive);
    Task<IEnumerable<ReceivingEmployeesByDepartment?>> GetReceivingEmployeesByDepartment(Guid departmentId);
    Task<ObtainingSalaryStatistics?> ObtainingSalaryStatistics(); 
    Task<IEnumerable<GettingEmployeesBornInACertainPeriod?>> GettingEmployeesBornInACertainPeriod(DateTime startDate, DateTime endDate);
}