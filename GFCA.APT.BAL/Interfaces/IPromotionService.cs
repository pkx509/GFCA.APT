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

        IEnumerable<PromotionPlanningSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanningSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID);
        BusinessResponse CreatePlanngSale(PromotionPlanningSaleDto entity);
        BusinessResponse EditPlanngSale(PromotionPlanningSaleDto entity);
        BusinessResponse RemovePlanngSale(PromotionPlanningSaleDto entity);

        IEnumerable<PromotionPlanningInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanningInvestmentDto GetInvestmentByItemID(int DOC_PROM_PH_ID, int DOC_PROM_PI_ID);
        BusinessResponse CreateInvestment(PromotionPlanningInvestmentDto entity);
        BusinessResponse EditInvestment(PromotionPlanningInvestmentDto entity);
        BusinessResponse RemoveInvestment(PromotionPlanningInvestmentDto entity);

    }
}
