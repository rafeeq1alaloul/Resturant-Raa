using Microsoft.AspNetCore.Mvc;
using Resturant.Interfaces;
using Resturant.Models;
using Resturant.Repositories;
using Resturant.Services;
using Resturant.ViewModel;

namespace Resturant.Areas.Admin.Controllers
{
    public class EventsController : BaseController
    {

        private readonly IEventsRepository _eventsRepository;
        private readonly IImageServices _imageServices;

        public EventsController(IEventsRepository eventsRepository, IImageServices imageServices) {
            _eventsRepository = eventsRepository;
            _imageServices = imageServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_eventsRepository.GetAllEvents());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(EventsVM eventsVM)
        {
            if (ModelState.IsValid)
            {
                var Img = await _imageServices.UploadImage(eventsVM.Image);
                Events events = new Events
                {
                    Title = eventsVM.Title,
                    Description = eventsVM.Description,
                    Price = eventsVM.Price,
                    ImagePath = Img
                };

                await _eventsRepository.Add(events);
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(eventsVM); 
            }

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null || _eventsRepository.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                var events = _eventsRepository.GetById(id);

                EventsVM eventsVM = new EventsVM
                {
                    Description = events.Description,
                    Price = events.Price,
                    Title = events.Title,
                };


                return View(eventsVM);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventsVM eventsVM)
        {
            if (ModelState.IsValid)
            {
                var menus = _eventsRepository.GetById(id);
                var Img = await _imageServices.UploadImage(eventsVM.Image);

                menus.Description = eventsVM.Description;
                menus.Title= eventsVM.Title;
                menus.ImagePath = Img;
                menus.Price = eventsVM.Price;

                await _eventsRepository.Update(menus);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(eventsVM);
            }
        }


        public IActionResult Delete(int id)
        {
            if (id == null || _eventsRepository.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                _eventsRepository.Delete(id);
                return Ok(new { message = "Deleted Succsesfully" });
            }
        }
    }
}
