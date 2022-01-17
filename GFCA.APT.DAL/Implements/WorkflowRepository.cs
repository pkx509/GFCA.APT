using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Dto.Workflow;
using GFCA.APT.Domain;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GFCA.APT.DAL.Implements
{
    public class WorkflowRepository : RepositoryBase, IWorkflowRepository
    {
        public WorkflowRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<CommandDto> GetCommands(string DOC_TYPE_CODE, int DOC_STATUS_ID = 0)
        {
            string sqlQuery =
@"IF @IN_DOC_STATUS_ID = 0
	BEGIN
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
		, a.SORT
		from TB_WP_STATE_DIRECTION a
		left join TB_WM_FLOW_ITEM b on b.FLOW_ITEM_ID = a.FLOW_ITEM_ID
		left join TB_WM_WORKFLOW_STATE c on c.WF_STATE_ID = a.WF_STATE_ID
		where 
		c.WF_CODE in (select top 1 WF_CODE from TB_M_DOCUMENT_TYPE where DOC_TYPE_CODE = @IN_DOC_TYPE_CODE)
		and c.WF_STATE_PARENT_ID is null
		and getdate() between c.EFF_DATE and c.END_DATE
		order by a.SORT asc
	END
ELSE
	BEGIN
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
		, a.SORT
		from TB_WP_STATE_DIRECTION a
		left join TB_WM_FLOW_ITEM b on b.FLOW_ITEM_ID = a.FLOW_ITEM_ID
		left join TB_WM_WORKFLOW_STATE c on c.WF_STATE_ID = a.WF_STATE_ID
		where
		c.WF_CODE in (select top 1 WF_CODE from TB_M_DOCUMENT_TYPE where DOC_TYPE_CODE = @IN_DOC_TYPE_CODE)
		and c.WF_STATE_ID = @IN_DOC_STATUS_ID
		and getdate() between c.EFF_DATE and c.END_DATE
		order by a.SORT asc
	END";

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

        public WorkflowStateDto GetStateInitial(string DOC_TYPE_CODE)
        {
			string sqlCommand =
@"SELECT 
  A.WF_STATE_ID
, A.WF_STATE_PARENT_ID
, A.STATE_CODE
, A.STATE_NAME
, A.STATE_DESC
, A.NOTI_SUBJECT
, A.NOTI_MESSAGE
, A.NOTI_FOOTER
, A.EFF_DATE
, A.END_DATE
, B.DOC_TYPE_CODE
, B.DOC_TYPE_NAME
--, C.WF_STATE_ID
, C.STATE_CODE
, C.FLOW_ITEM_ID
, C.FLOW_ITEM_CODE
--, C.DIRECTION_CODE
--, C.DIRECTION_NAME
, C.SORT
FROM
(
	SELECT 
	  WF_STATE_ID
	, WF_STATE_PARENT_ID
	, WF_ID
	, STATE_CODE
	, STATE_NAME
	, STATE_DESC
	, NOTI_SUBJECT
	, NOTI_MESSAGE
	, NOTI_FOOTER
	, EFF_DATE
	, END_DATE
	FROM TB_WM_WORKFLOW_STATE
	WHERE GETDATE() BETWEEN EFF_DATE AND END_DATE
) A
INNER JOIN
(
	SELECT 
	  A.WF_ID
	, A.WF_CODE
	, A.EFF_DATE
	, A.END_DATE
	, B.DOC_TYPE_CODE
	, B.DOC_TYPE_NAME
	FROM TB_WM_WORKFLOW A
	INNER JOIN TB_M_DOCUMENT_TYPE B ON B.WF_CODE = A.WF_CODE
	WHERE getdate() BETWEEN A.EFF_DATE AND A.END_DATE
	AND B.FLAG_ROW = 'S'
	AND B.DOC_TYPE_CODE = @IN_DOC_TYPE_CODE
) B ON B.WF_ID = A.WF_ID
LEFT JOIN TB_WP_STATE_DIRECTION C ON C.WF_STATE_ID = A.WF_STATE_ID
WHERE C.FLOW_ITEM_CODE = 'DRAFT'
AND A.WF_STATE_PARENT_ID IS NULL
ORDER BY A.WF_STATE_ID, C.Sort ASC";

			return new WorkflowStateDto();

        }
        public IEnumerable<WorkflowStateDto> GetStateCurrents(string DOC_TYPE_CODE, int FLOW_CURRENT)
        {

			string sqlCommand =
@"SELECT 
  A.WF_STATE_ID
, A.WF_STATE_PARENT_ID
, A.STATE_CODE
, A.STATE_NAME
, A.STATE_DESC
, A.NOTI_SUBJECT
, A.NOTI_MESSAGE
, A.NOTI_FOOTER
, A.EFF_DATE
, A.END_DATE
, B.DOC_TYPE_CODE
, B.DOC_TYPE_NAME
--, C.WF_STATE_ID
, C.STATE_CODE
, C.FLOW_ITEM_ID
, C.FLOW_ITEM_CODE
--, C.DIRECTION_CODE
--, C.DIRECTION_NAME
, C.SORT
FROM
(
	SELECT 
	  WF_STATE_ID
	, WF_STATE_PARENT_ID
	, WF_ID
	, STATE_CODE
	, STATE_NAME
	, STATE_DESC
	, NOTI_SUBJECT
	, NOTI_MESSAGE
	, NOTI_FOOTER
	, EFF_DATE
	, END_DATE
	FROM TB_WM_WORKFLOW_STATE
	WHERE GETDATE() BETWEEN EFF_DATE AND END_DATE
) A
INNER JOIN
(
	SELECT 
	  A.WF_ID
	, A.WF_CODE
	, A.EFF_DATE
	, A.END_DATE
	, B.DOC_TYPE_CODE
	, B.DOC_TYPE_NAME
	FROM TB_WM_WORKFLOW A
	INNER JOIN TB_M_DOCUMENT_TYPE B ON B.WF_CODE = A.WF_CODE
	WHERE getdate() BETWEEN A.EFF_DATE AND A.END_DATE
	AND B.FLAG_ROW = 'S'
	AND B.DOC_TYPE_CODE = @IN_DOC_TYPE_CODE
) B ON B.WF_ID = A.WF_ID
LEFT JOIN TB_WP_STATE_DIRECTION C ON C.WF_STATE_ID = A.WF_STATE_ID
WHERE C.FLOW_ITEM_CODE = 'DRAFT'
AND A.WF_STATE_PARENT_ID IS NULL
ORDER BY A.WF_STATE_ID, C.Sort ASC";

			throw new System.NotImplementedException();
        }
        public IEnumerable<WorkflowStateDto> GetStateNexts(string DOC_TYPE_CODE, int FLOW_CURRENT)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<WorkflowStateDto> GetStatePrevious(string DOC_TYPE_CODE, int FLOW_CURRENT)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<WorkflowStateDto> GetStateRoutes(string DOC_TYPE_CODE, int FLOW_CURRENT)
        {
            throw new System.NotImplementedException();
        }

		private async Task<bool> SubmittedAsync(DocumentDto document)
		{
			return false;
		}
		private async Task<bool> RejectedAsync(DocumentDto document)
		{
			return false;
		}
		private async Task<bool> ApprovedAsync(DocumentDto document)
		{
			return false;
		}
		private async Task<bool> CancelledAsync(DocumentDto document)
		{

			//duplicate header row1 and detail 
			//update row1 flag_row = disable
			return false;
		}

        public void PostDocument(string DOC_TYPE_CODE, COMMAND_TYPE command, DocumentStateDto documentState)
        {
			bool isSuccess = false;
			if (command == COMMAND_TYPE.NONE)
			{
			}
			

        }

    }

}
