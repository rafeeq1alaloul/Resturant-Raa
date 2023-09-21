using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly YummyDbContext _context;

        public ContactController(YummyDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Contact
        public async Task<IActionResult> Index()
        {
              return _context.contacts != null ? 
              View(await _context.contacts.ToListAsync()) :
              Problem("Entity set 'YummyDbContext.contacts'  is null.");
        }

        // GET: Admin/Contact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }


        public IActionResult Delete(int id)
        {
            var contact = _context.contacts.Find(id);
            _context.Remove(contact);
            _context.SaveChanges();
            return Ok(new { message = "Deleted successfully" });
            // return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
          return (_context.contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
