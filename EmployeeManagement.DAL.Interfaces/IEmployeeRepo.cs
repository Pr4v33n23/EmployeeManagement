using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> ReadAllAsync();
    }
}
