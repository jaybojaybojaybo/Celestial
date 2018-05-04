using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Celestial.Models;

namespace Celestial.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stat.Include(s => s.Planet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat = await _context.Stat
                .Include(s => s.Planet)
                .SingleOrDefaultAsync(m => m.StatId == id);
            if (stat == null)
            {
                return NotFound();
            }

            return View(stat);
        }

        // GET: Stats/Create
        public IActionResult Create()
        {
            ViewData["PlanetId"] = new SelectList(_context.Planet, "PlanetId", "PlanetId");
            return View();
        }

        // POST: Stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatId,capital,crystal,pop,stability,PlanetId")] Stat stat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PlanetId"] = new SelectList(_context.Planet, "PlanetId", "PlanetId", stat.PlanetId);
            return View(stat);
        }

        // GET: Stats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat = await _context.Stat.SingleOrDefaultAsync(m => m.StatId == id);
            if (stat == null)
            {
                return NotFound();
            }
            ViewData["PlanetId"] = new SelectList(_context.Planet, "PlanetId", "PlanetId", stat.PlanetId);
            return View(stat);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatId,capital,crystal,pop,stability,PlanetId")] Stat stat)
        {
            if (id != stat.StatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatExists(stat.StatId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PlanetId"] = new SelectList(_context.Planet, "PlanetId", "PlanetId", stat.PlanetId);
            return View(stat);
        }

        // GET: Stats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat = await _context.Stat
                .Include(s => s.Planet)
                .SingleOrDefaultAsync(m => m.StatId == id);
            if (stat == null)
            {
                return NotFound();
            }

            return View(stat);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stat = await _context.Stat.SingleOrDefaultAsync(m => m.StatId == id);
            _context.Stat.Remove(stat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StatExists(int id)
        {
            return _context.Stat.Any(e => e.StatId == id);
        }

        [HttpPost]
        public async Task<IActionResult> GetStats(int id)
        {
            var stat = await _context.Stat.Include(s => s.Planet).FirstOrDefaultAsync(m => m.StatId == id);
            return RedirectToAction("Details");
        }

        [HttpPost]
        public async Task<IActionResult> NaturalDisaster(int id)
        {
            var stat = await _context.Stat
                .Include(p => p.Planet)
                .SingleOrDefaultAsync(m => m.StatId == id);
            stat.Capital -= 50;
            stat.Crystal -= 50;
            stat.Pop -= 50;
            _context.Stat.Update(stat);
            await _context.SaveChangesAsync();
            return Json(stat);
        }
    }
}
