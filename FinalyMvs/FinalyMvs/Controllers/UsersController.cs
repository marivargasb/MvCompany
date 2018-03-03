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
    public class UsersController : Controller
    {
        private readonly FinalyMvsContext _context;

        public UsersController(FinalyMvsContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            if (Login.isActiv == true)
            {

                if (Login.isAdmin)
                {

                    return View("Views/Home/Contact.cshtml");
                }
                else
                {
                    return View("Views/Home/About.cshtml");
                }
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string Card, string Password)
        {


            if ((Card == null) || (Password == null))
            {
                ViewData["Message"] = "The fields can not be empty";
                return View("Index");

            }
            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Card == Card & m.Password == Password & m.Type == "Administrator");

            if (users == null)
            {
                users = await _context.Users
                   .SingleOrDefaultAsync(m => m.Card == Card & m.Password == Password & m.Type == "User");
                if (users == null)
                {

                    ViewData["Message"] = "This account does not exist in our system";

                }
                else
                {

                    Login.name = users.Name;
                    Login.isActiv = true;
                    Login.isAdmin = false;
                    return View("Views/Home/About.cshtml", users);

                }

            }
            else
            {
                Login.name = users.Name;
                Login.isActiv = true;
                Login.isAdmin = true;



                return View("Views/Home/Contact.cshtml", users);
            }

            return View("Index");
        }


        public async Task<IActionResult> List()
        {
            if (Login.isActiv == true)
            {

                if (!(Login.isAdmin))
                {

                    return View("Views/Home/About.cshtml");
                }


            }
            else
            {
                return View("Views/Users/Index.cshtml");
            }


            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (Login.isActiv == true)
            {

                if (!(Login.isAdmin))
                {

                    return View("Views/Home/About.cshtml");
                }


            }
            else
            {
                return View("Views/Users/Index.cshtml");
            }

            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (Login.isActiv == true)
            {

                if (!(Login.isAdmin))
                {

                    return View("Views/Home/About.cshtml");
                }


            }
            else
            {
                return View("Views/Users/Index.cshtml");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Card,Name,LastName,Password,PageWeb,Address,Phone,Type")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Login.isActiv == true)
            {

                if (!(Login.isAdmin))
                {

                    return View("Views/Home/About.cshtml");
                }


            }
            else
            {
                return View("Views/Users/Index.cshtml");
            }

            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Card,Name,LastName,Password,PageWeb,Address,Phone,Type")] Users users)
        {
            if (id != users.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (Login.isActiv == true)
            {

                if (!(Login.isAdmin))
                {

                    return View("Views/Home/About.cshtml");
                }


            }
            else
            {
                return View("Views/Users/Index.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

             public async Task<IActionResult> SignOut()
        {

            if (Login.isActiv)
            {
                Login.isActiv = false;
                Login.name = null;

            }
            return RedirectToAction("Index", "Users");
        }
    }
}
