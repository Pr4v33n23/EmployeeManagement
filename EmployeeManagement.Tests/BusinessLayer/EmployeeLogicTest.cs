using EmployeeManagement.BLL;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.BusinessLayer
{
    [TestClass]
    public class EmployeeLogicTest
    {
        private readonly Employee employee = new Employee { Id = 1, EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000 };
        private readonly EmployeeBOL employeeBOL = new EmployeeBOL { EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000 };

        [TestMethod]
        public void Setup()
        {
            var employeeLogic = new EmployeeLogic();

            Assert.IsNotNull(employeeLogic);
        }

        [TestMethod]
        public void Setup_DIConstructor()
        {
            var mockEmployeeRepo = new Mock<IEmployeeRepo>();
            var mockEmployeeModel = new Mock<IEmployeeModel>();

            var employeeLogic = new EmployeeLogic(mockEmployeeRepo.Object, mockEmployeeModel.Object);
            Assert.IsNotNull(employeeLogic);
        }

        [TestMethod]
        public async Task GetEmployeesFromDB_ShouldReturnEmployees()
        {

            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000}
            };

            IEnumerable<EmployeeBOL> employeesBOL = new List<EmployeeBOL>
            {
                new EmployeeBOL { EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000}
            };

            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.ReadAllAsync())
                .Returns(Task.FromResult(employees));

            employeeModel.Setup(x => x.GetMapEmployees(It.IsAny<IEnumerable<Employee>>()))
                .Returns(employeesBOL);

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.GetEmployeesAsync();

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(IEnumerable<EmployeeBOL>));

        }

        [TestMethod]
        public async Task GetEmployeesFromDB_ShouldReturnException()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.ReadAllAsync())
                .ThrowsAsync(new Exception());
            employeeModel.Setup(x => x.GetMapEmployees(It.IsAny<IEnumerable<Employee>>()))
                .Throws(new Exception());

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            await Assert.ThrowsExceptionAsync<Exception>(() => employeeLogic.GetEmployeesAsync(), "Exception was not thrown");

        }

        [TestMethod]
        public async Task GetEmployeeByIdFromDB_ShouldReturnEmployee()
        {

            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(employee));
            employeeModel.Setup(x => x.GetMapEmployee(It.IsAny<Employee>()))
                .Returns(employeeBOL);

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.GetEmployeeByIdAsync("21-12344");

            Assert.IsNotNull(response);
            Assert.AreEqual("21-12344", response.EmployeeId);
            Assert.IsInstanceOfType(response, typeof(EmployeeBOL));

        }

        [TestMethod]

        public async Task GetEmployeeByIdFromDB_ShouldReturnNull()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<Employee>()));

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.GetEmployeeByIdAsync(It.IsAny<string>());

            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task GetEmployeeByIdFromDB_ShouldReturnException()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            employeeModel.Setup(x => x.GetMapEmployees(It.IsAny<IEnumerable<Employee>>()))
                .Throws(new Exception());

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);
            await Assert.ThrowsExceptionAsync<Exception>(
                () => employeeLogic.GetEmployeeByIdAsync(It.IsAny<string>()), "Exception was not thrown");
        }

        [TestMethod]

        public async Task AddEmployeeToDB_ShouldReturnTrue()
        {
            
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.AddAsync(employee)).Returns(Task.FromResult(true));

            employeeModel.Setup(x => x.GetMapEmployeeBOL(employeeBOL)).Returns(employee);

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.AddEmployeeAsync(employeeBOL);

            Assert.IsTrue(response);

        }

        [TestMethod]
        public async Task AddEmployeesToDB_ShouldReturnException()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.AddAsync(It.IsAny<Employee>()))
                .ThrowsAsync(new Exception());
            employeeModel.Setup(x => x.GetMapEmployeeBOL(It.IsAny<EmployeeBOL>()))
                .Throws(new Exception());

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            await Assert.ThrowsExceptionAsync<Exception>(
                () => employeeLogic.AddEmployeeAsync(It.IsAny<EmployeeBOL>()), "Exception was not thrown");
        }

        [TestMethod]
        public async Task UpdateEmployeeToDB_ShouldReturnEmployee()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeModel.Setup(x => x.GetMapEmployeeBOL(employeeBOL))
                .Returns(employee);
            employeeModel.Setup(x => x.GetMapEmployee(employee))
                .Returns(employeeBOL);

            employeeRepo.Setup(x => x.UpdateAsync(employee.EmployeeId, employee))
                .Returns(Task.FromResult(employee));
       
            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.UpdateEmployeeAsync(employee.EmployeeId, employeeBOL);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(EmployeeBOL));
            Assert.AreEqual(response.EmployeeId, employeeBOL.EmployeeId);

        }

        [TestMethod]
        public async Task UpdateEmployeeToDB_ShouldReturnNull()
        {

            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Employee>()))
                .Returns(Task.FromResult(It.IsAny<Employee>()));

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.UpdateEmployeeAsync(It.IsAny<string>(), It.IsAny<EmployeeBOL>());

            Assert.IsNull(response);
        }

        [TestMethod]
        public async Task UpdateEmployeeToDB_ShouldReturnException()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<Employee>()))
                .ThrowsAsync(new Exception());
            employeeModel.Setup(x => x.GetMapEmployeeBOL(It.IsAny<EmployeeBOL>()))
                .Throws(new Exception());

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            await Assert.ThrowsExceptionAsync<Exception>(
                () => employeeLogic.UpdateEmployeeAsync(It.IsAny<string>(), It.IsAny<EmployeeBOL>()), "Exception was not thrown");
        }

        [TestMethod]

        public async Task DeleteEmployeeFromDB_ShouldReturnEmployee()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.DeleteAsync(employeeBOL.EmployeeId))
                .Returns(Task.FromResult(employee));
            employeeModel.Setup(x => x.GetMapEmployee(employee))
                .Returns(employeeBOL);

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.DeleteEmployeeByIdAsync(employeeBOL.EmployeeId);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.EmployeeId, employeeBOL.EmployeeId);
            Assert.IsInstanceOfType(response, typeof(EmployeeBOL));
        }

        [TestMethod]
        public async Task DeleteEmployeeFromDB_ShouldReturnNull()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                    .Returns(Task.FromResult(It.IsAny<Employee>()));

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);

            var response = await employeeLogic.DeleteEmployeeByIdAsync(It.IsAny<string>());

            Assert.IsNull(response);
        }


        [TestMethod]
        public async Task DeleteEmployeeFromDB_ShouldReturnException()
        {
            var employeeRepo = new Mock<IEmployeeRepo>();
            var employeeModel = new Mock<IEmployeeModel>();

            employeeRepo.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            employeeModel.Setup(x => x.GetMapEmployee(It.IsAny<Employee>()))
                .Throws(new Exception());

            var employeeLogic = new EmployeeLogic(employeeRepo.Object, employeeModel.Object);
            await Assert.ThrowsExceptionAsync<Exception>(
                () => employeeLogic.DeleteEmployeeByIdAsync(It.IsAny<string>()), "Exception was not thrown");
        }
    }
}
