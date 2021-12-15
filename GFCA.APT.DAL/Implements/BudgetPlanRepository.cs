using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain;

namespace GFCA.APT.DAL.Implements
{
    public class BudgetPlanRepository : RepositoryBase, IBudgetPlanRepository
    {

        public BudgetPlanRepository(IDbTransaction transaction) : base(transaction) { }


        public FixedContractHeaderDto GetFixedContractByItemID(int DOC_FCH_ID)
        {
            string sqlQuery =
@"SELECT 
  DOC_FCH_ID
, DOC_CODE
, DOC_VER
, DOC_REV
, CLIENT_CODE
, (SELECT TOP 1 b.CLIENT_NAME FROM TB_M_CLIENT b WHERE b.CLIENT_CODE = a.CLIENT_CODE) CLIENT_NAME
, CUST_CODE
, (SELECT TOP 1 b.CUST_NAME FROM TB_M_CUSTOMER b WHERE b.CUST_CODE = a.CUST_CODE) CUST_NAME
, CHANNEL_CODE
, (SELECT TOP 1 b.CHANNEL_NAME FROM TB_M_CHANNEL b WHERE b.CHANNEL_CODE = a.CHANNEL_CODE) CHANNEL_NAME
, DOC_STATUS
, COMMENT
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
, UPDATED_BY
, UPDATED_DATE
FROM TB_T_FIXED_CONTRACT_H a
WHERE DOC_FCH_ID = @DOC_FCH_ID;";
            var parms = new
            {
                DOC_FCH_ID = DOC_FCH_ID
            };

            var query = Connection.QueryFirstOrDefault<FixedContractHeaderDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
        public void InsertFixedContractHeader(FixedContractHeaderDto entity)
        {
            string sqlCommand = @"INSERT INTO TB_T_FIXED_CONTRACT_H
(
  DOC_CODE
, DOC_VER
, DOC_REV
, CLIENT_CODE
, CUST_CODE
, CHANNEL_CODE
, COMP_CODE
, DOC_STATUS
, COMMENT
, FLAG_ROW
, REQUESTER_ORG_CODE
, CREATED_BY
, CREATED_DATE
) VALUES (
  @DOC_CODE
, @DOC_VER
, @DOC_REV
, @CLIENT_CODE
, @CUST_CODE
, @CHANNEL_CODE
, @COMP_CODE
, @DOC_STATUS
, NULL
, @FLAG_ROW
, @REQUESTER_ORG_CODE
, @CREATED_BY
, SYSDATETIME()
); SELECT SCOPE_IDENTITY()";

            var parms = new
            {
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                CLIENT_CODE = entity.CLIENT_CODE,
                CUST_CODE = entity.CUST_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                COMP_CODE = entity.COMP_CODE,
                DOC_STATUS = entity.DOC_STATUS,
                //COMMENT          = entity.COMMENT,
                FLAG_ROW = entity.FLAG_ROW.ToValue(),
                REQUESTER_ORG_CODE = entity.ORG_CODE,
                CREATED_BY = entity.CREATED_BY
            };

            int DOC_FCH_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
            entity.DOC_FCH_ID = DOC_FCH_ID;
        }
        public void UpdateFixedContractHeader(FixedContractHeaderDto entity)
        {
            string sqlCommand = @"UPDATE TB_T_FIXED_CONTRACT_H
SET
 DOC_CODE     = @DOC_CODE
,DOC_VER      = @DOC_VER
,DOC_REV      = @DOC_REV
,CLIENT_CODE  = @CLIENT_CODE
,CUST_CODE    = @CUST_CODE
,CHANNEL_CODE = @CHANNEL_CODE
,DOC_STATUS   = @DOC_STATUS
,COMMENT      = @COMMENT
,FLAG_ROW     = @FLAG_ROW
,UPDATED_BY   = @UPDATED_BY
,UPDATED_DATE = @UPDATED_DATE
,ORG_CODE       = @ORG_CODE
,COMP_CODE      = @COMP_CODE
,REQUESTER      = @REQUESTER
WHERE DOC_FCH_ID = @DOC_FCH_ID;";

            var parms = new
            {
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                CLIENT_CODE = entity.CLIENT_CODE,
                CUST_CODE = entity.CUST_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                DOC_STATUS = entity.DOC_STATUS.ToString(),
                COMMENT = entity.COMMENT,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
                ORG_CODE = entity.ORG_CODE,
                COMP_CODE = entity.COMP_CODE,
                REQUESTER = entity.REQUESTER
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }
        public void DeleteFixedContractHeader(int DOC_FCH_ID)
        {
            string sqlCommand = @"DELETE TB_T_FIXED_CONTRACT_H WHERE DOC_FCH_ID = @DOC_FCH_ID;";

            var parms = new
            {
                DOC_FCH_ID = DOC_FCH_ID
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }

        public FixedContractDetailDto GetDetailItem(int DOC_FCD_ID, CONDITION_TYPE conditionType = CONDITION_TYPE.PLANNING)
        {
            string sqlQuery = @"SELECT
  a.DOC_FCH_ID
, a.DOC_FCD_ID
, a.DOC_CODE
, a.DOC_VER
, a.DOC_REV
, a.BRAND_CODE
, (SELECT TOP 1 b.BRAND_NAME FROM TB_M_BRAND b WHERE b.BRAND_CODE = a.BRAND_CODE) BRAND_NAME
, a.ACTIVITY_CODE
, (SELECT TOP 1 b.ACTIVTITY_NAME FROM TB_M_ACTIVITY b WHERE b.ACTIVITY_CODE = a.ACTIVITY_CODE) ACTIVTITY_NAME
, a.CENTER_CODE
, (SELECT TOP 1 b.CENTER_NAME FROM TB_M_COST_CENTER b WHERE b.CENTER_CODE = a.CENTER_CODE) CENTER_NAME
, a.ACC_CODE
, (SELECT TOP 1 b.ACC_NAME FROM TB_M_GL_ACCOUNT b WHERE b.ACC_CODE = a.ACC_CODE) ACC_NAME
, a.SIZE
, (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = a.SIZE) SIZE_NAME
, a.UOM
, a.PACK
, (SELECT TOP 1 b.PACK_NAME FROM TB_M_PACK b WHERE b.PACK_CODE = a.PACK) SIZE_NAME
, a.DATE_REF
, a.CONDITION_TYPE
, a.CONTRACT_CATE
, a.CONTRACT_DESC
, a.M01
, a.M02
, a.M03
, a.M04
, a.M05
, a.M06
, a.M07
, a.M08
, a.M09
, a.M10
, a.M11
, a.M12
, a.REMARK
, a.DOC_STATUS
, a.FLAG_ROW
, a.[START_DATE]
, a.END_DATE
, a.CREATED_BY
, a.CREATED_DATE
, a.UPDATED_BY
, a.UPDATED_DATE
FROM TB_T_FIXED_CONTRACT_D a
WHERE
CONDITION_TYPE = @CONDITION_TYPE
AND DOC_FCD_ID = @DOC_FCD_ID
;";

            var parms = new
            {
                DOC_FCD_ID = DOC_FCD_ID,
                CONDITION_TYPE = conditionType.ToString()
            };
            var query = Connection.QueryFirstOrDefault<FixedContractDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }
        public IEnumerable<FixedContractDetailDto> GetDetailItems(int DOC_FCH_ID, CONDITION_TYPE conditionType = CONDITION_TYPE.PLANNING)
        {
            string sqlQuery = @"SELECT
  a.DOC_FCH_ID
, a.DOC_FCD_ID
, a.DOC_CODE
, a.DOC_VER
, a.DOC_REV
, a.BRAND_CODE
, (SELECT TOP 1 b.BRAND_NAME FROM TB_M_BRAND b WHERE b.BRAND_CODE = a.BRAND_CODE) BRAND_NAME
, a.ACTIVITY_CODE
, (SELECT TOP 1 b.ACTIVTITY_NAME FROM TB_M_ACTIVITY b WHERE b.ACTIVITY_CODE = a.ACTIVITY_CODE) ACTIVTITY_NAME
, a.CENTER_CODE
, (SELECT TOP 1 b.CENTER_NAME FROM TB_M_COST_CENTER b WHERE b.CENTER_CODE = a.CENTER_CODE) CENTER_NAME
, a.ACC_CODE
, (SELECT TOP 1 b.ACC_NAME FROM TB_M_GL_ACCOUNT b WHERE b.ACC_CODE = a.ACC_CODE) ACC_NAME
, a.SIZE
, (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = a.SIZE) SIZE_NAME
, a.UOM
, a.PACK
, (SELECT TOP 1 b.PACK_NAME FROM TB_M_PACK b WHERE b.PACK_CODE = a.PACK) SIZE_NAME
, a.DATE_REF
, a.CONDITION_TYPE
, a.CONTRACT_CATE
, a.CONTRACT_DESC
, a.M01
, a.M02
, a.M03
, a.M04
, a.M05
, a.M06
, a.M07
, a.M08
, a.M09
, a.M10
, a.M11
, a.M12
, a.REMARK
, a.DOC_STATUS
, a.FLAG_ROW
, a.[START_DATE]
, a.END_DATE
, a.CREATED_BY
, a.CREATED_DATE
, a.UPDATED_BY
, a.UPDATED_DATE
FROM TB_T_FIXED_CONTRACT_D a
WHERE 
CONDITION_TYPE = @CONDITION_TYPE
AND DOC_FCH_ID = @DOC_FCH_ID;";

            var parms = new
            {
                DOC_FCH_ID = DOC_FCH_ID,
                CONDITION_TYPE = conditionType.ToString()
            };
            var query = Connection.Query<FixedContractDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                ).ToList();

            return query;
        }
        public IEnumerable<FixedContractDetailDto> GetDetailItems(string docCode, int docVer = -1, int docRev = -1)
        {
            string sqlQuery = @"SELECT
  a.DOC_FCH_ID
, a.DOC_FCD_ID
, a.DOC_CODE
, a.DOC_VER
, a.DOC_REV
, a.BRAND_CODE
, (SELECT TOP 1 b.BRAND_NAME FROM TB_M_BRAND b WHERE b.BRAND_CODE = a.BRAND_CODE) BRAND_NAME
, a.ACTIVITY_CODE
, (SELECT TOP 1 b.ACTIVTITY_NAME FROM TB_M_ACTIVITY b WHERE b.ACTIVITY_CODE = a.ACTIVITY_CODE) ACTIVTITY_NAME
, a.CENTER_CODE
, (SELECT TOP 1 b.CENTER_NAME FROM TB_M_COST_CENTER b WHERE b.CENTER_CODE = a.CENTER_CODE) CENTER_NAME
, a.ACC_CODE
, (SELECT TOP 1 b.ACC_NAME FROM TB_M_GL_ACCOUNT b WHERE b.ACC_CODE = a.ACC_CODE) ACC_NAME
, a.SIZE
, (SELECT TOP 1 b.SIZE_NAME FROM TB_M_SIZE b WHERE b.SIZE_CODE = a.SIZE) SIZE_NAME
, a.UOM
, a.PACK
, (SELECT TOP 1 b.PACK_NAME FROM TB_M_PACK b WHERE b.PACK_CODE = a.PACK) SIZE_NAME
, a.DATE_REF
, a.CONDITION_TYPE
, a.CONTRACT_CATE
, a.CONTRACT_DESC
, a.M01
, a.M02
, a.M03
, a.M04
, a.M05
, a.M06
, a.M07
, a.M08
, a.M09
, a.M10
, a.M11
, a.M12
, a.REMARK
, a.DOC_STATUS
, a.FLAG_ROW
, a.[START_DATE]
, a.END_DATE
, a.CREATED_BY
, a.CREATED_DATE
, a.UPDATED_BY
, a.UPDATED_DATE
FROM TB_T_FIXED_CONTRACT_D a
WHERE
CONDITION_TYPE = 'PLANNING'
AND DOC_CODE = @DOC_CODE
;";

            var parms = new
            {
                DOC_CODE = docCode
            };
            var query = Connection.Query<FixedContractDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                ).ToList();

            return query;
        }
        public void InsertFixedContractDetail(FixedContractDetailDto entity)
        {
            string sqlCommand = @"INSERT INTO TB_T_FIXED_CONTRACT_D
(
  DOC_FCH_ID
, DOC_CODE
, DOC_VER
, DOC_REV
, BRAND_CODE
, ACTIVITY_CODE
, CENTER_CODE
, ACC_CODE
, SIZE
, UOM
, PACK
, DATE_REF
, CONDITION_TYPE
, CONTRACT_CATE
, CONTRACT_DESC
, M01
, M02
, M03
, M04
, M05
, M06
, M07
, M08
, M09
, M10
, M11
, M12
, REMARK
, DOC_STATUS
, FLAG_ROW
, START_DATE
, END_DATE
, CREATED_BY
, CREATED_DATE
) VALUES (
  @DOC_FCH_ID
, @DOC_CODE
, @DOC_VER
, @DOC_REV
, @BRAND_CODE
, @ACTIVITY_CODE
, @CENTER_CODE
, @ACC_CODE
, @SIZE
, @UOM
, @PACK
, @DATE_REF
, @CONDITION_TYPE
, @CONTRACT_CATE
, @CONTRACT_DESC
, @M01
, @M02
, @M03
, @M04
, @M05
, @M06
, @M07
, @M08
, @M09
, @M10
, @M11
, @M12
, @REMARK
, @DOC_STATUS
, @FLAG_ROW
, @START_DATE
, @END_DATE
, @CREATED_BY
, @CREATED_DATE
); SELECT SCOPE_IDENTITY()";

            var parms = new
            {
                DOC_FCH_ID = entity.DOC_FCH_ID,
                DOC_FCD_ID = entity.DOC_FCD_ID,
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                BRAND_CODE = entity.BRAND_CODE,
                ACTIVITY_CODE = entity.ACTIVITY_CODE,
                CENTER_CODE = entity.CENTER_CODE,
                ACC_CODE = entity.ACC_CODE,
                SIZE = entity.SIZE,
                UOM = entity.UOM,
                PACK = entity.PACK,
                DATE_REF = entity.DATE_REF,
                CONDITION_TYPE = entity.CONDITION_TYPE.ToString(),
                CONTRACT_CATE = entity.CONTRACT_CATE,
                CONTRACT_DESC = entity.CONTRACT_DESC,
                M01 = entity.M01,
                M02 = entity.M02,
                M03 = entity.M03,
                M04 = entity.M04,
                M05 = entity.M05,
                M06 = entity.M06,
                M07 = entity.M07,
                M08 = entity.M08,
                M09 = entity.M09,
                M10 = entity.M10,
                M11 = entity.M11,
                M12 = entity.M12,
                REMARK = entity.REMARK,
                DOC_STATUS = entity.DOC_STATUS.ToString(),
                START_DATE = entity.START_DATE?.ToDateTime2(),
                END_DATE = entity.END_DATE?.ToDateTime2(),
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            int DOC_FCD_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

            entity.DOC_FCD_ID = DOC_FCD_ID;
        }
        public void UpdateFixedContractDetail(FixedContractDetailDto entity)
        {
            string sqlCommand = @"UPDATE TB_T_FIXED_CONTRACT_D
SET
  BRAND_CODE     = @BRAND_CODE
, ACTIVITY_CODE  = @ACTIVITY_CODE
, CENTER_CODE    = @CENTER_CODE
, ACC_CODE       = @ACC_CODE
, SIZE           = @SIZE
, UOM            = @UOM
, PACK           = @PACK
, DATE_REF       = @DATE_REF
, CONDITION_TYPE = @CONDITION_TYPE
, CONTRACT_CATE  = @CONTRACT_CATE
, CONTRACT_DESC  = @CONTRACT_DESC
, M01            = @M01
, M02            = @M02
, M03            = @M03
, M04            = @M04
, M05            = @M05
, M06            = @M06
, M07            = @M07
, M08            = @M08
, M09            = @M09
, M10            = @M10
, M11            = @M11
, M12            = @M12
, REMARK        = @REMARK
, DOC_STATUS     = @DOC_STATUS
, FLAG_ROW       = @FLAG_ROW
, START_DATE     = @START_DATE
, END_DATE       = @END_DATE
, UPDATED_BY     = @UPDATED_BY
, UPDATED_DATE   = @UPDATED_DATE
WHERE DOC_FCD_ID = @DOC_FCD_ID
;";

            /*
             DOC_CODE       = @DOC_CODE
            AND DOC_FCH_ID = @DOC_FCH_ID

             */

            var parms = new
            {
                DOC_FCH_ID = entity.DOC_FCH_ID,
                DOC_FCD_ID = entity.DOC_FCD_ID,
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                BRAND_CODE = entity.BRAND_CODE,
                ACTIVITY_CODE = entity.ACTIVITY_CODE,
                CENTER_CODE = entity.CENTER_CODE,
                ACC_CODE = entity.ACC_CODE,
                SIZE = entity.SIZE,
                UOM = entity.UOM,
                PACK = entity.PACK,
                DATE_REF = entity.DATE_REF,
                CONDITION_TYPE = entity.CONDITION_TYPE.ToString(),
                CONTRACT_CATE = entity.CONTRACT_CATE,
                CONTRACT_DESC = entity.CONTRACT_DESC,
                M01 = entity.M01,
                M02 = entity.M02,
                M03 = entity.M03,
                M04 = entity.M04,
                M05 = entity.M05,
                M06 = entity.M06,
                M07 = entity.M07,
                M08 = entity.M08,
                M09 = entity.M09,
                M10 = entity.M10,
                M11 = entity.M11,
                M12 = entity.M12,
                REMARK = entity.REMARK,
                DOC_STATUS = entity.DOC_STATUS.ToString(),
                START_DATE = entity.START_DATE?.ToDateTime2(),
                END_DATE = entity.END_DATE?.ToDateTime2(),
                FLAG_ROW = entity.FLAG_ROW,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2(),
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

        }
        public void DeleteFixedContractDetail(int DOC_FCD_ID)
        {
            string sqlCommand = @"DELETE TB_T_FIXED_CONTRACT_D WHERE DOC_FCD_ID = @DOC_FCD_ID;";

            var parms = new
            {
                DOC_FCD_ID = DOC_FCD_ID
            };

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }



        public IEnumerable<BudgetPlanHeaderDto> GetBudgetPlanAll()
        {
            string sqlQuery = @"SELECT A.[DOC_BGH_ID]
      ,A.[DOC_CODE]
      ,A.[BG_TYPE_CODE]
      ,A.[COMP_CODE]
	  ,(SELECT TOP 1 isnull(b.COMP_NAME,'') FROM [TB_M_COMPANY] b WHERE b.COMP_CODE = A.[COMP_CODE]) COMP_NAME
      ,A.[CREATED_BY]
      ,A.[CREATED_DATE]
      ,A.[UPDATED_BY]
      ,A.[UPDATED_DATE]
  FROM [dbo].[TB_T_BUDGET_H] AS A;";
            var query = Connection.Query<BudgetPlanHeaderDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;

        }

        public IEnumerable<FixedContractHeaderDto> GetFixedContractAll()
        {
            throw new NotImplementedException();
        }


        public BudgetPlanHeaderDto BudgetPlanByID(int DOC_BGH_ID)
        {


            string sqlQuery =
@"SELECT 
 *
FROM TB_T_BUDGET_H a
WHERE DOC_BGH_ID = @DOC_BGH_ID;";
            var parms = new
            {
                DOC_BGH_ID = DOC_BGH_ID
            };

            var query = Connection.QueryFirstOrDefault<BudgetPlanHeaderDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }

        public void InsertBudgetPlanHeaderHeader(BudgetPlanHeaderDto entity)
        {
            string sqlCommand = @"INSERT INTO TB_T_BUDGET_H
(DOC_CODE,
DOC_VER,
DOC_REV,
BG_TYPE_CODE,
COMP_CODE,
CUST_CODE,
FISCAL_YEAR,
BUDGET_AMOUNT,
ACTUAL,
COMMITMENT,
AVAILABLE,
FLAG_ROW,
CREATED_BY,
UPDATED_BY,
UPDATED_DATE,
CREATED_DATE
)
VALUES
(
 @DOC_CODE,
@DOC_VER,
@DOC_REV,
@BG_TYPE_CODE,
@COMP_CODE,
@CUST_CODE,
@FISCAL_YEAR,
@BUDGET_AMOUNT,
@ACTUAL,
@COMMITMENT,
@AVAILABLE,
@FLAG_ROW,
@CREATED_BY,
@UPDATED_BY,
@UPDATED_DATE,
@CREATED_DATE
); SELECT SCOPE_IDENTITY()";

            var parms = new
            {

                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                BG_TYPE_CODE = entity.BG_TYPE_CODE,
                COMP_CODE = entity.COMP_CODE,
                CUST_CODE = entity.CUST_CODE,
                FISCAL_YEAR = entity.FISCAL_YEAR,
                BUDGET_AMOUNT = entity.BUDGET_AMOUNT,
                ACTUAL = entity.ACTUAL,
                COMMITMENT = entity.COMMITMENT,
                AVAILABLE = entity.AVAILABLE,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE,





            };

            int DOC_BGH_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
            entity.DOC_BGH_ID = DOC_BGH_ID;
        }

        public BudgetPlanHeaderDto GetBudgetPlanID(int DOC_BGH_ID)
        {

            string sqlQuery =
@"SELECT 
 *
FROM TB_T_BUDGET_H a
WHERE DOC_BGH_ID = @DOC_BGH_ID;";
            var parms = new
            {
                DOC_BGH_ID = DOC_BGH_ID
            };

            var query = Connection.QueryFirstOrDefault<BudgetPlanHeaderDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }

        public void InsertBudgetPlanSale(BudgetPlanSaleDto entity)
        {

            string sqlCommand = @"INSERT INTO TB_T_BUDGET_SALES_D
([DOC_BGH_ID]
           ,[BRAND_CODE]
           ,[PACK_CODE]
           ,[SIZE_CODE]
           ,[PRD_CODE]
           ,[COST_ELEMENT_CODE]
           ,[COST_CENTER]
           ,[YEAR]
           ,[MONTH]
           ,[TOTAL]
           ,[M1]
           ,[M2]
           ,[M3]
           ,[M4]
           ,[M5]
           ,[M6]
           ,[M7]
           ,[M8]
           ,[M9]
           ,[M10]
           ,[M11]
           ,[M12]
           ,[FLAG_ROW]
           ,[CREATED_BY]
           ,[CREATED_DATE]
           ,[UPDATED_BY]
           ,[UPDATED_DATE]
)
VALUES
(
 @DOC_BGH_ID
           ,@BRAND_CODE
           ,@PACK_CODE
           ,@SIZE_CODE
           ,@PRD_CODE
           ,@COST_ELEMENT_CODE
           ,@COST_CENTER
           ,@YEAR
           ,@MONTH
           ,@TOTAL
           ,@M1
           ,@M2
           ,@M3
           ,@M4
           ,@M5
           ,@M6
           ,@M7
           ,@M8
           ,@M9
           ,@M10
           ,@M11
           ,@M12
           ,@FLAG_ROW
           ,@CREATED_BY
           ,@CREATED_DATE
           ,@UPDATED_BY
           ,@UPDATED_DATE
); SELECT SCOPE_IDENTITY()";



            var parms = new
            {
                DOC_BGH_ID = entity.DOC_BGH_ID,

                BRAND_CODE = entity.BRAND_CODE,
                PACK_CODE = entity.PACK_CODE,
                SIZE_CODE = entity.SIZE_CODE,
                PRD_CODE = entity.PRD_CODE,
                COST_ELEMENT_CODE = entity.COST_ELEMENT_CODE,
                COST_CENTER = entity.COST_CENTER,
                YEAR = entity.YEAR,
                MONTH = entity.MONTH,
                TOTAL = entity.TOTAL,
                M1 = entity.M1,
                M2 = entity.M2,
                M3 = entity.M3,
                M4 = entity.M4,
                M5 = entity.M5,
                M6 = entity.M6,
                M7 = entity.M7,
                M8 = entity.M8,
                M9 = entity.M9,
                M10 = entity.M10,
                M11 = entity.M11,
                M12 = entity.M12,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE,
            };



            int DOC_BGH_SALES_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
            entity.DOC_BGH_SALES_ID = DOC_BGH_SALES_ID;
        }

        public void InsertBudgetPlanInvestment(BudgetPlanInvestmentDto entity)
        {
            string sqlCommand = @"INSERT INTO TB_T_BUDGET_INVESTMENT_D
([DOC_BGH_ID]
,[ACTIVITY_CODE]
           ,[BRAND_CODE]
           ,[PACK_CODE]
           ,[SIZE_CODE]
           ,[PRD_CODE]
           ,[COST_ELEMENT_CODE]
           ,[COST_CENTER]
           ,[YEAR]
           ,[MONTH]
           ,[TOTAL]
           ,[M1]
           ,[M2]
           ,[M3]
           ,[M4]
           ,[M5]
           ,[M6]
           ,[M7]
           ,[M8]
           ,[M9]
           ,[M10]
           ,[M11]
           ,[M12]
           ,[FLAG_ROW]
           ,[CREATED_BY]
           ,[CREATED_DATE]
           ,[UPDATED_BY]
           ,[UPDATED_DATE]
)
VALUES
(
 @DOC_BGH_ID,@ACTIVITY_CODE
           ,@BRAND_CODE
           ,@PACK_CODE
           ,@SIZE_CODE
           ,@PRD_CODE
           ,@COST_ELEMENT_CODE
           ,@COST_CENTER
           ,@YEAR
           ,@MONTH
           ,@TOTAL
           ,@M1
           ,@M2
           ,@M3
           ,@M4
           ,@M5
           ,@M6
           ,@M7
           ,@M8
           ,@M9
           ,@M10
           ,@M11
           ,@M12
           ,@FLAG_ROW
           ,@CREATED_BY
           ,@CREATED_DATE
           ,@UPDATED_BY
           ,@UPDATED_DATE
); SELECT SCOPE_IDENTITY()";



            var parms = new
            {
                DOC_BGH_ID = entity.DOC_BGH_ID,
                ACTIVITY_CODE = entity.ACTIVITY_CODE,
                BRAND_CODE = entity.BRAND_CODE,
                PACK_CODE = entity.PACK_CODE,
                SIZE_CODE = entity.SIZE_CODE,
                PRD_CODE = entity.PRD_CODE,
                COST_ELEMENT_CODE = entity.COST_ELEMENT_CODE,
                COST_CENTER = entity.COST_CENTER,
                YEAR = entity.YEAR,
                MONTH = entity.MONTH,
                TOTAL = entity.TOTAL,
                M1 = entity.M1,
                M2 = entity.M2,
                M3 = entity.M3,
                M4 = entity.M4,
                M5 = entity.M5,
                M6 = entity.M6,
                M7 = entity.M7,
                M8 = entity.M8,
                M9 = entity.M9,
                M10 = entity.M10,
                M11 = entity.M11,
                M12 = entity.M12,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE,
            };



            int DOC_BGH_INV_ID = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
            entity.DOC_BGH_INV_ID = DOC_BGH_INV_ID;
        }

        public IEnumerable<BudgetPlanSaleDto> GetDetailSalesItems(int DOC_BGH_ID)
        {
            string sqlQuery = @"SELECT [DOC_BGH_ID]
      ,[DOC_BGH_SALES_ID]
      ,[BRAND_CODE]
	  ,(SELECT TOP 1 isnull(B.BRAND_NAME,'') FROM TB_M_BRAND b WHERE b.BRAND_CODE = A.BRAND_CODE) BRAND_NAME
      ,[PACK_CODE]
	  ,(SELECT TOP 1 isnull(P.PACK_NAME,'') FROM TB_M_PACK P WHERE P.PACK_CODE = A.PACK_CODE) PACK_NAME
      ,[SIZE_CODE]
	  ,(SELECT TOP 1 isnull(S.SIZE_NAME,'') FROM TB_M_SIZE S WHERE S.SIZE_CODE = A.SIZE_CODE) SIZE_NAME
      ,[PRD_CODE]
	  ,(SELECT TOP 1 isnull(PD.PROD_NAME,'') FROM TB_M_PRODUCT PD WHERE PD.PROD_CODE = A.PRD_CODE) PROD_NAME
      ,[COST_ELEMENT_CODE]
      ,[COST_CENTER]
	  ,(SELECT TOP 1 isnull(C.CENTER_NAME,'') FROM TB_M_COST_CENTER C WHERE C.CENTER_CODE = A.COST_CENTER) CENTER_NAME
      ,[YEAR]
      ,[MONTH]
      ,[TOTAL]
      ,[M1]
      ,[M2]
      ,[M3]
      ,[M4]
      ,[M5]
      ,[M6]
      ,[M7]
      ,[M8]
      ,[M9]
      ,[M10]
      ,[M11]
      ,[M12]
      --,[FLAG_ROW]
      ,[CREATED_BY]
      ,[CREATED_DATE]
      ,[UPDATED_BY]
      ,[UPDATED_DATE]
  FROM [dbo].[TB_T_BUDGET_SALES_D] AS A
  WHERE A.DOC_BGH_ID=@DOC_BGH_ID
;";
            var parms = new
            {
                DOC_BGH_ID = DOC_BGH_ID
            };

            var query = Connection.Query<BudgetPlanSaleDto>(
                sql: sqlQuery,
                param: parms
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public IEnumerable<BudgetPlanInvestmentDto> GetDetailInvItems(int DOC_BGH_ID)
        {
            string sqlQuery = @"SELECT 
      [DOC_BGH_ID]
      ,[DOC_BGH_INV_ID]
      ,[BRAND_CODE]
	  ,[ACTIVITY_CODE]
	  ,(SELECT TOP 1 isnull(B.BRAND_NAME,'') FROM TB_M_BRAND b WHERE b.BRAND_CODE = A.BRAND_CODE) BRAND_NAME
      ,[PACK_CODE]
	  ,(SELECT TOP 1 isnull(P.PACK_NAME,'') FROM TB_M_PACK P WHERE P.PACK_CODE = A.PACK_CODE) PACK_NAME
      ,[SIZE_CODE]
	  ,(SELECT TOP 1 isnull(S.SIZE_NAME,'') FROM TB_M_SIZE S WHERE S.SIZE_CODE = A.SIZE_CODE) SIZE_NAME
      ,[PRD_CODE]
	  ,(SELECT TOP 1 isnull(PD.PROD_NAME,'') FROM TB_M_PRODUCT PD WHERE PD.PROD_CODE = A.PRD_CODE) PROD_NAME
      ,[COST_ELEMENT_CODE]
      ,[COST_CENTER]
	  ,(SELECT TOP 1 isnull(C.CENTER_NAME,'') FROM TB_M_COST_CENTER C WHERE C.CENTER_CODE = A.COST_CENTER) CENTER_NAME
      ,[TYPE]
	  ,[YEAR]
      ,[MONTH]
      ,[TOTAL]
      ,[M1]
      ,[M2]
      ,[M3]
      ,[M4]
      ,[M5]
      ,[M6]
      ,[M7]
      ,[M8]
      ,[M9]
      ,[M10]
      ,[M11]
      ,[M12]
     -- ,[FLAG_ROW]
      ,[CREATED_BY]
      ,[CREATED_DATE]
      ,[UPDATED_BY]
      ,[UPDATED_DATE]
  FROM [dbo].TB_T_BUDGET_INVESTMENT_D AS A
  WHERE A.DOC_BGH_ID=@DOC_BGH_ID;";
            var parms = new
            {
                DOC_BGH_ID = DOC_BGH_ID
            };

            var query = Connection.Query<BudgetPlanInvestmentDto>(
                sql: sqlQuery,
                param: parms
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public BudgetPlanSaleDto GetDetailSalesItem(int DOC_BGH_SALES_ID)
        {

            string sqlQuery =
@"SELECT [DOC_BGH_ID]
      ,[DOC_BGH_SALES_ID]
      ,[BRAND_CODE]
      ,[PACK_CODE]
      ,[SIZE_CODE]
      ,[PRD_CODE]
      ,[COST_ELEMENT_CODE]
      ,[COST_CENTER]
      ,[YEAR]
      ,[MONTH]
      ,[TOTAL]
      ,[M1]
      ,[M2]
      ,[M3]
      ,[M4]
      ,[M5]
      ,[M6]
      ,[M7]
      ,[M8]
      ,[M9]
      ,[M10]
      ,[M11]
      ,[M12]
      --,[FLAG_ROW]
      ,[CREATED_BY]
      ,[CREATED_DATE]
      ,[UPDATED_BY]
      ,[UPDATED_DATE]
  FROM [dbo].[TB_T_BUDGET_SALES_D] 
WHERE DOC_BGH_SALES_ID = @DOC_BGH_SALES_ID;";
            var parms = new
            {
                DOC_BGH_SALES_ID = DOC_BGH_SALES_ID
            };

            var query = Connection.QueryFirstOrDefault<BudgetPlanSaleDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }

        public BudgetPlanInvestmentDto GetDetailInvItem(int DOC_BGH_INV_ID)
        {

            string sqlQuery =
@"SELECT [DOC_BGH_ID]
      ,[DOC_BGH_INV_ID]
      ,[BRAND_CODE]
      ,[PACK_CODE]
      ,[SIZE_CODE]
      ,[PRD_CODE]
      ,[COST_ELEMENT_CODE]
      ,[ACTIVITY_CODE]
      ,[COST_CENTER]
      ,[TYPE]
      ,[YEAR]
      ,[MONTH]
      ,[TOTAL]
      ,[M1]
      ,[M2]
      ,[M3]
      ,[M4]
      ,[M5]
      ,[M6]
      ,[M7]
      ,[M8]
      ,[M9]
      ,[M10]
      ,[M11]
      ,[M12]
      --,[FLAG_ROW]
      ,[CREATED_BY]
      ,[CREATED_DATE]
      ,[UPDATED_BY]
      ,[UPDATED_DATE]
  FROM [dbo].[TB_T_BUDGET_INVESTMENT_D]
  WHERE DOC_BGH_INV_ID = @DOC_BGH_INV_ID;";
            var parms = new
            {
                DOC_BGH_INV_ID = DOC_BGH_INV_ID
            };

            var query = Connection.QueryFirstOrDefault<BudgetPlanInvestmentDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;
        }

      
        public void UpdateBudgetPlanSale(BudgetPlanSaleDto entity)
        {
            string sqlCommand = @"
UPDATE  TB_T_BUDGET_SALES_D 
SET
 BRAND_CODE = @BRAND_CODE
,PACK_CODE = @PACK_CODE
,SIZE_CODE = @SIZE_CODE
,PRD_CODE = @PRD_CODE
,COST_ELEMENT_CODE = @COST_ELEMENT_CODE
,COST_CENTER = @COST_CENTER
,YEAR = @YEAR
,MONTH = @MONTH
,TOTAL = @TOTAL
,M1 = @M1
,M2 = @M2
,M3 = @M3
,M4 = @M4
,M5 = @M5
,M6 = @M6
,M7 = @M7
,M8 = @M8
,M9 = @M9
,M10 = @M10
,M11 = @M11
,M12 = @M12
--,FLAG_ROW = @FLAG_ROW
--,CREATED_BY = @CREATED_BY
--,CREATED_DATE = @CREATED_DATE
,UPDATED_BY = @UPDATED_BY
,UPDATED_DATE = @UPDATED_DATE
WHERE DOC_BGH_SALES_ID = @DOC_BGH_SALES_ID;";



            var parms = new
            {
                DOC_BGH_SALES_ID=entity.DOC_BGH_SALES_ID,
                BRAND_CODE = entity.BRAND_CODE,
                PACK_CODE = entity.PACK_CODE,
                SIZE_CODE = entity.SIZE_CODE,
                PRD_CODE = entity.PRD_CODE,
                COST_ELEMENT_CODE = entity.COST_ELEMENT_CODE,
                COST_CENTER = entity.COST_CENTER,
                YEAR = entity.YEAR,
                MONTH = entity.MONTH,
                TOTAL = entity.TOTAL,
                M1 = entity.M1,
                M2 = entity.M2,
                M3 = entity.M3,
                M4 = entity.M4,
                M5 = entity.M5,
                M6 = entity.M6,
                M7 = entity.M7,
                M8 = entity.M8,
                M9 = entity.M9,
                M10 = entity.M10,
                M11 = entity.M11,
                M12 = entity.M12,           
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2()
            };



         

            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );

        }
        public void UpdateBudgetInvsSale(BudgetPlanInvestmentDto entity)
        {
            string sqlCommand = @"
UPDATE  TB_T_BUDGET_INVESTMENT_D 
SET
 BRAND_CODE = @BRAND_CODE
,PACK_CODE = @PACK_CODE
,SIZE_CODE = @SIZE_CODE
,PRD_CODE = @PRD_CODE
,COST_ELEMENT_CODE = @COST_ELEMENT_CODE
,COST_CENTER = @COST_CENTER
,ACTIVITY_CODE=@ACTIVITY_CODE
,TYPE =@TYPE
,YEAR = @YEAR
,MONTH = @MONTH
,TOTAL = @TOTAL
,M1 = @M1
,M2 = @M2
,M3 = @M3
,M4 = @M4
,M5 = @M5
,M6 = @M6
,M7 = @M7
,M8 = @M8
,M9 = @M9
,M10 = @M10
,M11 = @M11
,M12 = @M12
--,FLAG_ROW = @FLAG_ROW
--,CREATED_BY = @CREATED_BY
--,CREATED_DATE = @CREATED_DATE
,UPDATED_BY = @UPDATED_BY
,UPDATED_DATE = @UPDATED_DATE
WHERE DOC_BGH_INV_ID = @DOC_BGH_INV_ID;";



            var parms = new
            {
                DOC_BGH_INV_ID=entity.DOC_BGH_INV_ID,
                BRAND_CODE = entity.BRAND_CODE,
                PACK_CODE = entity.PACK_CODE,
                SIZE_CODE = entity.SIZE_CODE,
                PRD_CODE = entity.PRD_CODE,
                COST_ELEMENT_CODE = entity.COST_ELEMENT_CODE,
                COST_CENTER = entity.COST_CENTER,
                ACTIVITY_CODE = entity.ACTIVITY_CODE,
                TYPE = entity.TYPE,
                YEAR = entity.YEAR,
                MONTH = entity.MONTH,
                TOTAL = entity.TOTAL,
                M1 = entity.M1,
                M2 = entity.M2,
                M3 = entity.M3,
                M4 = entity.M4,
                M5 = entity.M5,
                M6 = entity.M6,
                M7 = entity.M7,
                M8 = entity.M8,
                M9 = entity.M9,
                M10 = entity.M10,
                M11 = entity.M11,
                M12 = entity.M12,


                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2()
            };





            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }

        public void DeleteBudgetPlanSale(long DOC_BGH_SALES_ID)
        {
            string sqlCommand = @"DELETE   TB_T_BUDGET_SALES_D WHERE DOC_BGH_SALES_ID = @DOC_BGH_SALES_ID;";



            var parms = new
            {
                DOC_BGH_SALES_ID = DOC_BGH_SALES_ID
            };





            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }

        public void DeleteBudgetInvsSale(long DOC_BGH_INV_ID)
        {
            string sqlCommand = @"DELETE   TB_T_BUDGET_INVESTMENT_D WHERE DOC_BGH_INV_ID = @DOC_BGH_INV_ID;";



            var parms = new
            {
                DOC_BGH_INV_ID = DOC_BGH_INV_ID
            };





            int effected = Connection.ExecuteScalar<int>(
                sql: sqlCommand,
                param: parms,
                transaction: Transaction
            );
        }

         
    }
}
