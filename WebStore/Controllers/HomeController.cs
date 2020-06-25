using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Contracts;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _productData;

        public HomeController( IProductData productData )
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            var products = _productData.GetProducts().Take( 6 );
            var model = products.Select( p => 
                new ProductViewModel( p ) );
            return View( model );
        }
        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult CheckOut() => View();
        public IActionResult ContactUs() => View();
    }
}
