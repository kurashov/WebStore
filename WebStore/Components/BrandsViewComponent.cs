using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Contracts;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent( IProductData productData )
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke() => View( GetBrands() );

        private  IEnumerable<BrandViewModel> GetBrands()
        {
            return _productData.GetBrands().
                Select( b => new BrandViewModel( b ) ).
                OrderBy( b => b.Order );
        }
    }
}