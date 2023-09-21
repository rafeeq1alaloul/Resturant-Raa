using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Interfaces;
using Resturant.Models;
using Resturant.ViewModel;

namespace Resturant.Repositories
{
    public class ChefsRepository : IChefsRepository
    {
        private readonly YummyDbContext _yummyDbContext;

        public ChefsRepository(YummyDbContext yummyDbContext) {
            _yummyDbContext = yummyDbContext;
        }

        public async Task Add(Chefs chefs)
        {
            await _yummyDbContext.AddAsync(chefs);
            await _yummyDbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var chef = GetChefById(id);
            _yummyDbContext.chefs.Remove(chef);
            _yummyDbContext.SaveChanges();
        }

        public List<Chefs> GetAllChefs()
        {
            return _yummyDbContext.chefs.ToList();
        }

        public Chefs GetChefById(int id)
        {
            return _yummyDbContext.chefs.Find(id);
        }

        public async Task Update(Chefs chefs)
        {
            _yummyDbContext.Update(chefs);
            await _yummyDbContext.SaveChangesAsync();
        }
    }
}
