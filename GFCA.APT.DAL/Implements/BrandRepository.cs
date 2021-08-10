using AutoMapper;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq.Expressions;

namespace GFCA.APT.DAL.Implements
{
    public class BrandRepository : IRepository<BrandDto>
    {
        private Repository<TB_M_BRAND> _repository;
        private MapperConfiguration _mapConfig;
        private readonly IMapper mapper;
        private SqlConnection _conn;
        private SqlTransaction _tranx;

        public IQueryable<BrandDto> Table => throw new NotImplementedException();

        public IQueryable<BrandDto> TableNoTracking => throw new NotImplementedException();

        //private SqlConnection _conn;
        public BrandRepository(APTDbContext context)
        {
            _repository = new Repository<TB_M_BRAND>(context);
            /*
            _mapConfig = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<TB_M_BRAND, BrandDto>();
                cfg.CreateMap<BrandDto, TB_M_BRAND>();
            });
            */
            _mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TB_M_BRAND, BrandDto>());
            mapper = _mapConfig.CreateMapper();
            //_conn = context.Database.Connection;
            string conStr = ConfigurationManager.ConnectionStrings["APTDbConnectionString"].ToString();
            _conn = new SqlConnection(conStr);
            //SqlCommand cmm = new SqlCommand("sql", _conn, _tranx);
        }


        public IEnumerable<BrandDto> GetAll()
        {
            /*
            var dto = _repository.Get()
                .Where(s => s.FLAG_ROW.Equals("S"))
                .ProjectTo<BrandDto>(_mapConfig)
                .ToList();
            */

            string sql = "SELECT * FROM TB_M_BRAND;";
            IList<BrandDto> result = new List<BrandDto>();
            try
            {
                _conn.Open();
                result = _conn.Query<BrandDto>(sql).AsList();
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public BrandDto GetById(int primaryKey)
        {
            string sql = "SELECT * FROM TB_M_BRAND WHERE BRAND_ID = @BRAND_ID;";
            var dto = new BrandDto();
            try
            {
                _conn.Open();
                dto = _conn.QueryFirstOrDefault<BrandDto>(sql, new { BRAND_ID = primaryKey });
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dto;

        }

        public void Insert(BrandDto data)
        {

            string sql =
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
);
";
            //var entity = mapper.Map<TB_M_BRAND>(data);
            var entity = data.ToBrandEntity();
            _repository.Insert(entity);

            var parameters = new
            {
                //BRAND_ID = 0,
                BRAND_CODE = data.BRAND_CODE,
                BRAND_NAME = data.BRAND_NAME,
                FLAG_ROW = data.FLAG_ROW,
                CREATED_BY = data.CREATED_BY,
                CREATED_DATE = data.CREATED_DATE,
                //UPDATED_BY = data.UPDATED_BY,
                //UPDATED_DATE = data.UPDATED_DATE
            };
            try
            {
                _conn.Open();
                var effected = _conn.Execute(sql, parameters, commandType: CommandType.Text);
                _conn.Close();
            }
            catch (DbException ex)
            {
                _conn.Close();
                throw ex;
            }

        }

        public void Update(BrandDto data)
        {
            //var entity = mapper.Map<TB_M_BRAND>(data);
            //var entity = data.ToBrandEntity();
            //_repository.Context.Database.ExecuteSqlCommand
            //_repository.Update(entity);
            try
            {
                string sql = 
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
                //var parms = new DynamicParameters();
                //parms.Add("@effected", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue );
                //int effected = parms.Get<int>("@effected");
                var parms = new
                {
                    BRAND_ID = data.BRAND_ID,
                    BRAND_CODE = data.BRAND_CODE,
                    BRAND_NAME = data.BRAND_NAME,
                    FLAG_ROW = data.FLAG_ROW,
                    //CREATED_BY = data.CREATED_BY,
                    //CREATED_DATE = data.CREATED_DATE,
                    UPDATED_BY = data.UPDATED_BY,
                    //UPDATED_DATE = data.UPDATED_DATE
                    UPDATED_DATE = DateTime.UtcNow
                };
                /*
                Z.Dapper.Plus.DapperPlusManager
                    .Entity<TB_M_BRAND>()
                    .Table("TB_M_BRAND")
                    .Identity(x => x.BRAND_ID)
                    .BatchSize(200);
                */
                _conn.Open();
                var result = _conn.Execute(sql, parms, commandType: CommandType.Text);
                _conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(IEnumerable<BrandDto> entities)
        {
            throw new NotImplementedException();
        }
        
        public void Delete(int primaryKey)
        {
            string sql =
@"DELETE TB_M_BRAND
WHERE
BRAND_ID = @BRAND_ID;
";
            var parms = new
            {
                BRAND_ID = primaryKey,
                //BRAND_CODE = data.BRAND_CODE,
                //BRAND_NAME = data.BRAND_NAME,
                //FLAG_ROW = data.FLAG_ROW,
                //CREATED_BY = data.CREATED_BY,
                //CREATED_DATE = data.CREATED_DATE,
                //UPDATED_BY = data.UPDATED_BY,
                //UPDATED_DATE = data.UPDATED_DATE
                //UPDATED_DATE = DateTime.UtcNow
            };
            _conn.Open();
            var result = _conn.Execute(sql, parms, commandType: CommandType.Text);
            _conn.Close();
            //_repository.Delete(primaryKey);
        }
        public void Delete(BrandDto entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(IEnumerable<BrandDto> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BrandDto> Where(Expression<Func<BrandDto, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }

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
}
