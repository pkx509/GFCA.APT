namespace GFCA.APT.Domain.Dto
{
    public class DocumentWorkFlowDto : DocumentStateDto
    {
        public string FLOW_CURRENT { get; set; }
        public string FLOW_NEXT { get; set; }
    }
}
