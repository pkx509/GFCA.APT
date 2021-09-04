using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class DocumentTypeDto : Auditable
    {
        [Required]
        public int? DOC_TYPE_ID { get; set; }
        [Required]
        public string DOC_TYPE_CODE { get; set; }
        public string DOC_TYPE_NAME { get; set; }
        public string DOC_TYPE_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
