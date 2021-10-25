using GFCA.APT.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class DocumentDto
    {
        [Required] public string DOC_TYPE_CODE { get; set; }
        [Required] public string DOC_CODE { get; set; }
        public int DOC_VER { get; set; }
        public int DOC_REV { get; set; }
        [Required] public string DOC_MONTH { get; set; }
        [Required] public string DOC_YEAR { get; set; }
        public string CUST_CODE { get; set; }
        public DOCUMENT_STATUS DOC_STATUS { get; set; } = DOCUMENT_STATUS.DRAFT;
        public string FLOW_CURRENT { get; set; }
        public string FLOW_NEXT { get; set; }
        [Required] public string REQUESTER { get; set; }
        public string ORG_CODE { get; set; }
        public string COMP_CODE { get; set; }
    }
}
