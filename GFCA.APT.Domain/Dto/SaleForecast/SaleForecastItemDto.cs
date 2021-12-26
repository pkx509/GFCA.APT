using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class SaleForecastItemDto
    {
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }

        public SaleForecastHeaderDto HeaderData { get; set; }
        public IEnumerable<SaleForecastDetailDto> DetailData { get; set; }
        public SaleForecastFooterDto FooterData { get; set; }
    }
}
