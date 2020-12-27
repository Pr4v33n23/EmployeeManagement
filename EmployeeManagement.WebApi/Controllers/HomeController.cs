using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace EmployeeManagement.WebApi.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}