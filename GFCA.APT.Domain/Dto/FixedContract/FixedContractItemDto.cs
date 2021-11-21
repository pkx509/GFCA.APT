using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class FixedContractItemDto
    {
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }

        public FixedContractHeaderDto HeaderData { get; set; }
        public IEnumerable<FixedContractDetailDto> DetailData { get; set; }
        public FixedContractFooterDto FooterData { get; set; }
    }
}
