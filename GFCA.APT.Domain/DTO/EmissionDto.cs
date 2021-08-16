using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class EmissionDto
    {
        [Required]
        public int? EMIS_ID { get; set; }
        [Required]
        public string EMIS_CODE { get; set; }
        public string EMIS_NAME { get; set; }
        public string EMIS_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
