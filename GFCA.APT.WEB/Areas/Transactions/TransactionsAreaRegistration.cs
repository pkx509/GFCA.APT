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
            /* Document List Route */

            context.MapRoute(
                name: "Transaction_mutate",
                url: "T/Document/{action}/{yyyy}",
                defaults: new { controller = "Default", action = "Index", yyyy = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );


            /* Fixed Contract Route */
            context.MapRoute(
                name: "Transaction_FixedContract_List",
                url: "T/FixedContracts",
                defaults: new { controller = "FixedContract", action = "Index" },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_FixedContract_Item",
                url: "T/FixedContracts/{DOC_FCH_ID}",
                defaults: new { controller = "FixedContract", action = "FixedContractItem", DOC_FCH_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_FixedContract_Detail",
                url: "T/FixedContracts/{DOC_FCH_ID}/{DOC_FCD_ID}",
                defaults: new { controller = "FixedContract", action = "FixedContractDetail", DOC_FCH_ID = UrlParameter.Optional, DOC_FCD_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            /* Budget Planing Route */
            context.MapRoute(
                name: "Transaction_BudgetPlan_List",
                url: "T/BudgetPlans",
                defaults: new { controller = "BudgetPlan", action = "Index" },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_BudgetPlans_Item",
                url: "T/BudgetPlans/{DOC_BUDGET_PH_ID}",
                defaults: new { controller = "BudgetPlan", action = "BudgetPlanItem", DOC_BUDGET_PH_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            /* Promotion Planning Route */
            context.MapRoute(
                name: "Transaction_PromotionPlan_List",
                url: "T/Promotions",
                defaults: new { controller = "Promotion", action = "Index" },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_PromotionPlan_Item",
                url: "T/Promotions/{DOC_PROM_PH_ID}",
                defaults: new { controller = "Promotion", action = "PromotionItem", DOC_PROM_PH_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_PromotionPlan_Investment_Detail",
                url: "T/Promotions/{DOC_PROM_PH_ID}/I/{DOC_PROM_PI_ID}",
                defaults: new { controller = "Promotion", action = "PromotionInvestmentDetail", DOC_PROM_PH_ID = UrlParameter.Optional, DOC_PROM_PI_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_PromotionPlan_Sale_Detail",
                url: "T/Promotions/{DOC_PROM_PH_ID}/S/{DOC_PROM_PS_ID}/{PS_MODE}",
                defaults: new { controller = "Promotion", action = "PromotionSaleDetail", DOC_PROM_PH_ID = UrlParameter.Optional, DOC_PROM_PS_ID = UrlParameter.Optional, PS_MODE = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            /* Sale forecaset Route */
            context.MapRoute(
                name: "Transaction_Saleforecast_List",
                url: "T/Saleforecasts",
                defaults: new { controller = "SaleForecast", action = "Index" },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_Saleforecast_Item",
                url: "T/SaleForecasts/{DOC_SFCH_ID}",
                //defaults: new { controller = "SaleForecast", action = "SaleForecastItem", DocCode = UrlParameter.Optional },
                defaults: new { controller = "SaleForecast", action = "SaleForecastItem", DOC_SFCH_ID = UrlParameter.Optional},
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_Saleforecast_Detail",
                url: "T/SaleForecasts/{DOC_SFCH_ID}/{DOC_SFCD_ID}",
                //defaults: new { controller = "SaleForecast", action = "SaleForecastDetail", DocCode = UrlParameter.Optional },
                defaults: new { controller = "SaleForecast", action = "SaleForecastDetail", DOC_SFCH_ID = UrlParameter.Optional, DOC_SFCD_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_Saleforecast_ExportFiles",
                url: "T/SaleForecasts/EX/ExportFiles/{DOC_SFCH_ID}",
                //defaults: new { controller = "SaleForecast", action = "ExportFiles", DocCode = UrlParameter.Optional },
                defaults: new { controller = "SaleForecast", action = "ExportFiles", DOC_SFCH_ID = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
                );

            context.MapRoute(
                name: "Transaction_Saleforecast_ImportFiles",
                url: "T/SaleForecasts/IM/ImportFiles",
                //defaults: new { controller = "SaleForecast", action = "ImportFiles", DocCode = UrlParameter.Optional },
                defaults: new { controller = "SaleForecast", action = "ImportFiles" },
                namespaces: new[] { __nameSpace }
                );
            context.MapRoute(
                name: "Transaction_Saleforecast_ImportFiles2",
                url: "T/{controller}/{action}",
                //defaults: new { controller = "SaleForecast", action = "ImportFiles", DocCode = UrlParameter.Optional },
                defaults: new { controller = "SaleForecast", action = "ImportFiles" },
                namespaces: new[] { __nameSpace }
                );

            /* Default Route */
            context.MapRoute(
                name: "Transactions_default",
                url: "T/{controller}/{action}/{DocCode}",
                defaults: new { action = "Index", DocCode = UrlParameter.Optional },
                namespaces: new[] { __nameSpace }
            );

        }
    }
}