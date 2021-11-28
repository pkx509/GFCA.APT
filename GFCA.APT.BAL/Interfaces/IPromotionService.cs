using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IPromotionService : IDocumentService
    {

        IEnumerable<PromotionPlanngOverviewDto> GetPromotionPlanAll();
        PromotionPlanngOverviewDto GetPromotionPlanByItemID(int DOC_PROM_PH_ID);
        BusinessResponse CreateOverview(PromotionPlanngOverviewDto entity);
        BusinessResponse EditOverview(PromotionPlanngOverviewDto entity);
        BusinessResponse RemoveOverview(int DOC_PROM_PH_ID);
        PromotionPlanningFooterDto GetPromotionFooterByItemID(int DOC_PROM_PH_ID);

        IEnumerable<PromotionPlanngSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanngSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID);
        BusinessResponse CreatePlanngSale(PromotionPlanngSaleDto entity);
        BusinessResponse EditPlanngSale(PromotionPlanngSaleDto entity);
        BusinessResponse RemovePlanngSale(int DOC_PROM_PS_ID);

        IEnumerable<PromotionPlanngInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanngInvestmentDto GetInvestmentByItemID(int DOC_PROM_PI_ID);
        BusinessResponse CreateInvestment(PromotionPlanngInvestmentDto entity);
        BusinessResponse EditInvestment(PromotionPlanngInvestmentDto entity);
        BusinessResponse RemoveInvestment(int DOC_PROM_PI_ID);

    }
}
