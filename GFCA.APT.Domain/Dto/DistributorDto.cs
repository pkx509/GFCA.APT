using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class DistributorDto : Auditable
    {
        //[Required]
        //public int? DISTB_ID { get; set; }
        public string EMIS_CODE { get; set; }
        public string DISTB_CODE { get; set; }
        public string DISTB_NAME { get; set; }
        public string DISTB_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
