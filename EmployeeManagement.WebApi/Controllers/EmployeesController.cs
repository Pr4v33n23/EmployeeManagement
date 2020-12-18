using EmployeeManagement.BLL;
using EmployeeManagement.BLL.Interfaces;
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

        private readonly IEmployeeModel _employeeModel;

        public EmployeesController () { }

        public EmployeesController (IEmployeeModel employeeModel)
        {
            _employeeModel = employeeModel;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllEmployeesAsync()
        {
            var employees = await _employeeModel.GetEmployeesAysnc();
            return Request.CreateResponse(HttpStatusCode.OK, employees);
        }
    }
}
