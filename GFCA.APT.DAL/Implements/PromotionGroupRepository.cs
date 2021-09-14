using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class PromotionGroupRepository : RepositoryBase, IPromotionGroupRepository
    {

        public PromotionGroupRepository(IDbTransaction transaction) : base(transaction) { }



        public IEnumerable<PromotionGroupDto> All()
        {
            string sqlQuery = @"SELECT A.*,A.[CHANNEL_ID] ,(SELECT TOP 1 CHANNEL_CODE + '-' + CHANNEL_NAME  FROM [TB_M_CHANNEL] where [CHANNEL_ID] = A.[CHANNEL_ID]) as [CHANNEL_CODE] 
      ,A.[CUST_ID] ,(SELECT TOP 1 CUST_CODE + '-' + CUST_NAME  from [dbo].[TB_M_CUSTOMER] where [CUST_ID] = A.[CUST_ID]) as CUST_CODE 
      ,A.[CLIENT_ID] ,(SELECT TOP 1 CLIENT_CODE + '-' + CLIENT_NAME  from [dbo].[TB_M_CLIENT] where [CLIENT_ID] = A.[CLIENT_ID]) as CLIENT_CODE 
      ,A.[UPDATED_DATE]
  FROM [dbo].[TB_M_PROMOTION_GROUP]  AS A;";
            var query = Connection.Query<PromotionGroupDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public void Delete(int id)
        {
            string sqlExecute = @"DELETE TB_M_PROMOTION_GROUP
                                WHERE
                                PROGP_ID = @PROGP_ID;
                                ";
            var parms = new
            {
                PROGP_ID = id,
                //BRAND_CODE = entity.BRAND_CODE,
                //BRAND_NAME = entity.BRAND_NAME,
                //BRAND_DESC = entity.BRAND_DESC,
                //FLAG_ROW = entity.FLAG_ROW,
                //CREATED_BY = entity.CREATED_BY,
                //CREATED_DATE = entity.CREATED_DATE,
                //UPDATED_BY = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
                //UPDATED_DATE = DateTime.UtcNow
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }

       

        public PromotionGroupDto GetById(int id)
        {
                     
            string sqlQuery = @"SELECT A.*,A.[CHANNEL_ID] ,(SELECT TOP 1 CHANNEL_CODE + '-' + CHANNEL_NAME  FROM [TB_M_CHANNEL] where [CHANNEL_ID] = A.[CHANNEL_ID]) as [CHANNEL_CODE] 
      ,A.[CUST_ID] ,(SELECT TOP 1 CUST_CODE + '-' + CUST_NAME  from [dbo].[TB_M_CUSTOMER] where [CUST_ID] = A.[CUST_ID]) as CUST_CODE 
      ,A.[CLIENT_ID] ,(SELECT TOP 1 CLIENT_CODE + '-' + CLIENT_NAME  from [dbo].[TB_M_CLIENT] where [CLIENT_ID] = A.[CLIENT_ID]) as CLIENT_CODE 
      ,A.[UPDATED_DATE]
  FROM [dbo].[TB_M_PROMOTION_GROUP]  AS A WHERE  A.PROGP_ID = @PROGP_ID;";
            var query = Connection.Query<PromotionGroupDto>(
                sql: sqlQuery,
                param: new { PROGP_ID = id }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public PromotionGroupDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT A.*,A.[CHANNEL_ID] ,(SELECT TOP 1 CHANNEL_CODE + '-' + CHANNEL_NAME  FROM [TB_M_CHANNEL] where [CHANNEL_ID] = A.[CHANNEL_ID]) as [CHANNEL_CODE] 
      ,A.[CUST_ID] ,(SELECT TOP 1 CUST_CODE + '-' + CUST_NAME  from [dbo].[TB_M_CUSTOMER] where [CUST_ID] = A.[CUST_ID]) as CUST_CODE 
      ,A.[CLIENT_ID] ,(SELECT TOP 1 CLIENT_CODE + '-' + CLIENT_NAME  from [dbo].[TB_M_CLIENT] where [CLIENT_ID] = A.[CLIENT_ID]) as CLIENT_CODE 
      ,A.[UPDATED_DATE]
  FROM [dbo].[TB_M_PROMOTION_GROUP]  AS A WHERER  PROGP_CODE = @PROGP_CODE;";
            var query = Connection.Query<PromotionGroupDto>(
                sql: sqlQuery,
                param: new { BRAND_CODE = code }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }


        public void Insert(PromotionGroupDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_PROMOTION_GROUP(CHANNEL_ID,CUST_ID,CLIENT_ID,PROGP_CODE,PROGP_NAME,PROGP_DESC,FLAG_ROW,CREATED_BY,CREATED_DATE) VALUES (
@CHANNEL_ID,@CUST_ID,@CLIENT_ID,@PROGP_CODE,@PROGP_NAME,@PROGP_DESC,@FLAG_ROW,@CREATED_BY,@CREATED_DATE); SELECT SCOPE_IDENTITY()";

            var parms = new
            {


         
                CHANNEL_ID = entity.CHANNEL_ID,
                CUST_ID= entity.CUST_ID,
                CLIENT_ID = entity.CLIENT_ID,
                PROGP_CODE = entity.PROGP_CODE,
                PROGP_NAME = entity.PROGP_NAME,
                PROGP_DESC = entity.PROGP_DESC,            
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2()

            };

            entity.PROGP_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }


        public void Update(PromotionGroupDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_PROMOTION_GROUP
                                SET
                                  CHANNEL_ID   = @CHANNEL_ID
                                , CUST_ID    = @CUST_ID
                                , CLIENT_ID   = @CLIENT_ID
                                , PROGP_CODE   = @PROGP_CODE
                                , PROGP_NAME   = @PROGP_NAME
                                , PROGP_DESC   = @PROGP_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                PROGP_ID = @PROGP_ID;
                                ";


            var parms = new
            {


                PROGP_ID = entity.PROGP_ID,
                CHANNEL_ID = entity.CHANNEL_ID,
                CUST_ID = entity.CUST_ID,
                CLIENT_ID = entity.CLIENT_ID,
                PROGP_CODE = entity.PROGP_CODE,
                PROGP_NAME = entity.PROGP_NAME,
                PROGP_DESC = entity.PROGP_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2()

            };


 

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }
    }

}
