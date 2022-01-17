using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Dto.Workflow;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IWorkflowRepository
    {
        IEnumerable<CommandDto> GetCommands(string DOC_TYPE_CODE, int DOC_STATUS_ID = 0);
        WorkflowStateDto GetStateInitial(string DOC_TYPE_CODE);
        IEnumerable<WorkflowStateDto> GetStateCurrents(string DOC_TYPE_CODE, int FLOW_CURRENT);
        IEnumerable<WorkflowStateDto> GetStateNexts(string DOC_TYPE_CODE, int FLOW_CURRENT);
        IEnumerable<WorkflowStateDto> GetStatePrevious(string DOC_TYPE_CODE, int FLOW_CURRENT);
        IEnumerable<WorkflowStateDto> GetStateRoutes(string DOC_TYPE_CODE, int FLOW_CURRENT);

        void PostDocument(string DOC_TYPE_CODE, COMMAND_TYPE command, DocumentStateDto documentState);
    }

    

}
