using AutoMapper;
using Store.Core;
using Store.Core.Dtos;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;
using Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId)
        {
            var spec = new ProductSpec(sort,brandId, typeId);
            // convert to ProductDto
            return _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationsAsync(spec));
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpec(id);
            var product = await _unitOfWork.Repository<Product, int>().GetByIdWithSpecificationsAsync(spec);
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return mappedProduct;
        } 

        public async Task<IEnumerable<TypesAndBrandsDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypesAndBrandsDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }


        public async Task<IEnumerable<TypesAndBrandsDto>> GetAllTypesAsync()
        {
            return _mapper.Map<IEnumerable<TypesAndBrandsDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());
        }

       



        // With Specifications :


    }
}
