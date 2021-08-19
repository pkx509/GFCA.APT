using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GFCA.APT.DAL.Implements
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction) { }

        public ProductDto GetById(int id)
        {
            string sqlQuery = "SELECT * FROM TB_M_PRODUCT WHERE BRAND_ID = @BRAND_ID;";
            var query = Connection.Query<ProductDto>(
                sql: sqlQuery,
                param: new { PROD_ID = id },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public ProductDto GetByCode(string code)
        {
            string sqlQuery = "SELECT * FROM TB_M_PRODUCT WHERE BRAND_CODE = @BRAND_CODE;";
            var query = Connection.Query<ProductDto>(
                sql: sqlQuery,
                param: new { PROD_CODE = code },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<ProductDto> All()
        {
            string sqlQuery = "SELECT * FROM TB_M_PRODUCT;";
            var query = Connection.Query<ProductDto>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(ProductDto entity)
        {
            string sqlExecute =
@"INSERT INTO TB_M_PRODUCT
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
                //BRAND_ID     = 0,
                PROD_CODE      = entity.PROD_CODE,
                PROD_NAME      = entity.PROD_NAME,
                FLAG_ROW       = entity.FLAG_ROW,
                CREATED_BY     = entity.CREATED_BY,
                CREATED_DATE   = entity.CREATED_DATE?.ToDateTime2(),
                //UPDATED_BY   = entity.UPDATED_BY,
                //UPDATED_DATE = entity.UPDATED_DATE
            };

            entity.PROD_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(ProductDto entity)
        {
            string sqlExecute =
@"UPDATE TB_M_PRODUCT
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
                PROD_ID        = entity.PROD_ID,
                PROD_CODE      = entity.PROD_CODE,
                PROD_NAME      = entity.PROD_NAME,
                FLAG_ROW       = entity.FLAG_ROW,
                //CREATED_BY   = entity.CREATED_BY,
                //CREATED_DATE = entity.CREATED_DATE,
                UPDATED_BY     = entity.UPDATED_BY,
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
@"DELETE TB_M_PRODUCT
WHERE
BRAND_ID = @BRAND_ID;
";
            var parms = new
            {
                PROD_ID = id,
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
}
