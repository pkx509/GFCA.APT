using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class BrandRepository : RepositoryBase, IBrandRepository
    {
        
        public BrandRepository(IDbTransaction transaction): base(transaction) { }

        public BrandDto GetById(int id)
        {
            string sqlQuery = "SELECT * FROM TB_M_BRAND WHERE BRAND_ID = @BRAND_ID;";
            var query = Connection.Query<BrandDto>(
                sql: sqlQuery,
                param: new { BRAND_ID = id },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public BrandDto GetByCode(string code)
        {
            string sqlQuery = "SELECT * FROM TB_M_BRAND WHERE BRAND_CODE = @BRAND_CODE;";
            var query = Connection.Query<BrandDto>(
                sql: sqlQuery,
                param: new { BRAND_CODE = code },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<BrandDto> All()
        {
            string sqlQuery = "SELECT * FROM TB_M_BRAND;";
            var query = Connection.Query<BrandDto>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public void Add(BrandDto entity)
        {
            string sqlExecute =
@"INSERT INTO TB_M_BRAND
(
  BRAND_CODE
, BRAND_NAME
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
) VALUES (
  @BRAND_CODE
, @BRAND_NAME
, @FLAG_ROW
, @CREATED_BY
, @CREATED_DATE
); SELECT SCOPE_IDENTITY()
";

            var parms = new
            {
                //BRAND_ID = 0,
                BRAND_CODE = entity.BRAND_CODE,
                BRAND_NAME = entity.BRAND_NAME,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
                //UPDATED_BY = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
            };

            entity.BRAND_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(BrandDto entity)
        {
            string sqlExecute =
@"UPDATE TB_M_BRAND
SET
BRAND_CODE   = @BRAND_CODE
, BRAND_NAME   = @BRAND_NAME
, FLAG_ROW     = @FLAG_ROW
, UPDATED_BY   = @UPDATED_BY
, UPDATED_DATE = @UPDATED_DATE
WHERE
BRAND_ID = @BRAND_ID;
";

            var parms = new
            {
                BRAND_ID = entity.BRAND_ID,
                BRAND_CODE = entity.BRAND_CODE,
                BRAND_NAME = entity.BRAND_NAME,
                FLAG_ROW = entity.FLAG_ROW,
                //CREATED_BY = entity.CREATED_BY,
                //CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
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
@"DELETE TB_M_BRAND
WHERE
BRAND_ID = @BRAND_ID;
";
            var parms = new
            {
                BRAND_ID = id,
                //BRAND_CODE = entity.BRAND_CODE,
                //BRAND_NAME = entity.BRAND_NAME,
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

    }

    /*
    public static class BrandRepositoryExtensions
    {
        public static TB_M_BRAND ToBrandEntity(this BrandDto self)
        {
            TB_M_BRAND entity = new TB_M_BRAND();
            entity.BRAND_ID = self.BRAND_ID??0;
            entity.BRAND_CODE = self.BRAND_CODE;
            entity.BRAND_NAME = self.BRAND_NAME;
            entity.FLAG_ROW = self.FLAG_ROW;
            entity.CREATED_BY = self.CREATED_BY;
            entity.CREATED_DATE = self.CREATED_DATE?? DateTime.UtcNow;
            entity.UPDATED_BY = self.UPDATED_BY;
            entity.UPDATED_DATE = self.UPDATED_DATE;
            return entity;
        }
        public static BrandDto ToBrandDto(this TB_M_BRAND self)
        {
            BrandDto dto = new BrandDto();
            dto.BRAND_ID = self.BRAND_ID;
            dto.BRAND_CODE = self.BRAND_CODE;
            dto.BRAND_NAME = self.BRAND_NAME;
            dto.FLAG_ROW = self.FLAG_ROW;
            dto.CREATED_BY = self.CREATED_BY;
            dto.CREATED_DATE = self.CREATED_DATE;
            dto.UPDATED_BY = self.UPDATED_BY;
            dto.UPDATED_DATE = self.UPDATED_DATE;
            return dto;
        }
    }
    */

}
