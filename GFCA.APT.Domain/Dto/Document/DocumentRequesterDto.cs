namespace GFCA.APT.Domain.Dto
{
    public class DocumentRequesterDto : DocumentStateDto
    {

        public string EMP_CODE { get; set; }
        public string EMP_NAME { get; set; }

        //Position
        public string POSITION_CODE { get; set; }
        public string POSITION_NAME { get; set; }
        public string APPLICATION_ROLE { get; set; }

        //Level
        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
        public string ORG_CODE { get; set; }
        public string ORG_NAME { get; set; }
    }
}
