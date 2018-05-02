using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Celestial.Models;
using System.Security.Claims;

namespace Celestial.Controllers
{
    [Authorize]
    public class PlanetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlanetsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Planets
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(await _context.Planet.ToListAsync());
        }

        // GET: Planets/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var planet = await _context.Planet
                .Include(p => p.Stats)
                .SingleOrDefaultAsync(m => m.PlanetId == id);
            Stat stats = await _context.Stat
                .Include(p => p.Planet)
                .SingleOrDefaultAsync(s => s.PlanetId == id);
            if (planet == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // GET: Planets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanetId,Name,Government,Economy,Geography,StatId")] Planet planet)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            planet.User = currentUser;
            Stat baseStats = new Stat();
            //Set Baseline capital Stats based on government
            if (planet.Government == "united")
            {
                baseStats.Capital = 500;
            }
            else if (planet.Government == "divided")
            {
                baseStats.Capital = 250;
            }
            else if (planet.Government == "none")
            {
                baseStats.Capital = 100;
            }
            //Set Baseline crystal Stats based on economy
            if(planet.Economy == "civil")
            {
                baseStats.Crystal = 500;
            }
            else if(planet.Economy == "mixed")
            {
                baseStats.Crystal = 300;
            }
            else if(planet.Economy == "savage")
            {
                baseStats.Crystal = 200;
            }
            //Set Baseline population Stats based on economy
            if(planet.Geography == "balanced")
            {
                baseStats.Pop = 500;
            }
            else if(planet.Geography == "wet")
            {
                baseStats.Pop = 300;
            }
            else if(planet.Geography == "dry")
            {
                baseStats.Pop = 150;
            }

            //Create Planet in Database
            if (ModelState.IsValid)
            {
                _context.Planet.Add(planet);
                baseStats.PlanetId = planet.PlanetId;
                _context.Stat.Add(baseStats);
                await _context.SaveChangesAsync();
                var dbStats = await _context.Stat.SingleOrDefaultAsync(m => m.PlanetId == planet.PlanetId);
                planet.StatId = dbStats.StatId;
                _context.Planet.Update(planet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(planet);
        }

        // GET: Planets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planet = await _context.Planet.SingleOrDefaultAsync(m => m.PlanetId == id);
            if (planet == null)
            {
                return NotFound();
            }
            return View(planet);
        }

        // POST: Planets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanetId,Name,government,economy,geography,StatId")] Planet planet)
        {
            if (id != planet.PlanetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetExists(planet.PlanetId))
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
            return View(planet);
        }

        // GET: Planets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planet = await _context.Planet
                .SingleOrDefaultAsync(m => m.PlanetId == id);
            if (planet == null)
            {
                return NotFound();
            }

            return View(planet);
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planet = await _context.Planet.SingleOrDefaultAsync(m => m.PlanetId == id);
            _context.Planet.Remove(planet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlanetExists(int id)
        {
            return _context.Planet.Any(e => e.PlanetId == id);
        }
    }
}
