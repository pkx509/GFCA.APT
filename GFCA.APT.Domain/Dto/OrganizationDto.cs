using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class OrganizationDto : Auditable
    {
        [Required]
        public string ORG_CODE { get; set; }
        public string REPORT_TO { get; set; }
        public string COMP_CODE { get; set; }
        public string HIERACHY_ID { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_ABBR { get; set; }
        public string ORG_DESC { get; set; }
        public string FLAG_ORG { get; set; }
        
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
