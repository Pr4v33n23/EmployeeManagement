using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BOL;
using EmployeeManagement.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EmployeeManagement.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void Setup()
        {
            var controller = new EmployeesController();

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetAllEmployees_ShouldReturnAllEmployees()
        {
            //Arrange
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.GetEmployeesAsync())
                .Returns(Task.FromResult(It.IsAny<IEnumerable<EmployeeBOL>>()));
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act
            var response = await controller.GetAllEmployeesAsync();

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
        }

        [TestMethod]
        public async Task GetAllEmployees_ShouldReturnException()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.GetEmployeesAsync()).ThrowsAsync(new Exception());
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.GetAllEmployeesAsync();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
        }

        [TestMethod]
        public async Task GetEmployeeById_ShouldReturnEmployee()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.GetEmployeeByIdAsync("21-12344"))
                .Returns(Task.FromResult(new EmployeeBOL
                    {EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000}));
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.GetEmployeeByIdAsync("21-12344");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue(out EmployeeBOL employeeBOL));
            Assert.AreEqual("21-12344", employeeBOL.EmployeeId);
        }

        [TestMethod]
        public async Task GetEmployeeById_ShouldReturnNotFound()
        {
            var employeeBOL = new EmployeeBOL
                {EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000};
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.GetEmployeeByIdAsync("21-12344"))
                .Returns(Task.FromResult(employeeBOL));
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.GetEmployeeByIdAsync(It.IsAny<string>());


            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
            Assert.AreNotEqual(It.IsAny<string>(), employeeBOL.EmployeeId);
        }

        [TestMethod]
        public async Task GetEmployeeById_ShouldReturnException()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.GetEmployeeByIdAsync(It.IsAny<string>());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
        }

        [TestMethod]
        public async Task PostEmployee_ShouldReturnEmployee()
        {
            var employeeBOL = new EmployeeBOL
                {EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000};
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.AddEmployeeAsync(employeeBOL));

            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:44370/api/Employees")
            };

            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});

            controller.RequestContext.RouteData = new HttpRouteData(
                new HttpRoute(),
                new HttpRouteValueDictionary {{"controller", "Employees"}}
            );
            var response = await controller.PostEmployeeAsync(employeeBOL);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Assert.AreEqual("https://localhost:44370/api/Employees/21-12344", response.Headers.Location.AbsoluteUri);
            Assert.IsTrue(response.TryGetContentValue(out employeeBOL));
            Assert.AreEqual("21-12344", employeeBOL.EmployeeId);
        }

        [TestMethod]
        public async Task PostEmployeeShouldReturnException()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.AddEmployeeAsync(It.IsAny<EmployeeBOL>()))
                .ThrowsAsync(new Exception());
            

            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.PostEmployeeAsync(It.IsAny<EmployeeBOL>());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));

        }

        [TestMethod]
        public async Task PutEmployee_ShouldReturnUpdatedEmployee()
        {
            var employeeBOL = new EmployeeBOL
                {EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000};
            var employeeLogic = new Mock<IEmployeeLogic>();
            employeeLogic.Setup(x => x.UpdateEmployeeAsync(It.IsAny<string>(), It.IsAny<EmployeeBOL>()))
                .Returns(Task.FromResult(employeeBOL));
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.PutEmployeeAsync("21-12344",
                new EmployeeBOL {FirstName = "XYZ", LastName = "ABC", Department = "IT", Salary = 10000});

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue(out employeeBOL));
            Assert.AreEqual("21-12344", employeeBOL.EmployeeId);
        }

        [TestMethod]
        public async Task PutEmployee_ShouldReturnNotFound()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();


            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.PutEmployeeAsync("10",
                new EmployeeBOL {FirstName = "XYZ", LastName = "XYZ", Department = "IT", Salary = 10000});
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task PutEmployee_ShouldReturnException()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();

            employeeLogic.Setup(x => x.UpdateEmployeeAsync(It.IsAny<string>(), It.IsAny<EmployeeBOL>()))
                .ThrowsAsync(new Exception());

            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.PutEmployeeAsync(It.IsAny<string>(), It.IsAny<EmployeeBOL>());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
           
        }

        [TestMethod]
        public async Task DeleteEmployee_ShouldDeleteEMployeeById()
        {
            var employeeBOL = new EmployeeBOL
                {EmployeeId = "21-12344", FirstName = "xyz", LastName = "abc", Department = "IT", Salary = 20000};

            var employeeLogic = new Mock<IEmployeeLogic>();

            employeeLogic.Setup(x => x.DeleteEmployeeByIdAsync("21-12344"))
                .Returns(Task.FromResult(employeeBOL));
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.DeleteEmployeeByIdAsync("21-12344");

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.TryGetContentValue(out employeeBOL));
            Assert.AreEqual("21-12344", employeeBOL.EmployeeId);
        }

        [TestMethod]
        public async Task DeleteEmployee_ShouldReturnNotFound()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.DeleteEmployeeByIdAsync(It.IsAny<string>());

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task DeleteEmployee_ShouldReturnException()
        {
            var employeeLogic = new Mock<IEmployeeLogic>();

            employeeLogic.Setup(x => x.DeleteEmployeeByIdAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            var controller = new EmployeesController(employeeLogic.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var response = await controller.DeleteEmployeeByIdAsync(It.IsAny<string>());
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
        }
    }
}