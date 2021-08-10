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
    public class ProductRepository : IRepository<ProductDto>
    {
        private Repository<TB_M_PRODUCT> _repository;
        private MapperConfiguration _mapConfig;
        private readonly IMapper mapper;
        private SqlConnection _conn;
        private SqlTransaction _tranx;

        public IQueryable<ProductDto> Table => throw new NotImplementedException();

        public IQueryable<ProductDto> TableNoTracking => throw new NotImplementedException();

        //private SqlConnection _conn;
        public ProductRepository(APTDbContext context)
        {
            _repository = new Repository<TB_M_PRODUCT>(context);
            /*
            _mapConfig = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<TB_M_BRAND, BrandDto>();
                cfg.CreateMap<BrandDto, TB_M_BRAND>();
            });
            */
            _mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TB_M_PRODUCT, ProductDto>());
            mapper = _mapConfig.CreateMapper();
            //_conn = context.Database.Connection;
            string conStr = ConfigurationManager.ConnectionStrings["APTDbConnectionString"].ToString();
            _conn = new SqlConnection(conStr);
            //SqlCommand cmm = new SqlCommand("sql", _conn, _tranx);
        }


        public IEnumerable<ProductDto> GetAll()
        {
            /*
            var dto = _repository.Get()
                .Where(s => s.FLAG_ROW.Equals("S"))
                .ProjectTo<BrandDto>(_mapConfig)
                .ToList();
            */

            string sql = "SELECT * FROM TB_M_PRODUCT;";
            IList<ProductDto> result = new List<ProductDto>();
            try
            {
                _conn.Open();
                result = _conn.Query<ProductDto>(sql).AsList();
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public ProductDto GetById(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public void Insert(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<ProductDto> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<ProductDto> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductDto> Where(Expression<Func<ProductDto, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
