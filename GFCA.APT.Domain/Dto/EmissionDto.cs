using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class EmissionDto : Auditable
    {
        [Required]
        public int? EMIS_ID { get; set; }
        [Required]
        public string EMIS_CODE { get; set; }
        public string EMIS_NAME { get; set; }
        public string EMIS_DESC { get; set; }
        public bool IS_ACTIVED { get; set; } = true;
        public bool IS_DELETE_PERMANANT { get; set; } = false;
        public string FLAG_ROW { get; set; }
    }
}
