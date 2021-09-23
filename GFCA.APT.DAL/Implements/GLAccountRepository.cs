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

        public GLAccountDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM [TB_M_GL_ACCOUNT] ";
            sqlQuery += "WHERE ACC_ID = @ACC_ID;";

            var query = Connection.Query<GLAccountDto>(
                sql: sqlQuery,
                param: new { ACC_ID = id }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public GLAccountDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM [TB_M_GL_ACCOUNT] ";
            sqlQuery += "WHERE ACC_CODE = @ACC_CODE;";


            var query = Connection.Query<GLAccountDto>(
                sql: sqlQuery,
                param: new { ACC_CODE = code }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<GLAccountDto> All()
        {
            string sqlQuery = @"SELECT * FROM [TB_M_GL_ACCOUNT] "; 

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
           ([IO_ID]
           ,[CENTER_ID]
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
            @IO_ID
           ,@CENTER_ID
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

                IO_ID = entity.IO_ID,
                CENTER_ID = entity.CENTER_ID,
                FUND_ID = entity.FUND_ID,
                FUND_CENTER_ID = entity.FUND_CENTER_ID,
                ACC_CODE = entity.ACC_CODE,
                ACC_NAME = entity.ACC_NAME,
                ACC_TYPE = entity.ACC_TYPE,
                ACC_TYPE_DESC = entity.ACC_TYPE_DESC,
                ACC_GROUP1 = entity.ACC_GROUP1,
                ACC_GROUP1_DESC = entity.ACC_GROUP1_DESC,
                ACC_GROUP2 = entity.ACC_GROUP2,
                ACC_GROUP2_DESC = entity.ACC_GROUP2_DESC,
                ACC_REMARK = entity.ACC_REMARK,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),

            };

            entity.ACC_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Update(GLAccountDto entity)
        {
            string sqlExecute =
@"UPDATE [dbo].[TB_M_GL_ACCOUNT]
   SET [IO_ID] =			@IO_ID			
      ,[CENTER_ID] =		@CENTER_ID		
      ,[FUND_ID] =			@FUND_ID			
      ,[FUND_CENTER_ID] =	@FUND_CENTER_ID	
      ,[ACC_CODE] =			@ACC_CODE		
      ,[ACC_NAME] =			@ACC_NAME		
      ,[ACC_TYPE] =			@ACC_TYPE		
      ,[ACC_TYPE_DESC] =	@ACC_TYPE_DESC	
      ,[ACC_GROUP1] =		@ACC_GROUP1		
      ,[ACC_GROUP1_DESC] =	@ACC_GROUP1_DESC	
      ,[ACC_GROUP2] =		@ACC_GROUP2		
      ,[ACC_GROUP2_DESC] =	@ACC_GROUP2_DESC	
      ,[ACC_REMARK] =		@ACC_REMARK		
      ,[FLAG_ROW] =			@FLAG_ROW		
      ,[UPDATED_BY] =		@UPDATED_BY		
      ,[UPDATED_DATE] =		@UPDATED_DATE
WHERE ACC_ID = @ACC_ID; ";

            var parms = new
            {
                ACC_ID = entity.ACC_ID,
                IO_ID = entity.IO_ID,
                CENTER_ID = entity.CENTER_ID,
                FUND_ID = entity.FUND_ID,
                FUND_CENTER_ID = entity.FUND_CENTER_ID,
                ACC_CODE = entity.ACC_CODE,
                ACC_NAME = entity.ACC_NAME,
                ACC_TYPE = entity.ACC_TYPE,
                ACC_TYPE_DESC = entity.ACC_TYPE_DESC,
                ACC_GROUP1 = entity.ACC_GROUP1,
                ACC_GROUP1_DESC = entity.ACC_GROUP1_DESC,
                ACC_GROUP2 = entity.ACC_GROUP2,
                ACC_GROUP2_DESC = entity.ACC_GROUP2_DESC,
                ACC_REMARK = entity.ACC_REMARK,
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

        public void Delete(int id)
        {

            string sqlExecute =
@"DELETE [TB_M_GL_ACCOUNT]
WHERE ACC_ID = @ACC_ID; ";
            var parms = new
            {
                ACC_ID = id, 
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
