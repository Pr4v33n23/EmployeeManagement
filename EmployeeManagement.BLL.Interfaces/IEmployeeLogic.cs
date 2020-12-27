using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.BOL;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IEmployeeLogic
    {
        Task<IEnumerable<EmployeeBOL>> GetEmployeesAsync();
        Task<EmployeeBOL> GetEmployeeByIdAsync(string employeeId);
        Task<EmployeeBOL> DeleteEmployeeByIdAsync(string employeeId);
        Task<bool> AddEmployeeAsync(EmployeeBOL employeeBOL);
        Task<EmployeeBOL> UpdateEmployeeAsync(string employeeId, EmployeeBOL employeeBOL);
    }
}