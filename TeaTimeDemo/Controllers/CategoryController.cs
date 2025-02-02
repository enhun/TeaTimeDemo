using Microsoft.AspNetCore.Mvc;
using TeaTimeDemo.Data;
using TeaTimeDemo.Models;

namespace TeaTimeDemo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "顯示順序不可以和名稱一樣");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();

                TempData["success"] = "類別新增成功!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "類別修改成功!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "類別修改失敗!";
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                TempData["error"] = "類別修改失敗!";
                return NotFound();
            }
            return View(categoryFromDb);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "類別刪除失敗!";
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                TempData["error"] = "類別刪除失敗!";
                return NotFound();
            }
          
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                TempData["error"] = "類別刪除失敗!";
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "類別刪除成功!";
            return RedirectToAction("Index");
        }
    }
}
