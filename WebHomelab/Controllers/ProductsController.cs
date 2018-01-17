using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHomelab.Models;
using System.Data.SqlClient;
using System.Data;


namespace WebHomelab.Controllers
{
    public class ProductsController : Controller
    {
        private readonly homelabContext _context;

        public ProductsController(homelabContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Audit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var homelabContext = _context.Audit.Include(a => a.ProductModel).Include(a => a.ProductSubcategory).Include(a => a.SizeUnitMeasureCodeNavigation).Include(a => a.WeightUnitMeasureCodeNavigation).Include(a => a.AuditId == id); 
            // return View(await homelabContext.ToListAsync());
            var product = await _context.Product
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            var nr = product.ProductNumber;
            var productNumber = new SqlParameter("@ProductNumber", nr);
            return View(await _context.Audit.FromSql("usp_GetProductByProductNr @ProductNumber", productNumber).ToListAsync());

           // var productID = new SqlParameter("@ProductID", id);
           // return View(await _context.Audit.FromSql("usp_GetProductByID @ProductID", productID).ToListAsync());
            
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var homelabContext = _context.Product.Include(p => p.ProductModel).Include(p => p.ProductSubcategory).Include(p => p.SizeUnitMeasureCodeNavigation).Include(p => p.WeightUnitMeasureCodeNavigation);
            return View(await homelabContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name");
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode");
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
   
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);

            return View(product); 
        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,UserIdentifier")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    var auditIdParam = new SqlParameter("@AuditIdParam", SqlDbType.BigInt);
                    var nameParam = new SqlParameter("@NameParam", SqlDbType.NVarChar);
                    var productNumberParam = new SqlParameter("@ProductNumberParam", SqlDbType.NVarChar);
                    var makeFlagParam = new SqlParameter("@MakeFlagParam", SqlDbType.Bit);
                    var finishedGoodsFlagParam = new SqlParameter("@FinishedGoodsFlagParam", SqlDbType.Bit);
                    var colorParam = new SqlParameter("@ColorParam", SqlDbType.NVarChar);
                    var safetyStockLevelParam = new SqlParameter("@SafetyStockLevelParam", SqlDbType.SmallInt);
                    var reorderPointParam = new SqlParameter("@ReorderPointParam", SqlDbType.SmallInt);
                    var standardCostParam = new SqlParameter("@StandardCostParam", SqlDbType.Money);
                    var listPriceParam = new SqlParameter("@ListPriceParam", SqlDbType.Money);
                    var sizeParam = new SqlParameter("@SizeParam", SqlDbType.NVarChar);
                    var sizeUnitMeasureCodeParam = new SqlParameter("@SizeUnitMeasureCodeParam", SqlDbType.NVarChar);
                    var weightUnitMeasureCodeParam = new SqlParameter("@WeightUnitMeasureCodeParam", SqlDbType.NVarChar);
                    var weightParam = new SqlParameter("@WeightParam", SqlDbType.Decimal);
                    var daysToManufactureParam = new SqlParameter("@DaysToManufactureParam", SqlDbType.Int);
                    var productLineParam = new SqlParameter("@ProductLineParam", SqlDbType.NChar);
                    var classParam = new SqlParameter("@ClassParam", SqlDbType.NChar);
                    var styleParam = new SqlParameter("@StyleParam", SqlDbType.NChar);
                    var productSubcategoryIdParam = new SqlParameter("@ProductSubcategoryIdParam", SqlDbType.Int);
                    var productModelIdParam = new SqlParameter("@ProductModelIdParam", SqlDbType.Int);
                    var sellStartDateParam = new SqlParameter("@SellStartDateParam", SqlDbType.DateTime);
                    var sellEndDateParam = new SqlParameter("@SellEndDateParam", SqlDbType.DateTime);
                    var discontinuedDateParam = new SqlParameter("@DiscontinuedDateParam", SqlDbType.DateTime);
                    var rowguidParam = new SqlParameter("@RowguidParam", SqlDbType.UniqueIdentifier);
                    var modifiedDateParam = new SqlParameter("@ModifiedDateParam", SqlDbType.DateTime);
                    var userIdentifierParam = new SqlParameter("@UserIdentifierParam", SqlDbType.NVarChar);
                    var auditId = DateTime.Now.ToString("yyyyMMddhhmmss").ToString();

                    long auditID;
                    var time = DateTime.Now.ToString("yyyyMMddhhmmss").ToString();
                    //  var time = Guid.NewGuid().ToString();
                    long.TryParse(time, out auditID);

                    auditIdParam.Value = auditId; //product.ProductId;
                    nameParam.Value = product.Name;
                    productNumberParam.Value = product.ProductNumber;
                    makeFlagParam.Value = product.MakeFlag;
                    finishedGoodsFlagParam.Value = product.FinishedGoodsFlag;
                    colorParam.Value = product.Color;
                    if (colorParam.Value == null)
                        colorParam.Value = DBNull.Value;
                    safetyStockLevelParam.Value = product.SafetyStockLevel;
                    reorderPointParam.Value = product.ReorderPoint;
                    standardCostParam.Value = product.StandardCost;
                    listPriceParam.Value = product.ListPrice;
                    sizeParam.Value = product.Size;
                    if (sizeParam.Value == null)
                        sizeParam.Value = DBNull.Value;
                    sizeUnitMeasureCodeParam.Value = product.SizeUnitMeasureCode;
                    if (sizeUnitMeasureCodeParam.Value == null)
                        sizeUnitMeasureCodeParam.Value = DBNull.Value;
                    weightUnitMeasureCodeParam.Value = product.WeightUnitMeasureCode;
                    if (weightUnitMeasureCodeParam.Value == null)
                        weightUnitMeasureCodeParam.Value = DBNull.Value;
                    weightParam.Value = product.Weight;
                    if (weightParam.Value == null)
                        weightParam.Value = DBNull.Value;
                    daysToManufactureParam.Value = product.DaysToManufacture;
                    productLineParam.Value = product.ProductLine;
                    if (productLineParam.Value == null)
                        productLineParam.Value = DBNull.Value;
                    classParam.Value = product.Class;
                    if (classParam.Value == null)
                        classParam.Value = DBNull.Value;
                    styleParam.Value = product.Style;
                    if (styleParam.Value == null)
                        styleParam.Value = DBNull.Value;
                    productSubcategoryIdParam.Value = product.ProductSubcategoryId;
                    productModelIdParam.Value = product.ProductModelId;
                    sellStartDateParam.Value = product.SellStartDate;
                    sellEndDateParam.Value = product.SellEndDate;
                    discontinuedDateParam.Value = product.DiscontinuedDate;
                    rowguidParam.Value = product.Rowguid;
                    modifiedDateParam.Value = product.ModifiedDate;
                    userIdentifierParam.Value = product.UserIdentifier;
                    if (userIdentifierParam.Value == null)
                        userIdentifierParam.Value = DBNull.Value;

                    /*await _context.Database.ExecuteSqlCommandAsync("EXEC usp_InsertAudit @AuditIdParam,@NameParam,@ProductNumberParam,@MakeFlagParam,@FinishedGoodsFlagParam,@ColorParam,@SafetyStockLevelParam,@ReorderPointParam,@StandardCostParam,@ListPriceParam,@SizeParam,@SizeUnitMeasureCodeParam,@WeightUnitMeasureCodeParam,@WeightParam,@DaysToManufactureParam,@ProductLineParam,@ClassParam,@StyleParam,@ProductSubcategoryIdParam,@ProductModelIdParam,@SellStartDateParam,@SellEndDateParam,@DiscontinuedDateParam,@RowguidParam,@ModifiedDateParam,@UserIdentifierParam",
                           parameters: new[] { auditIdParam, nameParam, productNumberParam, makeFlagParam, finishedGoodsFlagParam, colorParam, safetyStockLevelParam, reorderPointParam, standardCostParam,
                                       listPriceParam, sizeParam, sizeUnitMeasureCodeParam, weightUnitMeasureCodeParam, weightParam, daysToManufactureParam, productLineParam, classParam,
                                       styleParam, productSubcategoryIdParam, productModelIdParam, sellStartDateParam, sellEndDateParam,  discontinuedDateParam, rowguidParam,
                                       modifiedDateParam, userIdentifierParam });*/

                    await _context.Database.ExecuteSqlCommandAsync("EXEC usp_InsertAudit @AuditIdParam,@NameParam,@ProductNumberParam,@MakeFlagParam,@FinishedGoodsFlagParam,@ColorParam,@SafetyStockLevelParam,@ReorderPointParam,@StandardCostParam,@ListPriceParam,@SizeParam,@SizeUnitMeasureCodeParam,@WeightUnitMeasureCodeParam,@WeightParam,@DaysToManufactureParam,@ProductLineParam,@ClassParam,@StyleParam,@ProductSubcategoryIdParam,@ProductModelIdParam,@SellStartDateParam,@SellEndDateParam,@DiscontinuedDateParam,@RowguidParam,@ModifiedDateParam,@UserIdentifierParam",
                          parameters: new[] { auditIdParam, nameParam, productNumberParam, makeFlagParam, finishedGoodsFlagParam, colorParam, safetyStockLevelParam, reorderPointParam, standardCostParam,
                                       listPriceParam, sizeParam, sizeUnitMeasureCodeParam, weightUnitMeasureCodeParam, weightParam, daysToManufactureParam, productLineParam, classParam,
                                       styleParam, productSubcategoryIdParam, productModelIdParam, sellStartDateParam, sellEndDateParam,  discontinuedDateParam, rowguidParam,
                                       modifiedDateParam, userIdentifierParam });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["ProductModelId"] = new SelectList(_context.ProductModel, "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategory, "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasure, "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
           
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductId == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
