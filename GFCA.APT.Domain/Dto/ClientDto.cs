using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.Domain.Dto
{
    public class ClientDto : Auditable
    {
        [Required]
        public int? CLIENT_ID { get; set; }
        [Required]
        public string CLIENT_CODE { get; set; }
        public string CLIENT_NAME { get; set; }
        public string CLIENT_DESC { get; set; }
        public string FLAG_ROW { get; set; }
    }
}
