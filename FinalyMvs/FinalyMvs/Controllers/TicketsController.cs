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
    public class TicketsController : Controller
    {
        private readonly FinalyMvsContext _context;

        public TicketsController(FinalyMvsContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            var finalyMvsContext = _context.Tickets.Include(t => t.Clients).Include(t => t.Users);
            return View(await finalyMvsContext.ToListAsync());
        }

        // GET: Tickets/Details/5
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

            var tickets = await _context.Tickets
                .Include(t => t.Clients)
                .Include(t => t.Users)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name");
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Problem,State,UsersID,ClientsID")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", tickets.ClientsID);
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name", tickets.UsersID);
            return View(tickets);
        }

        // GET: Tickets/Edit/5
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

            var tickets = await _context.Tickets.SingleOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", tickets.ClientsID);
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name", tickets.UsersID);
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Problem,State,UsersID,ClientsID")] Tickets tickets)
        {
            if (id != tickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.Id))
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
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", tickets.ClientsID);
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name", tickets.UsersID);
            return View(tickets);
        }

        // GET: Tickets/Delete/5
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

            var tickets = await _context.Tickets
                .Include(t => t.Clients)
                .Include(t => t.Users)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tickets = await _context.Tickets.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tickets.Remove(tickets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
