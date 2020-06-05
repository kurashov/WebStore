using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke() => View( GetSections() );

        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = _productData.GetSections().ToList();

            var parentSections = sections.
                Where( s => s.ParentId is null ).
                Select( s => new SectionViewModel(s, null) ).
                ToList();

            foreach( var parentSection in parentSections )
            {
                parentSection.ChildSections.AddRange(
                    sections.Where( s => s.ParentId == parentSection.Id ).
                        Select( s => new SectionViewModel( s, parentSection ) )
                    );
                parentSection.ChildSections.Sort( (x, y) => x.Order.CompareTo(y.Order) );
            }

            parentSections.Sort((x, y) => x.Order.CompareTo(y.Order));

            return parentSections;
        }
    }
}
