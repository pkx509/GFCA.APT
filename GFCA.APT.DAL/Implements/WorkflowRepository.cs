using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto.Workflow;
using System.Collections.Generic;
using System.Data;

namespace GFCA.APT.DAL.Implements
{
    public class WorkflowRepository : RepositoryBase, IWorkflowRepository
    {
        public WorkflowRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<CommandDto> GetCommands(string DOC_TYPE_CODE, int DOC_STATUS_ID = 0)
        {
            string sqlQuery =
@"if @IN_DOC_STATUS_ID = 0
	begin
		select 
		  c.WF_ID
		, c.WF_CODE
		, c.WF_STATE_ID
		, a.WF_STATE_ID
		, b.FLOW_ITEM_ID
		, a.STATE_CODE
		, b.FLOW_ITEM_CODE
		, b.FLOW_ITEM_NAME
		, b.FLOW_ITEM_DESC
		, a.DIRECTION_CODE
		, a.DIRECTION_NAME
		, a.Sort
		from TB_WP_STATE_DIRECTION a
		left join TB_WM_FLOW_ITEM b on b.FLOW_ITEM_ID = a.FLOW_ITEM_ID
		left join TB_WM_WORKFLOW_STATE c on c.WF_STATE_ID = a.WF_STATE_ID
		where 
		c.WF_CODE in (select top 1 WF_CODE from TB_M_DOCUMENT_TYPE where DOC_TYPE_CODE = @IN_DOC_TYPE_CODE)
		and c.WF_STATE_PARENT_ID is null
		and getdate() between c.EFF_DATE and c.END_DATE
		order by a.Sort asc
	end
else
	begin
		select 
		  c.WF_ID
		, c.WF_CODE
		, a.WF_STATE_ID
		, b.FLOW_ITEM_ID
		, a.STATE_CODE
		, b.FLOW_ITEM_CODE
		, b.FLOW_ITEM_NAME
		, b.FLOW_ITEM_DESC
		, a.DIRECTION_CODE
		, a.DIRECTION_NAME
		, a.Sort
		from TB_WP_STATE_DIRECTION a
		left join TB_WM_FLOW_ITEM b on b.FLOW_ITEM_ID = a.FLOW_ITEM_ID
		left join TB_WM_WORKFLOW_STATE c on c.WF_STATE_ID = a.WF_STATE_ID
		where
		c.WF_CODE in (select top 1 WF_CODE from TB_M_DOCUMENT_TYPE where DOC_TYPE_CODE = @IN_DOC_TYPE_CODE)
		and c.WF_STATE_ID = @IN_DOC_STATUS_ID
		and getdate() between c.EFF_DATE and c.END_DATE
		order by a.Sort asc
	end";

            if (DOC_STATUS_ID == 0) 
            {

            }
            var parms = new
            {
                IN_DOC_TYPE_CODE = DOC_TYPE_CODE,
                IN_DOC_STATUS_ID = DOC_STATUS_ID
            };

            var query = Connection.Query<CommandDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
    }
}
