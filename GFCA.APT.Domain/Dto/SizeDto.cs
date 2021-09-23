using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class SizeDto : Auditable
    {
        [Required]
        public int? SIZE_ID { get; set; }
        [Required]
        public string SIZE_CODE { get; set; }
        public string SIZE_NAME { get; set; }
        public string SIZE_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
