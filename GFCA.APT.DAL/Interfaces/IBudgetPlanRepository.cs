using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IBudgetPlanRepository
    {

        IEnumerable<FixedContractHeaderDto> GetFixedContractAll();
        IEnumerable<BudgetPlanHeaderDto> GetBudgetPlanAll();
        //
        BudgetPlanHeaderDto GetBudgetPlanID(int DOC_BGH_ID);
        void InsertBudgetPlanHeaderHeader(BudgetPlanHeaderDto entity);
        void UpdateFixedContractHeader(FixedContractHeaderDto entity);
        void DeleteFixedContractHeader(int DOC_FCH_ID);

        FixedContractDetailDto GetDetailItem(int DOC_FCD_ID, CONDITION_TYPE conditionType = CONDITION_TYPE.PLANNING);
        IEnumerable<FixedContractDetailDto> GetDetailItems(int DOC_FCH_ID, CONDITION_TYPE conditionType = CONDITION_TYPE.PLANNING);
        void InsertFixedContractDetail(FixedContractDetailDto entity);
        void UpdateFixedContractDetail(FixedContractDetailDto entity);
        void DeleteFixedContractDetail(int DOC_FCD_ID);




        void InsertBudgetPlanSale(BudgetPlanSaleDto entity);
        void InsertBudgetPlanInvestment(BudgetPlanInvestmentDto entity);
        IEnumerable<BudgetPlanSaleDto> GetDetailSalesItems(int DOC_BGH_ID);
        IEnumerable<BudgetPlanInvestmentDto> GetDetailInvItems(int DOC_BGH_ID);
        BudgetPlanSaleDto GetDetailSalesItem(int DOC_BGH_SALES_ID);
        BudgetPlanInvestmentDto GetDetailInvItem(int DOC_BGH_INV_ID);

        void UpdateBudgetPlanSale(BudgetPlanSaleDto entity);
        void UpdateBudgetInvsSale(BudgetPlanInvestmentDto entity);

        void DeleteBudgetPlanSale(long DOC_BGH_SALES_ID);
        void DeleteBudgetInvsSale(long DOC_BGH_INV_ID);




  



    }
}
