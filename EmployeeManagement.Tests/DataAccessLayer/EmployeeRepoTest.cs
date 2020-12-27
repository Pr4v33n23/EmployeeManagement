using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.DataAccessLayer
{
    [TestClass]
    public class EmployeeRepoTest
    {
        private readonly List<Employee> employee = new List<Employee> { new Employee { Id = 1, EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000 } };

        [TestMethod]
        public async Task ReadAllAsync_ShouldReturnAllEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000 },
                new Employee { Id = 2, EmployeeId = "12-12354", FirstName = "ABC", LastName = "XYZ", Department = "HR", Salary = 15000 }

            };

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employees);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);
            var response = await employeeRepo.ReadAllAsync();

            Assert.AreEqual(2, response.Count());
            Assert.AreEqual("12-12354", response.ElementAt(1).EmployeeId);
            Assert.IsInstanceOfType(response, typeof(List<Employee>));

            
        }

        [TestMethod]
        public async Task ReadAllAsync_ShouldReturnException()
        {
            var mockSet = new Mock<DbSet<Employee>>()
                            .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new Exception());

            var employeeRepo = new EmployeeRepo(mockContext.Object);
            await Assert.ThrowsExceptionAsync<Exception>(() => employeeRepo.ReadAllAsync());
        }

        [TestMethod]
        public async Task ReadOneAsync_ShouldReturnAllEmployees()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            var response = await employeeRepo.ReadOneAsync(employee.ElementAt(0).EmployeeId);

            Assert.AreEqual("21-12344", response.EmployeeId);

        }

        [TestMethod]
        public async Task ReadOneAsync_ShouldReturnException()
        {
            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new Exception());

            var employeeRepo = new EmployeeRepo(mockContext.Object);
            await Assert.ThrowsExceptionAsync<Exception>(() => employeeRepo.ReadOneAsync(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnTrue()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            var response = await employeeRepo.AddAsync(employee.ElementAt(0));

            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnDbEntityValidationException()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new DbEntityValidationException());

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            await Assert.ThrowsExceptionAsync<DbEntityValidationException>(() => employeeRepo.AddAsync(It.IsAny<Employee>()));
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnException()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new Exception());

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            await Assert.ThrowsExceptionAsync<Exception>(() => employeeRepo.AddAsync(It.IsAny<Employee>()));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnEmployee()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            var response = await employeeRepo.UpdateAsync(employee.ElementAt(0).EmployeeId, employee.ElementAt(0));

            Assert.AreEqual(1, employee.Count());
            Assert.AreEqual("21-12344", employee.ElementAt(0).EmployeeId);
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNull()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            var response = await employeeRepo.UpdateAsync(It.IsAny<string>(), It.IsAny<Employee>());

            Assert.IsNull(response);

        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnDbEntityValidationException()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new DbEntityValidationException());

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            await Assert.ThrowsExceptionAsync<DbEntityValidationException>(() => employeeRepo.UpdateAsync(It.IsAny<string>(), It.IsAny<Employee>()));
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnException()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new Exception());

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            await Assert.ThrowsExceptionAsync<Exception>(() => employeeRepo.UpdateAsync(It.IsAny<string>(), It.IsAny<Employee>()));
        }

        [TestMethod]

        public async Task DeleteAsync_ShouldReturnEmployee()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            var response = await employeeRepo.DeleteAsync(employee.ElementAt(0).EmployeeId);

            Assert.IsNotNull(response);
            Assert.AreEqual("21-12344", response.EmployeeId);
            Assert.IsInstanceOfType(response, typeof(Employee));
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnNull()
        {

            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Returns(mockSet.Object);

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            var response = await employeeRepo.DeleteAsync(It.IsAny<string>());

            Assert.IsNull(response);

        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnException()
        {
            var mockSet = new Mock<DbSet<Employee>>()
                .SetupData(employee);

            var mockContext = new Mock<EmployeeContext>();
            mockContext.Setup(x => x.Employees).Throws(new Exception());

            var employeeRepo = new EmployeeRepo(mockContext.Object);

            await Assert.ThrowsExceptionAsync<Exception>(() => employeeRepo.DeleteAsync(It.IsAny<string>()));
        }
    }
}
