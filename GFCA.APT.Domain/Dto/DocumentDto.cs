using GFCA.APT.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class DocumentDto
    {
        [Required] public virtual string DOC_TYPE_CODE { get; set; }
        [Required] public virtual string DOC_CODE { get; set; }
        public virtual int DOC_VER { get; set; }
        public virtual int DOC_REV { get; set; }
        [Required] public virtual string DOC_MONTH { get; set; }
        [Required] public virtual string DOC_YEAR { get; set; }
        public virtual string CUST_CODE { get; set; }
        public virtual string CUST_NAME { get; set; }
        public virtual DOCUMENT_STATUS DOC_STATUS { get; set; } = DOCUMENT_STATUS.DRAFT;
        public virtual string FLOW_CURRENT { get; set; }
        public virtual string FLOW_NEXT { get; set; }
        [Required] public virtual string REQUESTER { get; set; }
        public virtual string ORG_CODE { get; set; }
        public virtual string ORG_NAME { get; set; }
        public virtual string COMP_CODE { get; set; }
        public virtual string COMP_NAME { get; set; }
    }

}
