using CoreMVCIntro_1.Models.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro_1.Controllers
{
    public class CategoryController : Controller
    {


        MyContext _db;

        public CategoryController(MyContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
