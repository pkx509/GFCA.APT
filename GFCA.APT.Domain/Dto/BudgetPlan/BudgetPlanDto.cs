using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.Domain.Dto
{
    public class BudgetPlanDto
    {
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PAGE_MODE DataMode { get; set; }

       
        public int? DOC_BGH_ID { get; set; } = 0;
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; } = 0;
        public int? DOC_REV { get; set; } = 0;

        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }

        public PromotionPlanngOverviewDto OverviewData { get; set; }
        public BudgetPlanSaleDto DetailSaleItem { get; set; }
        public BudgetPlanInvestmentDto DetailInvesmentItem { get; set; }
      //  public IEnumerable<PromotionPlanningSaleDto> DetailSaleData { get; set; }
       // public IEnumerable<PromotionPlanningInvestmentDto> DetailInvesmentData { get; set; }

        public PromotionPlanningFooterDto FooterData { get; set; }

        public BudgetPlanHeaderDto BudgetPlanHeader { get; set; }
        public BudgetPlanSaleDto BudgetPlanSaleDto { get; set; }
        public IEnumerable<BudgetPlanSaleDto> DetailSaleData { get; set; }
        public IEnumerable<BudgetPlanInvestmentDto> DetailInvesmentData { get; set; }


        public BudgetPlanDto(int primaryKey = 0)
        {
            DataMode = primaryKey == 0 ? PAGE_MODE.CREATING : PAGE_MODE.EDITING;
            WorkflowData = new DocumentWorkFlowDto();
            DocumentData = new DocumentStateDto();
            RequesterData = new DocumentRequesterDto();
            HistoryData = new List<DocumentHistoryDto>();

            OverviewData = new PromotionPlanngOverviewDto();
            DetailSaleItem = new BudgetPlanSaleDto();
            DetailInvesmentItem = new BudgetPlanInvestmentDto();
            DetailSaleData = new List<BudgetPlanSaleDto>();
            DetailInvesmentData = new List<BudgetPlanInvestmentDto>();
            FooterData = new PromotionPlanningFooterDto();
            BudgetPlanHeader = new BudgetPlanHeaderDto();
            BudgetPlanSaleDto = new BudgetPlanSaleDto();


        }
    }
}
