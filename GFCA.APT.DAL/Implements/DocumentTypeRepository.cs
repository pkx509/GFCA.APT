using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class DocumentTypeRepository : RepositoryBase, IDocumentTypeRepository
    {

        public DocumentTypeRepository(IDbTransaction transaction): base(transaction) { }

        public DocumentTypeDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @DOC_TYPE_CODE;";
            var query = Connection.Query<DocumentTypeDto>(
                sql: sqlQuery,
                param: new { DOC_TYPE_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<DocumentTypeDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_DOCUMENT_TYPE";
            var query = Connection.Query<DocumentTypeDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(DocumentTypeDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_DOCUMENT_TYPE
                                (
                                  DOC_TYPE_CODE
                                , DOC_TYPE_NAME
                                , DOC_TYPE_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @DOC_TYPE_CODE
                                , @DOC_TYPE_NAME
                                , @DOC_TYPE_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                DOC_TYPE_CODE = entity.DOC_TYPE_CODE,
                DOC_TYPE_NAME = entity.DOC_TYPE_NAME,
                DOC_TYPE_DESC = entity.DOC_TYPE_DESC,
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
        public void Update(DocumentTypeDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_DOCUMENT_TYPE
                                SET
                                  DOC_TYPE_NAME = @DOC_TYPE_NAME
                                , DOC_TYPE_DESC = @DOC_TYPE_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                DOC_TYPE_CODE = @DOC_TYPE_CODE;
                                ";

            var parms = new
            {
                DOC_TYPE_CODE = entity.DOC_TYPE_CODE,
                DOC_TYPE_NAME = entity.DOC_TYPE_NAME,
                DOC_TYPE_DESC = entity.DOC_TYPE_DESC,
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

        public void Delete(string code)
        {
            string sqlExecute = @"DELETE TB_M_DOCUMENT_TYPE WHERE DOC_TYPE_CODE = @DOC_TYPE_CODE;";
            var parms = new { DOC_TYPE_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
