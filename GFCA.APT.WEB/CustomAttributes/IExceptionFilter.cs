using System.Web.Mvc;

namespace GFCA.APT.WEB.CustomAttributes
{
    public interface IExceptionFilter
    {
        void OnException(ExceptionContext filterContext);
    }
}
