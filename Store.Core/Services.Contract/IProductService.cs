using Store.Core.Dtos;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetAllProductsAsync(); 

        Task<IEnumerable<TypesAndBrandsDto>> GetAllTypesAsync();

        Task<IEnumerable<TypesAndBrandsDto>> GetAllBrandsAsync();

        Task<ProductDto> GetProductById(int id);


    }
}

