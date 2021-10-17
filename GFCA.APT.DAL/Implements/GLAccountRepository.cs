using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.DAL.Implements
{
   public  class GLAccountRepository : RepositoryBase, IGLAccountRepository
    {

        public GLAccountRepository(IDbTransaction transaction) : base(transaction) { }

        public GLAccountDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT      (SELECT TOP 1 (G.CENTER_CODE + '_' + C.CENTER_NAME) FROM TB_M_COST_CENTER AS C WHERE C.CENTER_CODE= G.CENTER_CODE) as CENTER_CODE_NAME 
		  , (SELECT TOP 1 (G.GRP_CODE + '_' + GL.GRP_NAME) FROM TB_M_GL_GROUP AS GL WHERE GL.GRP_CODE= G.GRP_CODE) as GRP_CODE_NAME 
		  ,G.* 
         FROM TB_M_GL_ACCOUNT AS G WHERE G.ACC_CODE = @ACC_CODE;";
            


            var query = Connection.Query<GLAccountDto>(
                sql: sqlQuery,
                param: new { ACC_CODE = code }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<GLAccountDto> All()
        {
            string sqlQuery = @"SELECT      (SELECT TOP 1 (G.CENTER_CODE + '_' + C.CENTER_NAME) FROM TB_M_COST_CENTER AS C WHERE C.CENTER_CODE= G.CENTER_CODE) as CENTER_CODE_NAME 
		  , (SELECT TOP 1 (G.GRP_CODE + '_' + GL.GRP_NAME) FROM TB_M_GL_GROUP AS GL WHERE GL.GRP_CODE= G.GRP_CODE) as GRP_CODE_NAME 
		  ,G.* 
         FROM TB_M_GL_ACCOUNT AS G"; 

            var query = Connection.Query<GLAccountDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(GLAccountDto entity)
        {
            string sqlExecute =
@"INSERT INTO [dbo].[TB_M_GL_ACCOUNT]
           ([IO_CODE]
           ,[CENTER_CODE]
           ,[FUND_ID]
           ,[FUND_CENTER_ID]
           ,[ACC_CODE]
           ,[ACC_NAME]
           ,[ACC_TYPE]
           ,[ACC_TYPE_DESC]
           ,[ACC_GROUP1]
           ,[ACC_GROUP1_DESC]
           ,[ACC_GROUP2]
           ,[ACC_GROUP2_DESC]
           ,[ACC_REMARK]
           ,[FLAG_ROW]
           ,[CREATED_BY]
           ,[CREATED_DATE] )
     VALUES (
            @IO_CODE
           ,@CENTER_CODE
           ,@FUND_ID
           ,@FUND_CENTER_ID
           ,@ACC_CODE
           ,@ACC_NAME
           ,@ACC_TYPE
           ,@ACC_TYPE_DESC
           ,@ACC_GROUP1
           ,@ACC_GROUP1_DESC
           ,@ACC_GROUP2
           ,@ACC_GROUP2_DESC
           ,@ACC_REMARK
           ,@FLAG_ROW
           ,@CREATED_BY
           ,@CREATED_DATE
); SELECT SCOPE_IDENTITY()
";

            var parms = new
            {

                IO_CODE         = entity.IO_CODE,
                CENTER_CODE     = entity.CENTER_CODE,
                FUND_ID         = entity.FUND_ID,
                FUND_CENTER_ID  = entity.FUND_CENTER_ID,
                ACC_CODE        = entity.ACC_CODE,
                ACC_NAME        = entity.ACC_NAME,
                ACC_TYPE        = entity.ACC_TYPE,
                ACC_TYPE_DESC   = entity.ACC_TYPE_DESC,
                ACC_GROUP1      = entity.ACC_GROUP1,
                ACC_GROUP1_DESC = entity.ACC_GROUP1_DESC,
                ACC_GROUP2      = entity.ACC_GROUP2,
                ACC_GROUP2_DESC = entity.ACC_GROUP2_DESC,
                ACC_REMARK      = entity.ACC_REMARK,
                FLAG_ROW        = entity.FLAG_ROW,
                CREATED_BY      = entity.CREATED_BY,
                CREATED_DATE    = entity.CREATED_DATE?.ToDateTime2(),

            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Update(GLAccountDto entity)
        {
            string sqlExecute =
@"UPDATE [dbo].[TB_M_GL_ACCOUNT]
SET 
       [IO_CODE]         = @IO_CODE			
      ,[CENTER_CODE]     = @CENTER_CODE		
      ,[FUND_ID]         = @FUND_ID			
      ,[FUND_CENTER_ID]  = @FUND_CENTER_ID
      ,[ACC_NAME]        = @ACC_NAME		
      ,[ACC_TYPE]        = @ACC_TYPE		
      ,[ACC_TYPE_DESC]   = @ACC_TYPE_DESC	
      ,[ACC_GROUP1]      = @ACC_GROUP1		
      ,[ACC_GROUP1_DESC] = @ACC_GROUP1_DESC	
      ,[ACC_GROUP2]      = @ACC_GROUP2		
      ,[ACC_GROUP2_DESC] = @ACC_GROUP2_DESC	
      ,[ACC_REMARK]      = @ACC_REMARK		
      ,[FLAG_ROW]        = @FLAG_ROW		
      ,[UPDATED_BY]      = @UPDATED_BY		
      ,[UPDATED_DATE]    = @UPDATED_DATE
WHERE ACC_CODE = @ACC_CODE; ";

            var parms = new
            {
                IO_CODE         = entity.IO_CODE,
                CENTER_CODE     = entity.CENTER_CODE,
                FUND_ID         = entity.FUND_ID,
                FUND_CENTER_ID  = entity.FUND_CENTER_ID,
                ACC_CODE        = entity.ACC_CODE,
                ACC_NAME        = entity.ACC_NAME,
                ACC_TYPE        = entity.ACC_TYPE,
                ACC_TYPE_DESC   = entity.ACC_TYPE_DESC,
                ACC_GROUP1      = entity.ACC_GROUP1,
                ACC_GROUP1_DESC = entity.ACC_GROUP1_DESC,
                ACC_GROUP2      = entity.ACC_GROUP2,
                ACC_GROUP2_DESC = entity.ACC_GROUP2_DESC,
                ACC_REMARK      = entity.ACC_REMARK,
                FLAG_ROW        = entity.FLAG_ROW,
                UPDATED_BY      = entity.UPDATED_BY,
                UPDATED_DATE    = entity.UPDATED_DATE?.ToDateTime2()
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Delete(string code)
        {

            string sqlExecute = @"DELETE TB_M_GL_ACCOUNT WHERE ACC_CODE = @ACC_CODE; ";
            var parms = new
            {
                ACC_CODE = code, 
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
