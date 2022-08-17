using CoreMVCIntro_1.Models.Context;
using CoreMVCIntro_1.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCIntro_1.ExtensionTools;
using CoreMVCIntro_1.VMClasses;

namespace CoreMVCIntro_1.Controllers
{
    public class CategoryController : Controller
    {


        MyContext _db;

        public CategoryController(MyContext db)
        {
            _db = db;
        }


        public IActionResult AddCategory()
        {
            Product p = new Product()
            {
                ProductName = "Deneme"
            };

            HttpContext.Session.SetObject("pro", p);
            
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            Product p = HttpContext.Session.GetObject<Product>("pro");

            CategoryVM cvm = new CategoryVM()
            {
                 Categories = _db.Categories.ToList()
            };

            return View(cvm);
        }

        public IActionResult DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = new CategoryVM
            {
                Category = _db.Categories.Find(id)
            };
            return View(cvm);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            Category toBeUpdated = _db.Categories.Find(category.ID);
            toBeUpdated.CategoryName = category.CategoryName;
            toBeUpdated.Description = category.Description;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Deneme()
        {
            return View();
        }
    }
}
