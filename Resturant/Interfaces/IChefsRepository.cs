using Resturant.Models;

namespace Resturant.Interfaces
{
    public interface IChefsRepository
    {
        List<Chefs> GetAllChefs();
        Task Add(Chefs chefs);
        Chefs GetChefById(int id);
        Task Update(Chefs chefs);
        void Delete(int id);
    }
}
