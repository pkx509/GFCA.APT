using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.Domain.Dto
{
    public class BudgetPlanDto
    {
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PAGE_MODE DataMode { get; set; }
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }

        public PromotionPlanngOverviewDto OverviewData { get; set; }
        public PromotionPlanningSaleDto DetailSaleItem { get; set; }
        public PromotionPlanningInvestmentDto DetailInvesmentItem { get; set; }
        public IEnumerable<PromotionPlanningSaleDto> DetailSaleData { get; set; }
        public IEnumerable<PromotionPlanningInvestmentDto> DetailInvesmentData { get; set; }

        public PromotionPlanningFooterDto FooterData { get; set; }

        public BudgetPlanHeaderDto BudgetPlanHeader { get; set; }

        public BudgetPlanDto(int primaryKey = 0)
        {
            DataMode = primaryKey == 0 ? PAGE_MODE.CREATING : PAGE_MODE.EDITING;
            WorkflowData = new DocumentWorkFlowDto();
            DocumentData = new DocumentStateDto();
            RequesterData = new DocumentRequesterDto();
            HistoryData = new List<DocumentHistoryDto>();

            OverviewData = new PromotionPlanngOverviewDto();
            DetailSaleItem = new PromotionPlanningSaleDto();
            DetailInvesmentItem = new PromotionPlanningInvestmentDto();
            DetailSaleData = new List<PromotionPlanningSaleDto>();
            DetailInvesmentData = new List<PromotionPlanningInvestmentDto>();
            FooterData = new PromotionPlanningFooterDto();
            BudgetPlanHeader = new BudgetPlanHeaderDto();

        }
    }
}
