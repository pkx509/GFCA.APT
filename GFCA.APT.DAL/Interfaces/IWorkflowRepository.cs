using GFCA.APT.Domain.Dto.Workflow;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IWorkflowRepository
    {
        IEnumerable<CommandDto> GetCommands(string DOC_TYPE_CODE, int DOC_STATUS_ID = 0);
        void PostDocument(string DOC_TYPE_CODE, int DOC_STATUS_ID = 0);
    }
}
