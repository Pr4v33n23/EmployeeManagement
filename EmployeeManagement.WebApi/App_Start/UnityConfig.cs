using EmployeeManagement.BLL;
using EmployeeManagement.DAL;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace EmployeeManagement.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IEmployeeLogic, EmployeeLogic>();
            container.RegisterType<IEmployeeRepo, EmployeeRepo>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}