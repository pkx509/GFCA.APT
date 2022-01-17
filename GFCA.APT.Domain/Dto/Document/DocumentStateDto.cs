using System;
using GFCA.APT.Domain.Enums;

namespace GFCA.APT.Domain.Dto
{
    public class DocumentStateDto
    {
        public int DOC_HEAD_ID { get; set; }
        public string DOC_TYPE_CODE { get; set; }
        public string DOC_TYPE_NAME { get; set; }
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; }
        public int? DOC_REV { get; set; }

        public string COMP_CODE { get; set; }
        public string CUST_CODE { get; set; }
        public int? DOC_MONTH { get; set; }
        public int? DOC_YEAR { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DOCUMENT_STATUS DOC_STATUS { get; set; } = DOCUMENT_STATUS.DRAFT;
        public int? FLOW_CURRENT { get; set; }
        public int? FLOW_PREVIOUS { get; set; }
        public int? FLOW_NEXT { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ROW_TYPE FLAG_ROW { get; set; }
        
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }

    }
}
