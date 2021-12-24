using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;

namespace GFCA.APT.DAL.Implements
{
    public class BulkInsertBestPracticesRepository
        : RepositoryBase
        , IBulkInsertBestPracticesRepository
    {
        public BulkInsertBestPracticesRepository(IDbTransaction transaction) : base(transaction) { }

        /*
            --Type
            CREATE TYPE T_S_TARGET_TABLE as table
            (
              ROW_INDEX     [int] NOT NULL
            , PROD_CODE     [T_CODE] NOT NULL
            , FISCAL_YEAR   [int] NOT NULL
            , FISCAL_MONTH  [int] NOT NULL
            , BUDGET_AMOUNT [T_MONEY] NULL
            , UPLOAD_BY     [T_NAME] NOT NULL
            , UPLOAD_DATE   [T_DATETIME] NOT NULL
            )
            --Staging table
            CREATE TABLE TB_S_TARGET_TABLE
            (
              ROW_INDEX     [int] IDENTITY(1,1) NOT NULL
            , PROD_CODE     [T_CODE] NOT NULL
            , FISCAL_YEAR   [int] NOT NULL
            , FISCAL_MONTH  [int] NOT NULL
            , BUDGET_AMOUNT [T_MONEY] NULL
            , UPLOAD_BY     [T_NAME] NOT NULL
            , UPLOAD_DATE   [T_DATETIME] NOT NULL
            )
            -- On System table
            CREATE TABLE TB_M_TARGET_TABLE
            (
              PROD_CODE      [T_CODE] NOT NULL
            , FISCAL_YEAR    [int] NOT NULL
            , FISCAL_MONTH   [int] NOT NULL
            , BUDGET_AMOUNT  [T_MONEY] NULL
            , CREATED_BY     [T_NAME] NOT NULL
            , CREATED_DATE   [T_DATETIME] NOT NULL
            , UPDATED_BY     [T_NAME] NULL
            , UPDATED_DATE   [T_DATETIME] NULL
            )
            */
        public IEnumerable<BulkMessageDto> ValidationTableStaging()
        {
            string sqlQuery =
@"SELECT
  S.ROW_INDEX
, S.PROD_CODE
, S.FISCAL_YEAR
, S.FISCAL_MONTH
, S.BUDGET_AMOUNT
, S.UPLOAD_BY
, S.UPLOAD_DATE
FROM TB_S_TARGET_TABLE S
INNER JOIN TB_M_TARGET_TABLE M ON 
    M.PROD_CODE    = S.PROD_CODE 
AND M.FISCAL_YEAR  = S.FISCAL_YEAR 
AND M.FISCAL_MONTH = S.FISCAL_MONTH";

            var query = Connection.Query<TableStagingDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            IList<BulkMessageDto> bmsg = new List<BulkMessageDto>();
            query.ForEach(i => bmsg.Add(new BulkMessageDto 
            {
                ROW_INDEX = i.ROW_INDEX, 
                BULK_MESSAGE = $"Duplicated on product = {i.PROD_CODE}, year = {i.FISCAL_YEAR} and month = {i.FISCAL_MONTH}" 
            }));

            return bmsg.AsEnumerable();

        }
        /*
        public void ClearTableStaging()
        {
            string sqlCommand = "TRUNCATE TABLE TB_S_TARGET_TABLE;";
            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                transaction: Transaction
            );
        }
        */
        public bool InsertTableStaging(IEnumerable<TableStagingDto> bulkData)
        {
            string sqlCommand =
@"TRUNCATE TABLE TB_S_TARGET_TABLE;
INSERT INTO TB_S_TARGET_TABLE 
(
  PROD_CODE
, FISCAL_YEAR
, FISCAL_MONTH
, BUDGET_AMOUNT
, UPLOAD_BY
, UPLOAD_DATE
)
SELECT
  PROD_CODE
, FISCAL_YEAR
, FISCAL_MONTH
, BUDGET_AMOUNT
, UPLOAD_BY
, SYSDATETIME() UPLOAD_DATE
FROM @data";

            var dt = bulkData.ToList().ToDataTable<TableStagingDto>();
            var dataParam = new
            { 
                data = dt.AsTableValuedParameter("T_S_TARGET_TABLE")
            };
            /*
            int effected = -1;
            string conString = "Data Source=123.253.61.68;Initial Catalog=APT2Db3;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;User Id=DbaAdmin;Password=Sm@rt1t2021;";
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(conString))
            {
                conn.Open();
                using (var comm = new System.Data.SqlClient.SqlCommand(sqlCommand, conn))
                {
                    var param = comm.Parameters.AddWithValue("@data", dt);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "T_S_TARGET_TABLE";
                    
                    effected = comm.ExecuteNonQuery();
                }
            }
            */
            
            int effected = Connection.Execute(
                sql: sqlCommand,
                param: dataParam,
                commandType: CommandType.Text,
                transaction: Transaction
            );
            
            return effected > 0;
        }
        public bool InsertTableReal()
        {
            string sqlCommand =
@"UPDATE M
SET
  M.BUDGET_AMOUNT = S.BUDGET_AMOUNT
, M.UPDATED_BY    = S.UPLOAD_BY
, M.UPDATED_DATE  = S.UPLOAD_DATE
FROM TB_M_TARGET_TABLE M
INNER JOIN TB_S_TARGET_TABLE S ON S.PROD_CODE = M.PROD_CODE
WHERE S.FISCAL_YEAR = M.FISCAL_YEAR 
AND S.FISCAL_MONTH = M.FISCAL_MONTH;

INSERT INTO TB_M_TARGET_TABLE
(
  PROD_CODE
, FISCAL_YEAR
, FISCAL_MONTH
, BUDGET_AMOUNT
, CREATED_BY
, CREATED_DATE
)
SELECT
  S.PROD_CODE
, S.FISCAL_YEAR
, S.FISCAL_MONTH
, S.BUDGET_AMOUNT
, S.UPLOAD_BY
, S.UPLOAD_DATE
FROM TB_S_TARGET_TABLE S
WHERE NOT EXISTS(
	SELECT M.*
	FROM TB_M_TARGET_TABLE M
	INNER JOIN TB_M_TARGET_TABLE S ON S.PROD_CODE = M.PROD_CODE 
	WHERE S.FISCAL_YEAR = M.FISCAL_YEAR 
	AND S.FISCAL_MONTH = M.FISCAL_MONTH
);";


            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                transaction: Transaction
            );

            return effected > 0;
        }

    }

}
