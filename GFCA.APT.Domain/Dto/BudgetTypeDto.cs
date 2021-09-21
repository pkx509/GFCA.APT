using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class BudgetTypeDto : Auditable
    {
        //[Required]
        //public int? BG_TYPE_ID { get; set; }
        [Required]
        public string BG_TYPE_CODE { get; set; }
        public string BG_TYPE_NAME { get; set; }
        public string BG_TYPE_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
