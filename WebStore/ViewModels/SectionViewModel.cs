using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class SectionViewModel
    {
        public SectionViewModel( Section section, SectionViewModel parentSection )
        {
            Id = section.Id;
            Name = section.Name;
            Order = section.Order;
            ParentSection = parentSection;

            ChildSections = new List<SectionViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<SectionViewModel> ChildSections { get; }
        public SectionViewModel ParentSection { get; }
    }
}
