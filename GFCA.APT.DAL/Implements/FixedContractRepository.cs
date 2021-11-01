using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;
using GFCA.APT.Domain.Enums;

namespace GFCA.APT.DAL.Implements
{
    public class FixedContractRepository : RepositoryBase, IFixedContractRepository
    {

        public FixedContractRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<FixedContractHeaderDto> GetHeaderAll()
        {
            string sqlQuery = @"SELECT  DOC_FCH_ID
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
  FROM TB_T_FIXED_CONTRACT_H a;";
            var query = Connection.Query<FixedContractHeaderDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;

        }
        public FixedContractHeaderDto GetHeaderById(int DOC_FCH_ID)
        {
            string sqlQuery = @"SELECT  DOC_FCH_ID
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
WHERE DOC_FCH_ID = @DOC_FCH_ID
;";
            var parms = new
            {
                DOC_FCH_ID = DOC_FCH_ID
            };

            var query = Connection.Query<FixedContractHeaderDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public void InsertHeader(FixedContractHeaderDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_T_FIXED_CONTRACT_H
(
  DOC_CODE
, DOC_VER
, DOC_REV
, CLIENT_CODE
, CUST_CODE
, CHANNEL_CODE
, DOC_STATUS
, COMMENT
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
, ORG_CODE
, COMP_CODE
, REQUESTER
) VALUES (
  @DOC_CODE
, @DOC_VER
, @DOC_REV
, @CLIENT_CODE
, @CUST_CODE
, @CHANNEL_CODE
, @DOC_STATUS
, @COMMENT
, @FLAG_ROW
, @CREATED_BY
, @CREATED_DATE
, @ORG_CODE
, @COMP_CODE
, @REQUESTER
);";

            var parms = new
            {
                DOC_CODE = entity.DOC_CODE,
                DOC_VER = entity.DOC_VER,
                DOC_REV = entity.DOC_REV,
                CLIENT_CODE = entity.CLIENT_CODE,
                CUST_CODE = entity.CUST_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                DOC_STATUS = entity.DOC_STATUS,
                COMMENT = entity.COMMENT,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
                ORG_CODE = entity.ORG_CODE,
                COMP_CODE = entity.COMP_CODE,
                REQUESTER = entity.REQUESTER
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void UpdateHeader(FixedContractHeaderDto entity)
        {
            string sqlExecute = @"UPDATE TB_T_FIXED_CONTRACT_H
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
                DOC_STATUS = entity.DOC_STATUS,
                COMMENT = entity.COMMENT,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
                ORG_CODE = entity.ORG_CODE,
                COMP_CODE = entity.COMP_CODE,
                REQUESTER = entity.REQUESTER
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
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
            var query = Connection.Query<FixedContractDetailDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                ).FirstOrDefault();

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
AND DOC_FCH_ID = @DOC_FCH_ID
;";

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
        public void InsertDetail(FixedContractDetailDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_T_FIXED_CONTRACT_D
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
);";

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
                CONDITION_TYPE = entity.CONDITION_TYPE,
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
                DOC_STATUS = entity.DOC_STATUS,
                START_DATE = entity.START_DATE,
                END_DATE = entity.END_DATE,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }
        public void UpdateDetail(FixedContractDetailDto entity)
        {
            string sqlExecute = @"UPDATE TB_T_FIXED_CONTRACT_D
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
, COMMENT        = @COMMENT
, DOC_STATUS     = @DOC_STATUS
, FLAG_ROW       = @FLAG_ROW
, START_DATE     = @START_DATE
, END_DATE       = @END_DATE
, UPDATED_BY     = @UPDATED_BY
, UPDATED_DATE   = @UPDATED_DATE
WHERE 
DOC_CODE       = @DOC_CODE
AND DOC_FCH_ID = @DOC_FCH_ID
AND DOC_FCD_ID = @DOC_FCD_ID
;";

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
                CONDITION_TYPE = entity.CONDITION_TYPE,
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
                DOC_STATUS = entity.DOC_STATUS,
                START_DATE = entity.START_DATE,
                END_DATE = entity.END_DATE,
                FLAG_ROW = entity.FLAG_ROW,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2(),
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }
}
