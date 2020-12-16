using EmployeeManagement.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EmployeeManagement.WebApi.Controllers
{
    public class EmployeesController : ApiController
    {

        private readonly IEmployeeLogic _employeeLogic;

        public EmployeesController () { }

        public EmployeesController (IEmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllEmployeesAsync()
        {
            var employees = await _employeeLogic.GetEmployeesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, employees);
        }
    }
}
