using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters
{
    public class MastersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Masters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute("Masters_default"
                ,"M/{controller}/{action}/{id}"
                , new { action = "Index", id = UrlParameter.Optional }
                , new[] { "GFCA.APT.WEB.Areas.Masters.Controllers" }
            );

        }
    }
}