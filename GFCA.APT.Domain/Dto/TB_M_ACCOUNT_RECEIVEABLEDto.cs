using System;
using System.ComponentModel.DataAnnotations;
namespace GFCA.APT.Domain.Dto
{
    public class TB_M_ACCOUNT_RECEIVEABLEDto
    {
        public int AR_ID { get; set; }
        public int? IO_ID { get; set; }
        public int? CENTER_ID { get; set; }
        public int? FUND_ID { get; set; }
        public int? FUND_CENTER_ID { get; set; }
        public int? MATERIAL { get; set; }
    }
}
