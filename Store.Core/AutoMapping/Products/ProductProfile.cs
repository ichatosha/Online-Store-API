using AutoMapper;
using Store.Core.Dtos;
using Store.Core.Dtos.Products;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.AutoMapping.Products
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            // in Product class : BrndName of ProductBrand and in ProductDto class is BrandName fo sting etc..
            CreateMap<Product , ProductDto>()
                .ForMember(d => d.BrandName , options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName , options => options.MapFrom(s => s.Type.Name));

            CreateMap<ProductBrand , TypesAndBrandsDto>();

            CreateMap<ProductType , TypesAndBrandsDto>();
        }
    }
}
