﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestiuneSaliNET7.Data;
using GestiuneSaliNET7.Models;

namespace GestiuneSaliNET7.Controllers
{
    public class LabRoomsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LabRoomsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: LabRooms
        public async Task<IActionResult> Index()
        {
              return _context.Labs != null ? 
                          View(await _context.Labs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.Labs'  is null.");
        }

        // GET: LabRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Labs == null)
            {
                return NotFound();
            }

            var labRoomModel = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labRoomModel == null)
            {
                return NotFound();
            }

            return View(labRoomModel);
        }

        // GET: LabRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LabRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Capacity,HasComputers,Id,Name")] LabRoomModel labRoomModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labRoomModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labRoomModel);
        }

        // GET: LabRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Labs == null)
            {
                return NotFound();
            }

            var labRoomModel = await _context.Labs.FindAsync(id);
            if (labRoomModel == null)
            {
                return NotFound();
            }
            return View(labRoomModel);
        }

        // POST: LabRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Capacity,HasComputers,Id,Name")] LabRoomModel labRoomModel)
        {
            if (id != labRoomModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labRoomModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabRoomModelExists(labRoomModel.Id))
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
            return View(labRoomModel);
        }

        // GET: LabRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Labs == null)
            {
                return NotFound();
            }

            var labRoomModel = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labRoomModel == null)
            {
                return NotFound();
            }

            return View(labRoomModel);
        }

        // POST: LabRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Labs == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Labs'  is null.");
            }
            var labRoomModel = await _context.Labs.FindAsync(id);
            if (labRoomModel != null)
            {
                _context.Labs.Remove(labRoomModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabRoomModelExists(int id)
        {
          return (_context.Labs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
