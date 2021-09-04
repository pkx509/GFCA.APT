using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class OrganizationRepository : RepositoryBase, IOrganizationRepository
    {
        
        public OrganizationRepository(IDbTransaction transaction): base(transaction) { }

        public OrganizationDto GetById(int id)
        {
            string sqlQuery = @"SELECT a.*
                            , (SELECT TOP 1 b.COMP_CODE FROM TB_M_COMPANY b WHERE b.COMP_ID = a.COMP_ID) COMP_CODE
                            FROM TB_M_ORGANIZATION a
                            WHERE ORG_ID = @ORG_ID;";
            var query = Connection.Query<OrganizationDto>(
                sql: sqlQuery,
                param: new { ORG_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public OrganizationDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT a.*
                            , (SELECT TOP 1 b.COMP_CODE FROM TB_M_COMPANY b WHERE b.COMP_ID = a.COMP_ID) COMP_CODE
                            FROM TB_M_ORGANIZATION a
                            WHERE a.ORG_CODE = @ORG_CODE;";
            var query = Connection.Query<OrganizationDto>(
                sql: sqlQuery,
                param: new { ORG_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<OrganizationDto> All()
        {
            string sqlQuery = @"SELECT a.*
                            , (SELECT TOP 1 b.COMP_CODE FROM TB_M_COMPANY b WHERE b.COMP_ID = a.COMP_ID) COMP_CODE
                            FROM TB_M_ORGANIZATION a;";
            var query = Connection.Query<OrganizationDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(OrganizationDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_ORGANIZATION
                                (
                                  COMP_ID
                                , ORG_CODE
                                , ORG_TYPE
                                , ORG_DEPARTMENT_NAME
                                , ORG_POSITION_NAME
                                , ORG_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @COMP_ID
                                , @ORG_CODE
                                , @ORG_TYPE
                                , @ORG_DEPARTMENT_NAME
                                , @ORG_POSITION_NAME
                                , @ORG_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                COMP_ID = entity.COMP_ID,
                ORG_CODE = entity.ORG_CODE,
                ORG_TYPE = entity.ORG_TYPE,
                ORG_DEPARTMENT_NAME = entity.ORG_DEPARTMENT_NAME,
                ORG_POSITION_NAME = entity.ORG_POSITION_NAME,
                ORG_DESC = entity.ORG_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.ORG_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(OrganizationDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_ORGANIZATION
                                SET
                                  COMP_ID               = @COMP_ID
                                , ORG_CODE              = @ORG_CODE
                                , ORG_TYPE              = @ORG_TYPE
                                , ORG_DEPARTMENT_NAME   = @ORG_DEPARTMENT_NAME
                                , ORG_POSITION_NAME     = @ORG_POSITION_NAME
                                , ORG_DESC              = @ORG_DESC
                                , FLAG_ROW              = @FLAG_ROW
                                , UPDATED_BY            = @UPDATED_BY
                                , UPDATED_DATE          = @UPDATED_DATE
                                WHERE
                                ORG_ID = @ORG_ID;
                                ";

            var parms = new
            {
                ORG_ID = entity.ORG_ID,
                COMP_ID = entity.COMP_ID,
                ORG_CODE = entity.ORG_CODE,
                ORG_TYPE = entity.ORG_TYPE,
                ORG_DEPARTMENT_NAME = entity.ORG_DEPARTMENT_NAME,
                ORG_POSITION_NAME = entity.ORG_POSITION_NAME,
                ORG_DESC = entity.ORG_DESC,
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
            string sqlExecute = @"DELETE TB_M_ORGANIZATION WHERE ORG_ID = @ORG_ID; ";
            var parms = new { ORG_ID = id  };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
