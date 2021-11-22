using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.Domain.Dto
{
    public class PromotionPlanningDto
    {
        public PAGE_MODE DataMode { get; set; }
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }

        public PromotionPlanngOverviewDto OverviewData { get; set; }
        public IEnumerable<PromotionPlanngSaleDto> DetailSaleData { get; set; }
        public IEnumerable<PromotionPlanngInvestmentDto> DetailInvesmentData { get; set; }

        public PromotionPlanningFooterDto FooterData { get; set; }

        public PromotionPlanningDto(int primaryKey = 0)
        {
            DataMode = primaryKey == 0 ? PAGE_MODE.CREATING : PAGE_MODE.EDITING;
            WorkflowData = new DocumentWorkFlowDto();
            DocumentData = new DocumentStateDto();
            RequesterData = new DocumentRequesterDto();
            HistoryData = new List<DocumentHistoryDto>();

            OverviewData = new PromotionPlanngOverviewDto();
            DetailSaleData = new List<PromotionPlanngSaleDto>();
            DetailInvesmentData = new List<PromotionPlanngInvestmentDto>();
            FooterData = new PromotionPlanningFooterDto();

        }
    }
}
