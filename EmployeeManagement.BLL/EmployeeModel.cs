using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;
using EmployeeManagement.Entities;

namespace EmployeeManagement.BLL
{
    public class EmployeeModel : IEmployeeModel
    {
        public IEnumerable<EmployeeBOL> GetMapEmployees(IEnumerable<Employee> employees)
        {
            try
            {
                var employeesBOL = new List<EmployeeBOL>();

                foreach (var employee in employees)
                {
                    var employeeBOL = new EmployeeBOL
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Salary = employee.Salary,
                        Department = employee.Department
                    };

                    employeesBOL.Add(employeeBOL);
                }

                return employeesBOL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeBOL GetMapEmployee(Employee employee)
        {
            try
            {
                var employeeBOL = new EmployeeBOL
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary,
                    Department = employee.Department
                };
                return employeeBOL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetMapEmployeeBOL(EmployeeBOL employeeBOL)
        {
            try
            {
                var employee = new Employee
                {
                    EmployeeId = employeeBOL.EmployeeId,
                    FirstName = employeeBOL.FirstName,
                    LastName = employeeBOL.LastName,
                    Salary = employeeBOL.Salary,
                    Department = employeeBOL.Department
                };

                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}