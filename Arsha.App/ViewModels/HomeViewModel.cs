using Arsha.Core.Entities;

namespace Arsha.App.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public List<Employee> Employees { get; set; }
        public List<Position> Positions { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }

    }
}
