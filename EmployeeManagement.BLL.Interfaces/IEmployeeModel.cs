using System.Collections.Generic;
using EmployeeManagement.BOL;
using EmployeeManagement.Entities;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IEmployeeModel
    {
        IEnumerable<EmployeeBOL> GetMapEmployees(IEnumerable<Employee> employee);

        EmployeeBOL GetMapEmployee(Employee employee);

        Employee GetMapEmployeeBOL(EmployeeBOL employeeBOL);
    }
}