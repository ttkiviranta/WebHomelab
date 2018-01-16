using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHomelab.Models;

namespace WebHomelab.Controllers
{
    public class AuditsController : Controller
    {
        private readonly homelabContext _context;

        public AuditsController(homelabContext context)
        {
            _context = context;
        }

        // GET: Audits
        public async Task<IActionResult> Index()
        {
            var homelabContext = _context.Audit.Include(a => a.ProductModel).Include(a => a.ProductSubcategory).Include(a => a.SizeUnitMeasureCodeNavigation).Include(a => a.WeightUnitMeasureCodeNavigation);
            return View(await homelabContext.ToListAsync());
        }

        // GET: Audits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audit = await _context.Audit
                .Include(a => a.ProductModel)
                .Include(a => a.ProductSubcategory)
                .Include(a => a.SizeUnitMeasureCodeNavigation)
                .Include(a => a.WeightUnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.AuditId == id);
            if (audit == null)
            {
                return NotFound();
            }

            return View(audit);
        }

        // GET: Audits/Create
        public IActionResult Create()
        {
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name");
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode");
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: Audits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuditId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name", audit.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name", audit.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", audit.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", audit.WeightUnitMeasureCode);
            return View(audit);
        }

        // GET: Audits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audit = await _context.Audit.SingleOrDefaultAsync(m => m.AuditId == id);
            if (audit == null)
            {
                return NotFound();
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name", audit.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name", audit.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", audit.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", audit.WeightUnitMeasureCode);
            return View(audit);
        }

        // POST: Audits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuditId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] Audit audit)
        {
            if (id != audit.AuditId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditExists(audit.AuditId))
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
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name", audit.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name", audit.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", audit.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", audit.WeightUnitMeasureCode);
            return View(audit);
        }

        // GET: Audits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audit = await _context.Audit
                .Include(a => a.ProductModel)
                .Include(a => a.ProductSubcategory)
                .Include(a => a.SizeUnitMeasureCodeNavigation)
                .Include(a => a.WeightUnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.AuditId == id);
            if (audit == null)
            {
                return NotFound();
            }

            return View(audit);
        }

        // POST: Audits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audit = await _context.Audit.SingleOrDefaultAsync(m => m.AuditId == id);
            _context.Audit.Remove(audit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditExists(int id)
        {
            return _context.Audit.Any(e => e.AuditId == id);
        }
    }
}
