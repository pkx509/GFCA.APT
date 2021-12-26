using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.Domain.Dto
{
    public class SaleForecastDto
    {
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PAGE_MODE DataMode { get; set; }
        public DocumentWorkFlowDto WorkflowData { get; set; }
        public DocumentStateDto DocumentData { get; set; }
        public DocumentRequesterDto RequesterData { get; set; }
        public IEnumerable<DocumentHistoryDto> HistoryData { get; set; }
        
        public SaleForecastHeaderDto HeaderData { get; set; }
        public SaleForecastDetailDto DetailItem { get; set; }
        public IEnumerable<SaleForecastDetailDto> DetailData { get; set; }
        public SaleForecastFooterDto FooterData { get; set; }

        public SaleForecastDto(int primaryKey = 0)
        {
            DataMode = primaryKey == 0 ? PAGE_MODE.CREATING : PAGE_MODE.EDITING;
            WorkflowData = new DocumentWorkFlowDto();
            DocumentData = new DocumentStateDto();
            RequesterData = new DocumentRequesterDto();
            HistoryData = new List<DocumentHistoryDto>();

            HeaderData = new SaleForecastHeaderDto();
            DetailItem = new SaleForecastDetailDto();
            DetailData = new List<SaleForecastDetailDto>();
            FooterData = new SaleForecastFooterDto();

        }
    }
}
