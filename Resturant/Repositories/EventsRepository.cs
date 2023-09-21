
using Resturant.Data;
using Resturant.Interfaces;
using Resturant.Models;

namespace Resturant.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly YummyDbContext _yummyDbContext;
        public EventsRepository(YummyDbContext yummyDbContext) {
            _yummyDbContext = yummyDbContext;
        }

        public async Task Add(Events events)
        {
            await _yummyDbContext.AddAsync(events);
            await _yummyDbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var oneEvent = GetById(id);
            _yummyDbContext.Remove(oneEvent);
            _yummyDbContext.SaveChanges();
        }

        public List<Events> GetAllEvents()
        {
            var Events = _yummyDbContext.events.ToList();
            return Events;
        }

        public Events GetById(int id)
        {
            var events = _yummyDbContext.events.Find(id);
            return events;
        }

        public async Task Update(Events events)
        {
            _yummyDbContext.events.Update(events);
            await _yummyDbContext.SaveChangesAsync();
        }
    }
}
