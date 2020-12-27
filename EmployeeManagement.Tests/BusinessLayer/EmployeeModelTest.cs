using EmployeeManagement.BLL;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;
using EmployeeManagement.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EmployeeManagement.Tests.BusinessLayer
{
    [TestClass]
    public class EmployeeModelTest
    {

        private readonly Employee employee = new Employee { Id = 1, EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000 };
        private readonly EmployeeBOL employeeBOL = new EmployeeBOL { EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000 };

        [TestMethod]

        public void GetMapEmployees_ShouldReturnEmployeeBOL()
        {

            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000}
            };
            IEnumerable<EmployeeBOL> employeesBOL = new List<EmployeeBOL>
            {
                new EmployeeBOL { EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000}
            };

            var model = new EmployeeModel();

            var response = model.GetMapEmployees(employees);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(IEnumerable<EmployeeBOL>));
            
        }

        [TestMethod]

        public void GetMapEmployees_ShouldReturnException()
        {
            var model = new EmployeeModel();
            Assert.ThrowsException<NullReferenceException>(() => model.GetMapEmployees(null));
        }

        [TestMethod]
        public void GetMapEmployee_ShouldReturnEmployeeBOL()
        {
           
            var model = new EmployeeModel();
            var response = model.GetMapEmployee(employee);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(EmployeeBOL));
            Assert.AreEqual(response.EmployeeId, employeeBOL.EmployeeId);
        }

        [TestMethod]

        public void GetMapEmployee_ShouldReturnException()
        {
            var model = new EmployeeModel();
            Assert.ThrowsException<NullReferenceException>(() => model.GetMapEmployee(null));

        }

        [TestMethod]
        public void GetMapEmployeeBOL_ShouldReturnEmployee()
        {
            var model = new EmployeeModel();

            var response = model.GetMapEmployeeBOL(employeeBOL);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(Employee));
            Assert.AreEqual(response.EmployeeId, employee.EmployeeId);
        }

        [TestMethod]
        public void GetMapEmployeeBOL_ShouldReturnException()
        {
            var model = new EmployeeModel();
            Assert.ThrowsException<NullReferenceException>(() => model.GetMapEmployeeBOL(null));
        }
    }
}
