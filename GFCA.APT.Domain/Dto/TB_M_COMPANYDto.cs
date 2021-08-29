using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_COMPANYDto
    {
        public int COMP_ID { get; set; }
        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
