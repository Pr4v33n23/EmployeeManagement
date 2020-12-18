using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;
using EmployeeManagement.DAL;
using EmployeeManagement.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BLL
{
    public class EmployeeModel : IEmployeeModel
    {
        private readonly IEmployeeLogic _employeeLogic;
        private readonly EmployeeBOL _employee;
        private IEnumerable<Employee> employees;
        private readonly List<EmployeeBOL> employeesBOL = new List<EmployeeBOL>();

        public EmployeeModel() { }

        public EmployeeModel (IEmployeeLogic employeeLogic, EmployeeBOL employee)
        {
            _employeeLogic = employeeLogic;
            _employee = employee;
        }

        public async Task<IEnumerable<EmployeeBOL>> GetEmployeesAysnc()
        {
            employees = await _employeeLogic.GetEmployeesAsync();

            foreach(var employee in employees)
            {
                _employee.EmployeeId = employee.EmployeeId;
                _employee.FirstName = employee.FirstName;
                _employee.LastName = employee.LastName;
                _employee.Salary = employee.Salary;
                _employee.Department = employee.Department;

                employeesBOL.Add(_employee);
            }

            return employeesBOL;
        }


    }
}
