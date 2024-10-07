using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.Products
{
    public class ProductSpec : BaseSpecification<Product, int>
    {
        
        // this ctor is getting the one product by id
        public ProductSpec(int id) : base(P => P.Id == id)
        {
            applyIncludes();
        }

        // this ctor getting the all product 
        public ProductSpec(string? sort, int? brandId, int? typeId) : base(
            
            // !hasvalue because to sure if any logic is false it will run the other statment >> Opposite Logic 
            P => 
            (!brandId.HasValue || brandId.Value == P.BrandId) 
            &&
            (!typeId.HasValue || typeId.Value == P.TypeId)
            )
        {

            // the sorting can by name and price asec and desc: +
            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsec":
                        OrderByAsec = P => P.Price;
                        break;
                    case "priceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                    default: 
                        OrderByAsec = P => P.Name; 
                        break;
                }
            }
            else
            {
                OrderByAsec = P => P.Name;
            }
            applyIncludes(); 
        }


        private void applyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }

    }
}
