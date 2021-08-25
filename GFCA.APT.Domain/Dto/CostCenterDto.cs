using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class CostCenterDto : Auditable
    {
        [Required]
        public int? CENTER_ID { get; set; }
        [Required]
        public string CENTER_CODE { get; set; }
        public string CENTER_NAME { get; set; }
        public string CENTER_DESC { get; set; }
        public string FLAG_ROW { get; set; }
    }
}
