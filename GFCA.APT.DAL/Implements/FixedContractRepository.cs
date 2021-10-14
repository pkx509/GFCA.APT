using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System;

namespace GFCA.APT.DAL.Implements
{
    public class FixedContractRepository : RepositoryBase, IFixedContractRepository
    {

        public FixedContractRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<FixedContractDto> All()
        {
            string sqlQuery = @"SELECT FCH.DOC_CODE,
                                (SELECT TOP 1 CHANNEL_NAME  from TB_M_CHANNEL where CHANNEL_CODE = FCH.CHANNEL_CODE) as CHANNEL_NAME,
                                FCH.CREATED_BY AS REQUESTER,
                                (SELECT TOP 1 CLIENT_NAME  from TB_M_CLIENT where CLIENT_CODE = FCH.CLIENT_CODE) as CLIENT_NAME,
                                FCH.CREATED_DATE
                                FROM TB_T_FIXED_CONTRACT_H FCH";

            var query = Connection.Query<FixedContractDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;

        }

        public void GetDetailItem(string docCode, int docVersion)
        {
            throw new NotImplementedException();
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
);";

            var parms = new
            {
                DOC_CODE     = entity.DOC_CODE,
                DOC_VER      = entity.DOC_VER,
                DOC_REV      = entity.DOC_REV,
                CLIENT_CODE  = entity.CLIENT_CODE,
                CUST_CODE    = entity.CUST_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                DOC_STATUS   = entity.DOC_STATUS,
                COMMENT      = entity.COMMENT,
                FLAG_ROW     = entity.FLAG_ROW,
                CREATED_BY   = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
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
WHERE DOC_FCH_ID = @DOC_FCH_ID;";

            var parms = new
            {
                DOC_CODE     = entity.DOC_CODE,
                DOC_VER      = entity.DOC_VER,
                DOC_REV      = entity.DOC_REV,
                CLIENT_CODE  = entity.CLIENT_CODE,
                CUST_CODE    = entity.CUST_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                DOC_STATUS   = entity.DOC_STATUS,
                COMMENT      = entity.COMMENT,
                FLAG_ROW     = entity.FLAG_ROW,
                CREATED_BY   = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
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
                //DOC_FCH_ID = entity.DOC_FCH_ID,
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
                DOC_CODE       = entity.DOC_CODE,
                DOC_VER        = entity.DOC_VER,
                DOC_REV        = entity.DOC_REV,
                BRAND_CODE     = entity.BRAND_CODE,
                ACTIVITY_CODE  = entity.ACTIVITY_CODE,
                CENTER_CODE    = entity.CENTER_CODE,
                ACC_CODE       = entity.ACC_CODE,
                SIZE           = entity.SIZE,
                UOM            = entity.UOM,
                PACK           = entity.PACK,
                DATE_REF       = entity.DATE_REF,
                CONDITION_TYPE = entity.CONDITION_TYPE,
                CONTRACT_CATE  = entity.CONTRACT_CATE,
                CONTRACT_DESC  = entity.CONTRACT_DESC,
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

        public IEnumerable<FixedContractDetailDto> GetDetailAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FixedContractHeaderDto> GetHeaderAll()
        {
            throw new NotImplementedException();
        }

        public FixedContractDto GetDetailItem(string docCode, int docVer = -1, int docRev = -1)
        {
            throw new NotImplementedException();
        }
    }
}
