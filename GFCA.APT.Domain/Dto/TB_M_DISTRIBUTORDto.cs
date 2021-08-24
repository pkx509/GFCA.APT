using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_DISTRIBUTORDto
    {
        public int DISTB_ID { get; set; }
        public int? EMIS_ID { get; set; }
        public string DISTB_CODE { get; set; }
        public string DISTB_NAME { get; set; }
        public string DISTB_DESC { get; set; }
        public string FLAG_ROW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
