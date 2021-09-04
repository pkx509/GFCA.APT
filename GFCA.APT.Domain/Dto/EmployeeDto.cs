using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class EmployeeDto : Auditable
    {
        [Required]
        public int? EMP_ID { get; set; }
        [Required]
        public string EMP_CODE { get; set; }
        public string PREFIX { get; set; }
        public string NAME_FIRST { get; set; }
        public string NAME_LAST { get; set; }
        public string EMAIL { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
