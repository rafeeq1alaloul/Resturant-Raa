using Resturant.Models;

namespace Resturant.Interfaces
{
    public interface IEventsRepository
    {
        List<Events> GetAllEvents();
        Task Add(Events events);
        Events GetById(int id);
        Task Update(Events events);
        void Delete(int id);
    }
}
