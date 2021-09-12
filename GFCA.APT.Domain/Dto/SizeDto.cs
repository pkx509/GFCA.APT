using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{

    public class SizeDto : Auditable
    {
        public int SIZE_ID { get; set; }
        public string SIZE_CODE { get; set; }
        public string SIZE_NAME { get; set; }
        public string SIZE_DESC { get; set; }
        public string FLAG_ROW { get; set; }
    }

}


