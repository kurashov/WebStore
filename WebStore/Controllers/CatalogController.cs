using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController( IProductData productData )
        {
            _productData = productData;
        }

        public IActionResult Shop(int? sectionId, int? brandId)
        {
            var products = _productData.
                GetProducts( new ProductFilter( sectionId, brandId ) ).
                Select(p => new ProductViewModel(p)).
                OrderBy(p => p.Order).
                ToList();
            return View(new CatalogViewModel(sectionId, brandId, products));
        }

        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}
