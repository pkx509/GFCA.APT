using System;
using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.Domain.Dto
{
    public class CostCenterDto
    {
        [Required()]
        public int? CENTER_ID { get; set; }

    }
}
