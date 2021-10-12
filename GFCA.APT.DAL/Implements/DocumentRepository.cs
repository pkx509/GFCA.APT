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

        
        public DocumentDto GenerateDocNo(string docTypeCode, string docCode)
        {
            throw new NotImplementedException();
        }

        public DocumentDto GenerateDocNo(string docTypeCode, string docYear, string docMonth)
        {
            //string sqlQuery = @"EXECUTE SP_GENERATE_DOC_CODE @DOC_TYPE_CODE, @DOC_YEAR, @DOC_MONTH";
            string sqlQuery = @"SP_GENERATE_DOC_CODE";
            var query = Connection.Query<DocumentDto>(
                sql: sqlQuery,
                param: new
                {
                    DOC_TYPE_CODE = docTypeCode,
                    DOC_YEAR = docYear,
                    DOC_MONTH = docMonth,
                },
                commandType: CommandType.StoredProcedure,
                transaction: Transaction
                ).FirstOrDefault()
                ;

            return query;
        }

        public IEnumerable<DocumentDto> All()
        {
            throw new NotImplementedException();
        }

        public DocumentDto GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public void Insert(DocumentDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DocumentDto entity)
        {
            throw new NotImplementedException();
        }
        
        public void Delete(string code)
        {
            throw new NotImplementedException();
        }

    }
}
