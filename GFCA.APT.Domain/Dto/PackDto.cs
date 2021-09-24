using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class PackDto : Auditable
    {
        //[Required]
        //public int? PACK_ID { get; set; }
        [Required]
        public string PACK_CODE { get; set; }
        public string PACK_NAME { get; set; }
        public string PACK_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
