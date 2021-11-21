using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.Domain.Dto
{
    public class FixedContractDto : Auditable
    {
        public PAGE_MODE DataMode { get; set; }
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }
        
        public FixedContractHeaderDto HeaderData { get; set; }
        //public IEnumerable<FixedContractDetailDto> DetailData { get; set; }
        public FixedContractFooterDto FooterData { get; set; }

        public FixedContractDto(int primaryKey = 0)
        {
            DataMode = primaryKey == 0 ? PAGE_MODE.CREATING : PAGE_MODE.EDITING;
            WorkflowData = new DocumentWorkFlowDto();
            DocumentData = new DocumentStateDto();
            RequesterData = new DocumentRequesterDto();
            HistoryData = new List<DocumentHistoryDto>();
            //DetailData = new List<FixedContractDetailDto>();
            FooterData = new FixedContractFooterDto();

        }
    }
}
