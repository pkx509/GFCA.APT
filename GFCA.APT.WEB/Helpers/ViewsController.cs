using System.Web.Mvc;

namespace GFCA.APT.WEB.Helpers
{
    public class ViewsController : Controller
    {
        public ContentResult Index(string path)
        {
            var localPath = Server.MapPath($"~/{path}");
            var content = System.IO.File.ReadAllText(localPath);
            return Content(content);
        }
    }
}