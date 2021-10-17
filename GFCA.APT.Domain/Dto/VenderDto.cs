using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class VenderDto : Auditable
    {
        public string FUND_CODE { get; set; }
        public string CENTER_CODE { get; set; }
        [Required]
        public string VENDOR_CODE { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_ADDR { get; set; }
        public string VENDOR_DESC { get; set; }
        public string FLAG_ROW { get; set; }
    }
}
