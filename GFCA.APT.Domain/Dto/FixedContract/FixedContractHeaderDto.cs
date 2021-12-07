using GFCA.APT.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class FixedContractHeaderDto : Auditable
    {
        [Required]
        /* Document */
        public string DOC_TYPE_CODE { get; set; }
        //[Required]
        public string DOC_CODE { get; set; }
        public int? DOC_VER { get; set; } = 0;
        public int? DOC_REV { get; set; } = 0;
        [Required]
        public int DOC_MONTH { get; set; }
        [Required]
        public int DOC_YEAR { get; set; }
        public DOCUMENT_STATUS DOC_STATUS { get; set; }
        public string FLOW_CURRENT { get; set; }
        public string FLOW_NEXT { get; set; }
        public string REQUESTER { get; set; }

        [Required]
        /* Fixed contract header */
        public int DOC_FCH_ID { get; set; }
        //public string DOC_CODE { get; set; }
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_NAME { get; set; }
        public string CHANNEL_CODE { get; set; }
        public string CHANNEL_NAME { get; set; }
        public string COMMENT { get; set; }
        public string FLAG_ROW { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public COMMAND_TYPE COMMAND_TYPE { get; set; } = COMMAND_TYPE.NONE; //SUBMIT, CANCEL, APPROVE, REVIEW, CONFIRM, COMMIT
        public string ORG_CODE { get; set; }
        public string ORG_NAME { get; set; }
        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
    }
}
