using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions
{
    public class TransactionsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Transactions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Transactions_FixedContract_Id",
                "T/FixedContract/{id}",
                new { controller = "FixedContract", action = "DocumentItem" }
            );

            context.MapRoute(
                "Transactions_default",
                "T/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Transactions_default1",
                "T/{controller}/{action}/{id}",
                new {controller = "default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}