using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public Section Section { get; init; }

        public Brand Brand { get; init; }

        public string Product { get; init; }
    }
}
