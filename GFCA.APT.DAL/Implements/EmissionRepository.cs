using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class EmissionRepository : RepositoryBase, IEmissionRepository
    {

        public EmissionRepository(IDbTransaction transaction) : base(transaction) { }

        public EmissionDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_EMISSION WHERE EMIS_ID = @EMIS_ID;";
            var query = Connection.Query<EmissionDto>(
                sql: sqlQuery,
                param: new { EMIS_ID = id }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public EmissionDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_EMISSION WHERE EMIS_CODE = @EMIS_CODE;";
            var query = Connection.Query<EmissionDto>(
                sql: sqlQuery,
                param: new { EMIS_CODE = code }
                , transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<EmissionDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_EMISSION";
            var query = Connection.Query<EmissionDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(EmissionDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_EMISSION
                                (
                                  EMIS_CODE
                                , EMIS_NAME
                                , EMIS_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @EMIS_CODE
                                , @EMIS_NAME
                                , @EMIS_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                EMIS_CODE = entity.EMIS_CODE,
                EMIS_NAME = entity.EMIS_NAME,
                EMIS_DESC = entity.EMIS_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.EMIS_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(EmissionDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_EMISSION
                                SET
                                  EMIS_CODE   = @EMIS_CODE
                                , EMIS_NAME   = @EMIS_NAME
                                , EMIS_DESC   = @EMIS_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                EMIS_ID = @EMIS_ID;
                                ";

            var parms = new
            {
                EMIS_ID = entity.EMIS_ID,
                EMIS_CODE = entity.EMIS_CODE,
                EMIS_NAME = entity.EMIS_NAME,
                EMIS_DESC = entity.EMIS_DESC,
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
            string sqlExecute = @"DELETE TB_M_EMISSION WHERE EMIS_ID = @EMIS_ID;";
            var parms = new { EMIS_ID = id };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
