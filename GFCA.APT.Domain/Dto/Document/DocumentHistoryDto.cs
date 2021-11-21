using GFCA.APT.Domain.Enums;
using System;

namespace GFCA.APT.Domain.Dto
{
    public class DocumentHistoryDto : DocumentStateDto
    {
        public string ACTOR_NAME { get; set; }
        public string ACTOR_POSITION { get; set; }
        public DateTime ACTION_DATETIME { get; set; }
        public COMMAND_TYPE DOC_ACTION { get; set; }
        public string COMMENT { get; set; }
    }
}
