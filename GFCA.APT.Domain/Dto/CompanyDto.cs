using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class CompanyDto : Auditable
    {
        [Required]
        public int? COMP_ID { get; set; }
        [Required]
        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string FLAG_ROW { get; set; }
    }
}
