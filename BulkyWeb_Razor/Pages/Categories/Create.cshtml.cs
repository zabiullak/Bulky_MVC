using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "the DisplayOder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["success"] = "Category added successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
