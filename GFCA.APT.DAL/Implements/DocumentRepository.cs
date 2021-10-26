using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GFCA.APT.DAL.Implements
{
    public class DocumentRepository : RepositoryBase, IDocumentRepository
    {

        public DocumentRepository(IDbTransaction transaction) : base(transaction) { }

        public bool ValidateFixedContract(string docTypeCode, string docYear, string docMonth)
        {
            return true;
        }

        public DocumentStateFlowDto GetDocumentStateFlow(int headerId)
        {
            string sqlQuery = @"SELECT 
  ISNULL(b.UPDATED_BY, b.CREATED_BY) ACTION_BY
, ISNULL(b.UPDATED_DATE, b.CREATED_DATE) ACTION_DATETIME
, b.COMMENT
, a.*
FROM TB_T_FIXED_CONTRACT_D a
LEFT JOIN TB_T_FIXED_CONTRACT_H b on b.DOC_FCH_ID = a.DOC_FCH_ID
WHERE a.CONDITION_TYPE = 'PLANNING'
and a.FLAG_ROW = 'S'
-- and DOC_FCH_ID = 7
ORDER BY b.DOC_FCH_ID, b.DOC_VER, b.DOC_REV";

            var parms = new
            {
                DOC_FCH_ID = headerId
            };

            var query = Connection.Query<DocumentStateFlowDto>(
                sql: sqlQuery,
                param: parms,
                transaction: Transaction
                ).FirstOrDefault()
                ;

            return query;

        }

        public IEnumerable<DocumentHistoryDto> GetDocumentHistories(int headerId)
        {
            string sqlQuery = @"SELECT 
  ISNULL(b.UPDATED_BY, b.CREATED_BY) ACTION_BY
, ISNULL(b.UPDATED_DATE, b.CREATED_DATE) ACTION_DATETIME
, b.COMMENT
FROM TB_T_FIXED_CONTRACT_D a
LEFT JOIN TB_T_FIXED_CONTRACT_H b on b.DOC_FCH_ID = a.DOC_FCH_ID
WHERE a.CONDITION_TYPE = 'PLANNING'
and a.FLAG_ROW = 'S'
and b.DOC_FCH_ID = @DOC_FCH_ID
ORDER BY b.DOC_FCH_ID, b.DOC_VER, b.DOC_REV";

            var parms = new
            {
                DOC_FCH_ID = headerId
            };

            var query = Connection.Query<DocumentHistoryDto>(
                sql: sqlQuery,
                param: parms,
                transaction: Transaction
                ).ToList()
                ;

            return query;

        }

        public DocumentDto GenerateDocNo(string docTypeCode, string docCode)
        {
            throw new NotImplementedException();
        }

        public DocumentDto GenerateDocNo(string docTypeCode, int docYear, int docMonth, string clientCode, string channelCode, string custCode)
        {
            string sqlQuery = @"EXECUTE SP_GENERATE_DOC_CODE @DOC_TYPE_CODE, @DOC_YEAR, @DOC_MONTH, @CLIENT_CODE, @CHANNEL_CODE, @CUST_CODE";
            //string sqlQuery = @"SP_GENERATE_DOC_CODE";
            var query = Connection.Query<DocumentDto>(
                sql: sqlQuery,
                param: new
                {
                    DOC_TYPE_CODE = docTypeCode,
                    DOC_YEAR = docYear,
                    DOC_MONTH = docMonth,
                    CLIENT_CODE = clientCode,
                    CHANNEL_CODE = channelCode,
                    CUST_CODE = custCode
                },
                commandType: CommandType.Text,
                transaction: Transaction
                ).FirstOrDefault()
                ;

            return query;
        }

        public IEnumerable<DocumentDto> All()
        {
            string sqlQuery = @"SELECT a.DOC_TYPE_CODE
	        ,(SELECT TOP 1 DOC_TYPE_NAME  from TB_M_DOCUMENT_TYPE b where b.DOC_TYPE_CODE = a.DOC_TYPE_CODE) as DOC_TYPE_NAME
            ,a.DOC_CODE
            ,a.DOC_VER
            ,a.DOC_REV
            ,a.DOC_MONTH
            ,a.DOC_YEAR
            ,a.CUST_CODE
            ,a.DOC_STATUS
            ,a.FLOW_CURRENT
            ,a.FLOW_NEXT
            ,a.REQUESTER
            ,a.ORG_CODE
            ,a.COMP_CODE
FROM TB_T_DOCUMENT a;";
            var query = Connection.Query<DocumentDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public DocumentDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT a.DOC_TYPE_CODE
	        ,(SELECT TOP 1 DOC_TYPE_NAME  from TB_M_DOCUMENT_TYPE b where b.DOC_TYPE_CODE = a.DOC_TYPE_CODE) as DOC_TYPE_NAME
            ,a.DOC_CODE
            ,a.DOC_VER
            ,a.DOC_REV
            ,a.DOC_MONTH
            ,a.DOC_YEAR
            ,a.CUST_CODE
            ,a.DOC_STATUS
            ,a.FLOW_CURRENT
            ,a.FLOW_NEXT
            ,a.REQUESTER
            ,a.ORG_CODE
            ,a.COMP_CODE
            FROM TB_T_DOCUMENT a
            WHERE a.DOC_CODE = @DOC_CODE;";
            var query = Connection.Query<DocumentDto>(
                sql: sqlQuery
                , param: new { DOC_CODE = code }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }

        public void Insert(DocumentDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_T_DOCUMENT 
(
  DOC_TYPE_CODE
, DOC_CODE
, DOC_VER
, DOC_REV
, DOC_MONTH
, DOC_YEAR
, CUST_CODE
, DOC_STATUS
, FLOW_CURRENT
, FLOW_NEXT
, REQUESTER
) VALUES (
  @DOC_TYPE_CODE
, @DOC_CODE
, @DOC_VER
, @DOC_REV
, @DOC_MONTH
, @DOC_YEAR
, @CUST_CODE
, @DOC_STATUS
, @FLOW_CURRENT
, @FLOW_NEXT
, @REQUESTER
, @ORG_CODE
, @COMP_CODE
);";

            var parms = new
            {
                DOC_TYPE_CODE = entity.DOC_TYPE_CODE,
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                DOC_MONTH = entity.DOC_MONTH,
                DOC_YEAR = entity.DOC_YEAR,
                CUST_CODE = entity.CUST_CODE,
                DOC_STATUS = entity.DOC_STATUS,
                FLOW_CURRENT = entity.FLOW_CURRENT,
                FLOW_NEXT = entity.FLOW_NEXT,
                REQUESTER = entity.REQUESTER,
                ORG_CODE = entity.ORG_CODE,
                COMP_CODE = entity.COMP_CODE
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Update(DocumentDto entity)
        {
            string sqlExecute = @"UPDATE TB_T_DOCUMENT 
SET 
  DOC_TYPE_CODE = @DOC_TYPE_CODE
, DOC_CODE      = @DOC_CODE
, DOC_VER       = @DOC_VER
, DOC_REV       = @DOC_REV
, DOC_MONTH     = @DOC_MONTH
, DOC_YEAR      = @DOC_YEAR
, CUST_CODE     = @CUST_CODE
, DOC_STATUS    = @DOC_STATUS
, FLOW_CURRENT  = @FLOW_CURRENT
, FLOW_NEXT     = @FLOW_NEXT
, REQUESTER     = @REQUESTER
, ORG_CODE     = @ORG_CODE
, COMP_CODE     = @COMP_CODE
WHERE DOC_CODE = @DOC_CODE
and DOC_VER    = @DOC_VER
and DOC_REV    = @DOC_REV
;";

            var parms = new
            {
                DOC_TYPE_CODE = entity.DOC_TYPE_CODE,
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                DOC_MONTH = entity.DOC_MONTH,
                DOC_YEAR = entity.DOC_YEAR,
                CUST_CODE = entity.CUST_CODE,
                DOC_STATUS = entity.DOC_STATUS,
                FLOW_CURRENT = entity.FLOW_CURRENT,
                FLOW_NEXT = entity.FLOW_NEXT,
                REQUESTER = entity.REQUESTER,
                ORG_CODE = entity.ORG_CODE,
                COMP_CODE = entity.COMP_CODE
            };

            Connection.ExecuteScalar<string>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }

        public void Delete(string code)
        {
            string sqlExecute = @"DELETE TB_T_DOCUMENT
                                WHERE
                                DOC_CODE = @DOC_CODE;
                                ";
            var parms = new
            {
                DOC_CODE = code,
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }

    }
}
