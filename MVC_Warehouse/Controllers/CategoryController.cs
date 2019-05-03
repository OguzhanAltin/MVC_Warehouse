using MVC_Warehouse.DAL.ORM.Entity;
using MVC_Warehouse.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Warehouse.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                Category cat = new Category();

                cat.CategoryName = model.CategoryName;
                cat.CategoryDescription = model.CategoryDescription;

                db.Categories.Add(cat);
                db.SaveChanges();

                return Redirect("/Category/ListCategory");
            }

            else return View();
        }

        public ActionResult ListCategory()
        {
            List<CategoryDTO> model = db.Categories
                .Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated)
                .Select(y => new CategoryDTO
                {
                    ID = y.ID,
                    CategoryName = y.CategoryName,
                    CategoryDescription = y.CategoryDescription

                }).ToList();

            return View(model);
        }
            
        public ActionResult UpdateCategory(int id)
        {
            Category cat = db.Categories
                .FirstOrDefault(x => x.ID == id);

            CategoryDTO model = new CategoryDTO();

            model.ID = cat.ID;
            model.CategoryName = cat.CategoryName;
            model.CategoryDescription = cat.CategoryDescription;

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                Category cat = db.Categories
                    .FirstOrDefault(x => x.ID == model.ID);

                cat.CategoryName = model.CategoryName;
                cat.CategoryDescription = model.CategoryDescription;

                cat.Status = DAL.ORM.Enum.Status.Updated;
                cat.UpdateDate = DateTime.Now;

                db.SaveChanges();

                return Redirect("/Category/ListCategory");
            }

            else return View();
        }

        public ActionResult DeleteCategory(int id)
        {
            Category cat = db.Categories
                .FirstOrDefault(x => x.ID == id);

            cat.Status = DAL.ORM.Enum.Status.Deleted;
            cat.DeleteDate = DateTime.Now;

            db.SaveChanges();

            return Redirect("/Category/ListCategory");
        }
    }
}