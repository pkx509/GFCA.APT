using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class UnitDto : Auditable
    {
        [Required]
        public string UNIT_CODE { get; set; }
        public string UNIT_PARENT_CODE { get; set; }
        public string UNIT_NAME { get; set; }
        public string UNIT_TYPE { get; set; }
        public decimal FACTOR { get; set; }
        public string UNIT_DESC { get; set; }
        
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
