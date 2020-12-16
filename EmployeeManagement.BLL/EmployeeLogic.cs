using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL;
using EmployeeManagement.Entities;

namespace EmployeeManagement.BLL
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeLogic() { }
        public EmployeeLogic(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _employeeRepo.GetAllEmployeesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
