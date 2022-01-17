using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.DAL.Utilities;
using GFCA.APT.Domain;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
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

        public DocumentStateDto GetDocumentStateFlow(string documentType, int headerId)
        {
            string sqlQuery =
@"IF(@IN_DOC_TYPE_CODE = 'FC')
BEGIN
    SELECT TOP 1

      DOC_FCH_ID 'DOC_HEAD_ID'
	, COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE'
    , (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE
	, DOC_VER
	, DOC_REV
	, CUST_CODE
	, DOC_MONTH
	, DOC_YEAR
	, DOC_STATUS
	, FLOW_CURRENT
	, FLOW_PREVIOUS
	, FLOW_NEXT
	, FLAG_ROW
	, CREATED_BY
	, CREATED_DATE
    , UPDATED_BY
	, UPDATED_DATE
    FROM TB_T_FIXED_CONTRACT_H
    WHERE DOC_FCH_ID = @IN_DOC_HEAD_ID
END

IF(@IN_DOC_TYPE_CODE = 'PP')
BEGIN
    SELECT TOP 1

      DOC_PROM_PH_ID 'DOC_HEAD_ID'
	, COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE'
    , (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE
	, DOC_VER
	, DOC_REV
	, CUST_CODE
	, DOC_MONTH
	, DOC_YEAR
	, DOC_STATUS
	, FLOW_CURRENT
	, FLOW_PREVIOUS
	, FLOW_NEXT
	, FLAG_ROW
	, CREATED_BY
	, CREATED_DATE
    , UPDATED_BY
	, UPDATED_DATE
    FROM TB_T_PROMOTION_H
    WHERE DOC_PROM_PH_ID = @IN_DOC_HEAD_ID
END

IF(@IN_DOC_TYPE_CODE = 'SF')
BEGIN
    SELECT TOP 1

      DOC_SFCH_ID 'DOC_HEAD_ID'
	, COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE'
    , (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE
	, DOC_VER
	, DOC_REV
    , CUST_CODE
	, DOC_MONTH
	, DOC_YEAR
	, DOC_STATUS
	, FLOW_CURRENT
	, FLOW_PREVIOUS
	, FLOW_NEXT
	, FLAG_ROW
	, CREATED_BY
	, CREATED_DATE
    , UPDATED_BY
	, UPDATED_DATE
    FROM TB_T_SALES_FORECAST_H
    WHERE DOC_SFCH_ID = @IN_DOC_HEAD_ID
END

IF(@IN_DOC_TYPE_CODE = 'CM')
BEGIN
    SELECT TOP 1

      DOC_CLMH_ID 'DOC_HEAD_ID'
	, COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE'
    , (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE
	, DOC_VER
	, DOC_REV
	, CUST_CODE
	, DOC_MONTH
	, DOC_YEAR
	, DOC_STATUS
	, FLOW_CURRENT
	, FLOW_PREVIOUS
	, FLOW_NEXT
	, FLAG_ROW
	, CREATED_BY
	, CREATED_DATE
    , UPDATED_BY
	, UPDATED_DATE
    FROM TB_T_CLAIM_H
    WHERE DOC_CLMH_ID = @IN_DOC_HEAD_ID
END

IF(@IN_DOC_TYPE_CODE = 'BP')
BEGIN
    SELECT TOP 1

      DOC_BGH_ID 'DOC_HEAD_ID'
	, COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE'
    , (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE
	, DOC_VER
	, DOC_REV
	, CUST_CODE
	, DOC_MONTH
	, DOC_YEAR
	, DOC_STATUS
	, FLOW_CURRENT
	, FLOW_PREVIOUS
	, FLOW_NEXT
	, FLAG_ROW
	, CREATED_BY
	, CREATED_DATE
    , UPDATED_BY
	, UPDATED_DATE
    FROM TB_T_BUDGET_H
    WHERE DOC_BGH_ID = @IN_DOC_HEAD_ID
END";

            var parms = new
            {
                IN_DOC_TYPE_CODE = documentType,
                IN_DOC_HEAD_ID = headerId
            };

            DocumentStateDto result = new DocumentStateDto();
            try
            {
                
                var query = Connection.Query(
                    sql: sqlQuery,
                    param: parms,
                    transaction: Transaction
                );
                
                if (query != null)
                {
                    result = query.Select(x => transformDocumentState(x)).FirstOrDefault();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private DocumentStateDto transformDocumentState(dynamic x)
        {
            DocumentStateDto result = new DocumentStateDto();
            try
            {
                var dto = new DocumentStateDto();

                dto.DOC_HEAD_ID = x.DOC_HEAD_ID;
                dto.COMP_CODE = x.COMP_CODE;
                dto.DOC_TYPE_CODE = x.DOC_TYPE_CODE;
                dto.DOC_TYPE_NAME = x.DOC_TYPE_NAME;
                dto.DOC_CODE = x.DOC_CODE;
                dto.DOC_VER = x.DOC_VER;
                dto.DOC_REV = x.DOC_REV;
                dto.CUST_CODE = x.CUST_CODE;
                dto.DOC_MONTH = x.DOC_MONTH;
                dto.DOC_YEAR = x.DOC_YEAR;

                string docStatus = x.DOC_STATUS as string;
                dto.DOC_STATUS = docStatus.ToEnum<DOCUMENT_STATUS>();
                dto.FLOW_CURRENT = x.FLOW_CURREN;
                dto.FLOW_PREVIOUS = x.FLOW_PREVIOUS;
                dto.FLOW_NEXT = x.FLOW_NEXT;
            
                string flagRow = x.FLAG_ROW as string;
                dto.FLAG_ROW = flagRow.ToEnum<ROW_TYPE>();
                dto.CREATED_BY = x.CREATED_BY;
                dto.CREATED_DATE = x.CREATED_DATE;
                dto.UPDATED_BY = x.UPDATED_BY;
                dto.UPDATED_DATE = x.UPDATED_DATE;

                result = dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        private DocumentHistoryDto transformDocumentHistories(dynamic x)
        {
            DocumentHistoryDto result = new DocumentHistoryDto();
            try
            {
                var dto = new DocumentHistoryDto();

                dto.DOC_HEAD_ID = x.DOC_HEAD_ID;
                dto.COMP_CODE = x.COMP_CODE;
                dto.DOC_TYPE_CODE = x.DOC_TYPE_CODE;
                dto.DOC_TYPE_NAME = x.DOC_TYPE_NAME;
                dto.DOC_CODE = x.DOC_CODE;
                dto.DOC_VER = x.DOC_VER;
                dto.DOC_REV = x.DOC_REV;
                dto.CUST_CODE = x.CUST_CODE;
                dto.DOC_MONTH = x.DOC_MONTH;
                dto.DOC_YEAR = x.DOC_YEAR;

                string docStatus = x.DOC_STATUS as string;
                dto.DOC_STATUS = docStatus.ToEnum<DOCUMENT_STATUS>();
                dto.FLOW_CURRENT = x.FLOW_CURREN;
                dto.FLOW_PREVIOUS = x.FLOW_PREVIOUS;
                dto.FLOW_NEXT = x.FLOW_NEXT;

                string flagRow = x.FLAG_ROW as string;
                dto.FLAG_ROW = flagRow.ToEnum<ROW_TYPE>();
                dto.CREATED_BY = x.CREATED_BY;
                dto.CREATED_DATE = x.CREATED_DATE;
                dto.UPDATED_BY = x.UPDATED_BY;
                dto.UPDATED_DATE = x.UPDATED_DATE;

                dto.COMMENT = x.COMMENT;
                dto.DOC_ACTION = COMMAND_TYPE.NONE;
                dto.ACTOR_NAME = x.ACTOR_NAME;
                dto.ACTION_DATETIME = x.ACTION_DATETIME;
                dto.ACTOR_POSITION = null;

                result = dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public IEnumerable<DocumentHistoryDto> GetDocumentHistories(string documentType, int headerId)
        {
            string sqlQuery =
@"IF (@IN_DOC_TYPE_CODE = 'FC')
BEGIN
	SELECT 
	  DOC_FCH_ID 'DOC_HEAD_ID'
    , COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
    , CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	, ISNULL(UPDATED_BY, CREATED_BY) 'ACTOR_NAME'
	, ISNULL(UPDATED_DATE, CREATED_DATE) 'ACTION_DATETIME'
	FROM TB_T_FIXED_CONTRACT_H
	WHERE DOC_CODE IN (SELECT TOP 1 DOC_CODE FROM TB_T_FIXED_CONTRACT_H WHERE DOC_FCH_ID = @IN_DOC_HEAD_ID)
	ORDER BY DOC_TYPE_CODE, COMP_CODE, DOC_YEAR, CUST_CODE, DOC_MONTH, DOC_VER, DOC_REV, DOC_HEAD_ID
END
ELSE IF (@IN_DOC_TYPE_CODE = 'PP')
BEGIN
	SELECT
	  DOC_PROM_PH_ID 'DOC_HEAD_ID'
    , COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
    , CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	, ISNULL(UPDATED_BY, CREATED_BY) 'ACTOR_NAME'
	, ISNULL(UPDATED_DATE, CREATED_DATE) 'ACTION_DATETIME'
	FROM TB_T_PROMOTION_H
	WHERE DOC_CODE IN (SELECT TOP 1 DOC_CODE FROM TB_T_PROMOTION_H WHERE DOC_PROM_PH_ID = @IN_DOC_HEAD_ID)
	ORDER BY DOC_TYPE_CODE, COMP_CODE, DOC_YEAR, CUST_CODE, DOC_MONTH, DOC_VER, DOC_REV, DOC_HEAD_ID
END
ELSE IF (@IN_DOC_TYPE_CODE = 'FC')
BEGIN
	SELECT
	  DOC_SFCH_ID 'DOC_HEAD_ID'
    , COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
    , CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	, ISNULL(UPDATED_BY, CREATED_BY) 'ACTOR_NAME'
	, ISNULL(UPDATED_DATE, CREATED_DATE) 'ACTION_DATETIME'
	FROM TB_T_SALES_FORECAST_H
	WHERE DOC_CODE IN (SELECT TOP 1 DOC_CODE FROM TB_T_SALES_FORECAST_H WHERE DOC_SFCH_ID = @IN_DOC_HEAD_ID)
	ORDER BY DOC_TYPE_CODE, COMP_CODE, DOC_YEAR, CUST_CODE, DOC_MONTH, DOC_VER, DOC_REV, DOC_HEAD_ID
END
ELSE IF (@IN_DOC_TYPE_CODE = 'CM')
BEGIN
	SELECT
	  DOC_CLMH_ID 'DOC_HEAD_ID'
    , COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
    , CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	, ISNULL(UPDATED_BY, CREATED_BY) 'ACTOR_NAME'
	, ISNULL(UPDATED_DATE, CREATED_DATE) 'ACTION_DATETIME'
	FROM TB_T_CLAIM_H
	WHERE DOC_CODE IN (SELECT TOP 1 DOC_CODE FROM TB_T_CLAIM_H WHERE DOC_CLMH_ID = @IN_DOC_HEAD_ID)
	ORDER BY DOC_TYPE_CODE, COMP_CODE, DOC_YEAR, CUST_CODE, DOC_MONTH, DOC_VER, DOC_REV, DOC_HEAD_ID
END
ELSE IF (@IN_DOC_TYPE_CODE = 'BP')
BEGIN

	SELECT
	  DOC_BGH_ID 'DOC_HEAD_ID'
    , COMP_CODE
	, @IN_DOC_TYPE_CODE 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @IN_DOC_TYPE_CODE) 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
    , CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	, ISNULL(UPDATED_BY, CREATED_BY) 'ACTOR_NAME'
	, ISNULL(UPDATED_DATE, CREATED_DATE) 'ACTION_DATETIME'
	FROM TB_T_BUDGET_H
	WHERE DOC_CODE IN (SELECT TOP 1 DOC_CODE FROM TB_T_BUDGET_H WHERE DOC_BGH_ID = @IN_DOC_HEAD_ID)
	ORDER BY DOC_TYPE_CODE, COMP_CODE, DOC_YEAR, CUST_CODE, DOC_MONTH, DOC_VER, DOC_REV, DOC_HEAD_ID
END";

            IList<DocumentHistoryDto> result = new List<DocumentHistoryDto>();
            try
            {
            
                var parms = new
                {
                    IN_DOC_TYPE_CODE = documentType,
                    IN_DOC_HEAD_ID = headerId
                };

                var query = Connection.Query(
                    sql: sqlQuery,
                    param: parms,
                    transaction: Transaction
                    ).ToList()
                    ;
                
                if (query != null)
                {
                    result = query.Select<dynamic, DocumentHistoryDto>(x => transformDocumentHistories(x)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }

        public DocumentDto GenerateDocNo(string docTypeCode, string custCode, int docYear, int docMonth)
        {
            string sqlQuery = @"EXECUTE SP_GENERATE_DOC_CODE @IN_DOC_TYPE_CODE, @IN_DOC_YEAR, @IN_DOC_MONTH, @IN_CUST_CODE";
            //Connection.ExecuteReader()
            var query = Connection.Query<DocumentDto>(
                sql: sqlQuery,
                param: new
                {
                    IN_DOC_TYPE_CODE = docTypeCode,
                    IN_DOC_YEAR = docYear,
                    IN_DOC_MONTH = docMonth,
                    IN_CUST_CODE = custCode
                },
                commandType: CommandType.Text,
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }

        public IEnumerable<DocumentDto> All()
        {
            string sqlQuery =
@"SELECT
  A.DOC_HEAD_ID, A.DOC_TYPE_NAME
, A.DOC_CODE, A.DOC_VER, A.DOC_REV, A.COMP_CODE, A.CUST_CODE, A.DOC_MONTH, A.DOC_YEAR
, A.DOC_STATUS, A.FLOW_CURRENT, A.FLOW_PREVIOUS, A.FLOW_NEXT, A.COMMENT, A.FLAG_ROW
, A.CREATED_BY, A.CREATED_DATE, A.UPDATED_BY, A.UPDATED_DATE
FROM
(
	SELECT 
	  DOC_FCH_ID 'DOC_HEAD_ID'
	, 'FC' 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = 'FC') 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, COMP_CODE, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
	, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	FROM TB_T_FIXED_CONTRACT_H
	WHERE FLAG_ROW = 'S'
	UNION
	SELECT
	  DOC_PROM_PH_ID 'DOC_HEAD_ID'
	, 'PP' 'DOC_TYPE_CODE', (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = 'PP') 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, COMP_CODE, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
	, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	FROM TB_T_PROMOTION_H
	WHERE FLAG_ROW = 'S'
	UNION
	SELECT
	  DOC_SFCH_ID 'DOC_HEAD_ID'
	, 'SF' 'DOC_TYPE_CODE'
	, (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = 'SF') 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, COMP_CODE, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
	, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	FROM TB_T_SALES_FORECAST_H
	WHERE FLAG_ROW = 'S'
	UNION
	SELECT
	  DOC_CLMH_ID 'DOC_HEAD_ID'
	, 'CM' 'DOC_TYPE_CODE'
	, (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = 'CM') 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, COMP_CODE, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
	, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	FROM TB_T_CLAIM_H
	WHERE FLAG_ROW = 'S'
	UNION
	SELECT
	  DOC_BGH_ID 'DOC_HEAD_ID'
	, 'BP' 'DOC_TYPE_CODE'
	, (SELECT TOP 1 DOC_TYPE_NAME FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = 'BP') 'DOC_TYPE_NAME'
	, DOC_CODE, DOC_VER, DOC_REV, COMP_CODE, CUST_CODE, DOC_MONTH, DOC_YEAR
	, DOC_STATUS, FLOW_CURRENT, FLOW_PREVIOUS, FLOW_NEXT, COMMENT, FLAG_ROW
	, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE
	FROM TB_T_BUDGET_H
	WHERE FLAG_ROW = 'S'
) A
ORDER BY A.DOC_TYPE_CODE, A.COMP_CODE, A.DOC_YEAR, A.CUST_CODE, A.DOC_MONTH, A.DOC_VER, A.DOC_REV, A.DOC_HEAD_ID";

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
, ORG_CODE
, COMP_CODE
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
