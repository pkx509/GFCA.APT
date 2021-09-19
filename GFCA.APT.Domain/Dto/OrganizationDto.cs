using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class OrganizationDto : Auditable
    {
        [Required]
        public int? ORG_ID { get; set; }
        public int? COMP_ID { get; set; }
        public string ORG_CODE { get; set; }
        public string ORG_TYPE { get; set; }
        public string ORG_DEPARTMENT_NAME { get; set; }
        public string ORG_POSITION_NAME { get; set; }
        public string ORG_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
