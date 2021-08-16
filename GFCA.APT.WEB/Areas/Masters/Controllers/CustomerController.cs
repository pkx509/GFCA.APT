using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class CustomerController : ControllerWebBase
    {
        private readonly IUnitOfWork _uow;
       // private readonly ICustomerService _customerSvc;
        public CustomerController(ILogService log)
        {
            _uow = UnitOfWork.CreateInstant();
           // _customerSvc = new CustomerService(_uow, log);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}