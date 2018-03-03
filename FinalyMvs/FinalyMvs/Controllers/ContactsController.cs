using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalyMvs.Models;

namespace FinalyMvs.Controllers
{
    public class ContactsController : Controller
    {
        private readonly FinalyMvsContext _context;

        public ContactsController(FinalyMvsContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(int? id)
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }

            if (id == null)
            {
                var finalyMvsContext = _context.Contact.Include(c => c.Clients);
                return View(await finalyMvsContext.ToListAsync());
            }
            else
            {

                var finalyMvsContext = _context.Contact.Include(c => c.Clients).Where(t => t.ClientsID == id); ;
                return View(await finalyMvsContext.ToListAsync());
            }

            
            return View();
        }

        public async Task<IActionResult> List(int? id)
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }

            var finalyMvsContext = _context.Contact.Include(c => c.Clients).Where(t => t.ClientsID == id); ;
            return View(await finalyMvsContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Clients)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,LastName,Mail,Position,Phone,ClientsID")] Contact contact)
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", contact.ClientsID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", contact.ClientsID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,LastName,Mail,Position,Phone,ClientsID")] Contact contact)
        {
            if (id != contact.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "ID", contact.ClientsID);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .Include(c => c.Clients)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.ID == id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ID == id);
        }
    }
}
