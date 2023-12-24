using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Base.Entities;
using OnlineShop.DAL.Domain;
using OnlineShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Classes
{
    public class DataRepository: IDataRepository
    {
        private StoreDbContext _dbContext;
        private ILogger _log;
        private IMapper _mapper;


        public DataRepository(ILogger<DataRepository> log)
        {
            _log = log;
            
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductDTO>()
                .ReverseMap());
            _mapper = new Mapper(config);

            var appDbContextFactory = new AppDbContextFactory();
            string[] args = {};
            _dbContext = appDbContextFactory.CreateDbContext(args);

            SeedData seedData = new SeedData(_dbContext, _log);
            seedData.SeedDatabase();
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<IList<Product>, IList<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProduct(long id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
                return null;
            else
                return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<ProductDTO, Product>(productDTO);
            var resProduct = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map< Product, ProductDTO> (resProduct.Entity);
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<ProductDTO, Product>(productDTO);
            var resProduct = _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(resProduct.Entity);
        }

        public async Task<bool> DeleteProduct(long id)
        {
            var resProduct =_dbContext.Products.Remove(new Product() { Id = id });
            await _dbContext.SaveChangesAsync();
            if (resProduct != null)
                return true;
            else
                return false;
        }
    }
}
