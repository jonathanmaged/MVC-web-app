using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_web_app.Data;
using MVC_web_app.Models;

namespace MVC_web_app.Controllers
{
    public class ItemsController: Controller
    {
        private readonly MyAppDbContext _context;

        public ItemsController(MyAppDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _context.Items
                .Include(r=>r.SerialNumber)
                .Include(r=>r.Category)
                .ToListAsync();
            return View(items);
        }
        public IActionResult Create() {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId")]Item item) {
            if (ModelState.IsValid) { 
                _context.Items.Add(item);  
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }
       
        public async Task<IActionResult> Edit(int id) {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            var item = await _context.Items.Include(x => x.SerialNumber).FirstOrDefaultAsync(x => x.Id==id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Update(item); 
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(item);
        }
        public async Task<IActionResult> Delete(int id) {

            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null) { 
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }
    }
}
