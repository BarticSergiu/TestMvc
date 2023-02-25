using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvc.Data;
using TestMvc.Models;

namespace TestMvc.Controllers
{
    public class SarciniController : Controller
    {
        private readonly TestMvcContext _context;

        public SarciniController(TestMvcContext context)
        {
            _context = context;
        }

        // GET: Persoana
        private List<SelectListItem> GetPersoana()
        {
            var listaPersoane = new List<SelectListItem>();
            List<Persoana> Persoana = _context.Persoana.ToList();
            listaPersoane = Persoana.Select(lP => new SelectListItem()
            {
                Value = lP.PersoanaId.ToString(),
                Text = lP.Nume
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-- Selecteză Persoană --"
            };
            listaPersoane.Insert(0, defItem);
            return listaPersoane;
        }

        // GET: Sarcini
        public async Task<IActionResult> Index()
        {
            var proiectContext = _context.Sarcina.Include(s => s.Persoana);
            return View(await proiectContext.ToListAsync());
        }

        // GET: Sarcini/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sarcina == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcina
                .Include(s => s.Persoana)
                .FirstOrDefaultAsync(m => m.SarcinaId == id);
            if (sarcina == null)
            {
                return NotFound();
            }

            return View(sarcina);
        }

        // GET: Sarcini/Create
        public IActionResult Create()
        {
            ViewBag.PersoanaId = GetPersoana();
            return View();
        }

        // POST: Sarcini/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SarcinaId,Denumire,Descriere,Prioritate,TipSarcina,OreEstimate,PersoanaId")] Sarcina sarcina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sarcina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Prioritate = sarcina.Prioritate.ToString();
            ViewBag.TipSarcina = sarcina.TipSarcina.ToString();
            return View(sarcina);
        }

        // GET: Sarcini/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sarcina == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcina.FindAsync(id);
            if (sarcina == null)
            {
                return NotFound();
            }
            ViewData["PersoanaId"] = new SelectList(_context.Persoana, "PersoanaId", "Nume", sarcina.Persoana);
            return View(sarcina);
        }

        // POST: Sarcini/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SarcinaId,Denumire,Descriere,Prioritate,TipSarcina,OreEstimate,PersoanaId")] Sarcina sarcina)
        {
            if (id != sarcina.SarcinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sarcina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SarcinaExists(sarcina.SarcinaId))
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
            ViewData["PersoanaId"] = new SelectList(_context.Persoana, "PersoanaId", "Nume", sarcina.Persoana);
            return View(sarcina);
        }

        // GET: Sarcini/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sarcina == null)
            {
                return NotFound();
            }

            var sarcina = await _context.Sarcina
                .Include(s => s.Persoana)
                .FirstOrDefaultAsync(m => m.SarcinaId == id);
            if (sarcina == null)
            {
                return NotFound();
            }

            return View(sarcina);
        }

        // POST: Sarcini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sarcina == null)
            {
                return Problem("Entity set 'ProiectContext.Sarcina'  is null.");
            }
            var sarcina = await _context.Sarcina.FindAsync(id);
            if (sarcina != null)
            {
                _context.Sarcina.Remove(sarcina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SarcinaExists(int id)
        {
            return _context.Sarcina.Any(e => e.SarcinaId == id);
        }
    }
}
