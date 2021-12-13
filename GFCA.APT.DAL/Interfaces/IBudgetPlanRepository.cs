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
         

    }
}
