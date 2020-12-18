using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL.Interfaces
{
    public interface IEmployeeLogic
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
