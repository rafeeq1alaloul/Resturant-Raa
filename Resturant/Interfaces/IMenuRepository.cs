using Microsoft.AspNetCore.Mvc.Rendering;
using Resturant.Models;
using Resturant.ViewModel;

namespace Resturant.Interfaces
{
    public interface IMenuRepository
    {
        List<SelectListItem> GetMeats();
        List<Menu> GetAll();
        Task Add(Menu menu);
        Task Update(Menu menu);
        Menu GetById(int id);
        void Delete(int Id);
    }
}
