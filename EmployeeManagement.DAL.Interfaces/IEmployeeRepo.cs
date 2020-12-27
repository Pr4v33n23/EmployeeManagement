using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Entities;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> ReadAllAsync();
        Task<Employee> ReadOneAsync(string employeeId);
        Task<Employee> DeleteAsync(string employeeId);
        Task<bool> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(string employeeId, Employee employee);
    }
}