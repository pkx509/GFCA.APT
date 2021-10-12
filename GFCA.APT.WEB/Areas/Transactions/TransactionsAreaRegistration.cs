using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions
{
    public class TransactionsAreaRegistration : AreaRegistration 
    {
        private const string __nameSpace = "GFCA.APT.WEB.Areas.Transactions.Controllers";
        public override string AreaName 
        {
            get 
            {
                return "Transactions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            /*
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
            */
            
            context.MapRoute(
                name      : "Transaction_mutate", 
                url       : "T/Document/{action}/{yyyy}",
                defaults  : new { controller = "Default", action = "Index", yyyy = UrlParameter.Optional }, 
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_FixedContract_Item",
                url: "T/FixedContracts/{DocCode}",
                defaults: new { controller = "FixedContract", action = "FixedContractDetail", DocCode = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name : "Transactions_default",
                url: "T/{controller}/{action}/{DocCode}",
                defaults: new { action = "Index", DocCode = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
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