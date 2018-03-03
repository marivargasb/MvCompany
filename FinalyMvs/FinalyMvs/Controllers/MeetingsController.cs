﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalyMvs.Models;

namespace FinalyMvs.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly FinalyMvsContext _context;

        public MeetingsController(FinalyMvsContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            if (Login.isActiv == false)
            {
                return View("Views/Users/Index.cshtml");
            }
            var finalyMvsContext = _context.Meeting.Include(m => m.Clients).Include(m => m.Users);
            return View(await finalyMvsContext.ToListAsync());
        }

        // GET: Meetings/Details/5
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

            var meeting = await _context.Meeting
                .Include(m => m.Clients)
                .Include(m => m.Users)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
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

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Type,UsersID,ClientsID")] Meeting meeting)
        {

            if (ModelState.IsValid)
            {
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", meeting.ClientsID);
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name", meeting.UsersID);
            return View(meeting);
        }

        // GET: Meetings/Edit/5
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

            var meeting = await _context.Meeting.SingleOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", meeting.ClientsID);
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name", meeting.UsersID);
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Type,UsersID,ClientsID")] Meeting meeting)
        {
            if (id != meeting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.ID))
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
            ViewData["ClientsID"] = new SelectList(_context.Client, "ID", "Name", meeting.ClientsID);
            ViewData["UsersID"] = new SelectList(_context.Users, "ID", "Name", meeting.UsersID);
            return View(meeting);
        }

        // GET: Meetings/Delete/5
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

            var meeting = await _context.Meeting
                .Include(m => m.Clients)
                .Include(m => m.Users)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meeting.SingleOrDefaultAsync(m => m.ID == id);
            _context.Meeting.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.ID == id);
        }
    }
}
