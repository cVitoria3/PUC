﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app_web_backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace app_web_backend.Controllers
{
    [Authorize]
    public class VeiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Veiculos.ToListAsync());
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculos == null)
            {
                return NotFound();
            }

            return View(veiculos);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Placa")] Veiculos veiculos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculos);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos.FindAsync(id);
            if (veiculos == null)
            {
                return NotFound();
            }
            return View(veiculos);
        }
        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Relatorio(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos
                .Include(t => t.Consumos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veiculos == null)
            {
                return NotFound();
            }
            return View(veiculos);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Placa")] Veiculos veiculos)
        {
            if (id != veiculos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculosExists(veiculos.Id))
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
            return View(veiculos);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veiculos == null)
            {
                return NotFound();
            }

            var veiculos = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculos == null)
            {
                return NotFound();
            }

            return View(veiculos);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veiculos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Veiculos'  is null.");
            }
            var veiculos = await _context.Veiculos.FindAsync(id);
            if (veiculos != null)
            {
                _context.Veiculos.Remove(veiculos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculosExists(int id)
        {
          return _context.Veiculos.Any(e => e.Id == id);
        }
    }
}
