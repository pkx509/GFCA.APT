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
                "Transactions_FixedContract_List",
                "T/FixedContract",
                new { controller = "FixedContract", action = "Index" }
            );

            context.MapRoute(
                "Transactions_FixedContract_Detail",
                "T/FixedContract/{DocCode}",
                new { controller = "FixedContract", DocCode = UrlParameter.Optional, action = "FixedContractDetail" }
            );

            /*
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
            */
        }
    }
}