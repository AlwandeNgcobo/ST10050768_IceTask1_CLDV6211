using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCHeroes.Data;
using MVCHeroes.Models;

namespace MVCHeroes.Controllers
{
    public class HeroesController : Controller
    {
        private readonly MVCHeroesContext _context;

        public HeroesController(MVCHeroesContext context)
        {
            _context = context;
        }

        // GET: Heroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Heroes.ToListAsync());
        }

        public async Task<IActionResult> Search()
        {
            return View(await _context.Heroes.ToListAsync());
        }

        public async Task<IActionResult> ShowResult(string Search)
        {
            return View("Index",await _context.Heroes.Where(h => h.SuperHero.Contains(Search)).ToListAsync());//.Where is like a sql filter to spcify how to select an item
        }

        // GET: Heroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroes = await _context.Heroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroes == null)
            {
                return NotFound();
            }

            return View(heroes);
        }

        // GET: Heroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Heroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SuperHero,Powers")] Heroes heroes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heroes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heroes);
        }

        // GET: Heroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroes = await _context.Heroes.FindAsync(id);
            if (heroes == null)
            {
                return NotFound();
            }
            return View(heroes);
        }

        // POST: Heroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SuperHero,Powers")] Heroes heroes)
        {
            if (id != heroes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heroes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroesExists(heroes.Id))
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
            return View(heroes);
        }

        // GET: Heroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroes = await _context.Heroes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (heroes == null)
            {
                return NotFound();
            }

            return View(heroes);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heroes = await _context.Heroes.FindAsync(id);
            if (heroes != null)
            {
                _context.Heroes.Remove(heroes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeroesExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
    }
}
