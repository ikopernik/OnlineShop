using Microsoft.Extensions.Logging;
using OnlineShop.Base.Entities;
using OnlineShop.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DAL.Interfaces
{
    public interface IDataRepository
    {
        public Task<IEnumerable<ProductDTO>> GetProducts();
        public Task<ProductDTO> GetProduct(long id);
        public Task<ProductDTO> AddProduct(ProductDTO productDTO);
        public Task<ProductDTO> UpdateProduct(ProductDTO productDTO);
        public Task<bool> DeleteProduct(long id);
    }
}
