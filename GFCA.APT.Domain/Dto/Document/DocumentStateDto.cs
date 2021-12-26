using GFCA.APT.Domain.Enums;

namespace GFCA.APT.Domain.Dto
{
    public class DocumentStateDto
    {
        public virtual string DOC_TYPE_CODE { get; set; }
        public virtual string DOC_CODE { get; set; }
        public virtual int DOC_VER { get; set; }
        public virtual int DOC_REV { get; set; }

        public virtual string DOC_MONTH { get; set; }
        public virtual string DOC_YEAR { get; set; }

        public int? DOC_STATUS_ID { get; set; } = 0;
        public DOCUMENT_STATUS DOC_STATUS { get; set; } = DOCUMENT_STATUS.DRAFT;

    }
}
