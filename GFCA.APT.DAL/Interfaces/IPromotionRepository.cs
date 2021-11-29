using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IPromotionRepository
    {
        IEnumerable<PromotionPlanngOverviewDto> GetPromotionPlanAll();
        PromotionPlanngOverviewDto GetPromotionPlanByItemID(int DOC_PROM_PH_ID);
        void InsertOverview(PromotionPlanngOverviewDto entity);
        void UpdateOverview(PromotionPlanngOverviewDto entity);
        void DeleteOverview(int DOC_PROM_PH_ID);

        IEnumerable<PromotionPlanningSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanningSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID);
        void InsertPlanngSale(PromotionPlanningSaleDto entity);
        void UpdatePlanngSale(PromotionPlanningSaleDto entity);
        void DeletePlanngSale(int DOC_PROM_PS_ID);

        IEnumerable<PromotionPlanningInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanningInvestmentDto GetInvestmentByItemID(int DOC_PROM_PI_ID);
        void InsertInvestment(PromotionPlanningInvestmentDto entity);
        void UpdateInvestment(PromotionPlanningInvestmentDto entity);
        void DeleteInvestment(int DOC_PROM_PI_ID);


    }
}
