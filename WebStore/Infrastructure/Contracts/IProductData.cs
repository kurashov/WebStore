﻿using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Contracts
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter = null);

        Product GetProductById( int id );
    }
}