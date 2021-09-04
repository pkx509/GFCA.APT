using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class InternalOrderDto : Auditable
    {
        [Required]
        public int? IO_ID { get; set; }
        public string IO_CODE { get; set; }
        public string IO_NAME { get; set; }
        public string IO_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
