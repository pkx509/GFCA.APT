using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.Domain.Dto
{
    public class FixedContractDto
    {
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PAGE_MODE DataMode { get; set; }
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }
        
        public FixedContractHeaderDto HeaderData { get; set; }
        public FixedContractDetailDto DetailItem { get; set; }
        public IEnumerable<FixedContractDetailDto> DetailData { get; set; }
        public FixedContractFooterDto FooterData { get; set; }

        public FixedContractDto(int primaryKey = 0)
        {
            DataMode = primaryKey == 0 ? PAGE_MODE.CREATING : PAGE_MODE.EDITING;
            WorkflowData = new DocumentWorkFlowDto();
            DocumentData = new DocumentStateDto();
            RequesterData = new DocumentRequesterDto();
            HistoryData = new List<DocumentHistoryDto>();

            HeaderData = new FixedContractHeaderDto();
            DetailItem = new FixedContractDetailDto();
            DetailData = new List<FixedContractDetailDto>();
            FooterData = new FixedContractFooterDto();

        }
    }
}
