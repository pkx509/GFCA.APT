using GFCA.APT.BAL.Log;
using GFCA.APT.BAL.Parties;
using GFCA.APT.DAL;
using GFCA.APT.DAL.Implements;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class CustomerController : ControllerWebBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomerService _customerSvc;
        public CustomerController(ILogService log) : base(log)
        {
            _uow = new UnitOfWork();
            _customerSvc = new CustomerService(_uow, log);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}