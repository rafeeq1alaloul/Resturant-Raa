using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Interfaces;
using Resturant.Models;
using Resturant.ViewModel;

namespace Resturant.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly YummyDbContext _yummyDbContext;
        private readonly IImageServices _imageServices;

        public MenuRepository(YummyDbContext yummyDbContext, IImageServices imageServices)
        {
            _yummyDbContext = yummyDbContext;
            _imageServices = imageServices;
        }
        
        public List<SelectListItem> GetMeats()
        {
            var meats = _yummyDbContext.meats
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();
            return meats;
        }

        public async Task Add(Menu menu)
        {
            await _yummyDbContext.menus.AddAsync(menu);
            await _yummyDbContext.SaveChangesAsync();
        }

        public void Delete(int Id)
        {
            var menu = GetById(Id);
            _yummyDbContext.menus.Remove(menu);
            _yummyDbContext.SaveChanges();
        }

        public List<Menu> GetAll()
        {
            var menu = _yummyDbContext.menus.Include(x => x.Meat).ToList();
            return menu;
        }

        public Menu GetById(int id)
        {
            return _yummyDbContext.menus.Include(x => x.Meat).FirstOrDefault(x => x.MenuId == id);
        }

        public async Task Update(Menu menu)
        {
            _yummyDbContext.menus.Update(menu);
            await _yummyDbContext.SaveChangesAsync();
        }
    }
}
