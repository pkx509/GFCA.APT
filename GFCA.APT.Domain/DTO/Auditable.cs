using GFCA.APT.Domain.Enums;
using System;

namespace GFCA.APT.Domain.Dto
{
    public abstract class Auditable
    {
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}
