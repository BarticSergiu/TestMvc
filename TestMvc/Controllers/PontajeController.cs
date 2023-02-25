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
    public class PontajeController : Controller
    {
        private readonly TestMvcContext _context;

        public PontajeController(TestMvcContext context)
        {
            _context = context;
        }

        // GET: Pontaje
        public async Task<IActionResult> Index()
        {
            var proiectContext = _context.Pontaj.Include(p => p.Sarcina);
            return View(await proiectContext.ToListAsync());
        }


        // GET: Sarcina
        private List<SelectListItem> GetSarcina()
        {
            var listaSarcina = new List<SelectListItem>();
            List<Sarcina> Persoana = _context.Sarcina.ToList();
            listaSarcina = Persoana.Select(lS => new SelectListItem()
            {
                Value = lS.SarcinaId.ToString(),
                Text = lS.Denumire
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-- Selecteză Persoană --"
            };
            listaSarcina.Insert(0, defItem);
            return listaSarcina;
        }

        // GET: Pontaje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pontaj == null)
            {
                return NotFound();
            }

            var pontaj = await _context.Pontaj
                .Include(p => p.Sarcina)
                .FirstOrDefaultAsync(m => m.PontajId == id);
            if (pontaj == null)
            {
                return NotFound();
            }

            return View(pontaj);
        }

        // GET: Pontaje/Create
        public IActionResult Create()
        {
            ViewBag.SarcinaId = GetSarcina();
            ViewData["SarcinaId"] = new SelectList(_context.Sarcina, "SarcinaId", "Denumire");
            return View();
        }

        // POST: Pontaje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PontajId,Data,Durata,SarcinaId,Observatii")] Pontaj pontaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pontaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SarcinaId"] = new SelectList(_context.Sarcina, "SarcinId", "Denumire", pontaj.Sarcina);
            return View(pontaj);
        }

        // GET: Pontaje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pontaj == null)
            {
                return NotFound();
            }

            var pontaj = await _context.Pontaj.FindAsync(id);
            if (pontaj == null)
            {
                return NotFound();
            }
            ViewData["SarcinaId"] = new SelectList(_context.Sarcina, "SarcinaId", "Denumire", pontaj.Sarcina);
            return View(pontaj);
        }

        // POST: Pontaje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PontajId,Data,Durata,SarcinaId,Observatii")] Pontaj pontaj)
        {
            if (id != pontaj.PontajId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontajExists(pontaj.PontajId))
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
            ViewData["SarcinaId"] = new SelectList(_context.Sarcina, "SarcinId", "Denumire", pontaj.Sarcina);
            return View(pontaj);
        }

        // GET: Pontaje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pontaj == null)
            {
                return NotFound();
            }

            var pontaj = await _context.Pontaj
                .Include(p => p.Sarcina)
                .FirstOrDefaultAsync(m => m.PontajId == id);
            if (pontaj == null)
            {
                return NotFound();
            }

            return View(pontaj);
        }

        // POST: Pontaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pontaj == null)
            {
                return Problem("Entity set 'ProiectContext.Pontaj'  is null.");
            }
            var pontaj = await _context.Pontaj.FindAsync(id);
            if (pontaj != null)
            {
                _context.Pontaj.Remove(pontaj);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontajExists(int id)
        {
          return _context.Pontaj.Any(e => e.PontajId == id);
        }
    }
}
