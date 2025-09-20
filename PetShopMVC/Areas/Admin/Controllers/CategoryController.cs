using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopMVC.DataContext;
using PetShopMVC.DataContext.Entities;

namespace PetShopMVC.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var existCategory = _dbContext.Categories.Any(x => x.Name == category.Name);
            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category name already exists");
                return View(category); // stop execution
            }

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var existCategory = _dbContext.Categories.AsNoTracking().FirstOrDefault(x => x.Id == category.Id);
            if (existCategory == null)
            {
                return NotFound();
            }

            var hasNewName = _dbContext.Categories.Any(x => x.Name == category.Name && x.Id != id);
            if (hasNewName)
            {
                ModelState.AddModelError("Name", "This category name already exists");
                return View(category); // stop here
            }

            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost] // better to force POST for delete
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if (category == null) return NotFound();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();

            return Json(new { IsDeleted = true });
        }
    }
}
