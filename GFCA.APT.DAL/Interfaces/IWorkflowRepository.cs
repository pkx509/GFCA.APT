using GFCA.APT.Domain.Dto.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IWorkflowRepository
    {
        IEnumerable<CommandDto> GetCommands(string DOC_TYPE_CODE, int DOC_STATUS_ID = 0);
    }
}
