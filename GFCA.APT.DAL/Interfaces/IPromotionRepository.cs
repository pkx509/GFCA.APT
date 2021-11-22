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

        IEnumerable<PromotionPlanngSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanngSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID);
        void InsertPlanngSale(PromotionPlanngSaleDto entity);
        void UpdatePlanngSale(PromotionPlanngSaleDto entity);
        void DeletePlanngSale(int DOC_PROM_PS_ID);

        IEnumerable<PromotionPlanngInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID);
        PromotionPlanngInvestmentDto GetInvestmentByItemID(int DOC_PROM_PI_ID);
        void InsertInvestment(PromotionPlanngInvestmentDto entity);
        void UpdateInvestment(PromotionPlanngInvestmentDto entity);
        void DeleteInvestment(int DOC_PROM_PI_ID);


    }
}
