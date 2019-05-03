using MVC_Warehouse.DAL.ORM.Entity;
using MVC_Warehouse.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Warehouse.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult AddProduct()
        {
            List<Category> model = db.Categories
                .Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated)
                .OrderBy(x => x.AddDate)
                .ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                Product pro = new Product();

                pro.ProductName = model.ProductName;
                pro.ProductDescription = model.ProductDescription;
                pro.UnitPrice = model.UnitPrice;
                pro.UnitInStock = model.UnitInStock;

                db.Products.Add(pro);
                db.SaveChanges();

                return Redirect("/Product/ListProduct");
            }

            else return View();
        }

        public ActionResult ListProduct()
        {
            List<ProductDTO> model = db.Products
                .Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated)
                .Select(y => new ProductDTO
                {
                    ID = y.ID,
                    ProductName = y.ProductName,
                    ProductDescription = y.ProductDescription,
                    UnitPrice = y.UnitPrice,
                    UnitInStock = y.UnitInStock

                }).ToList();

            return View(model);
        }

        public ActionResult UpdateProduct(int id)
        {
            Product pro = db.Products

                .FirstOrDefault(x => x.ID == id);

            ProductDTO model = new ProductDTO();

            model.ID = pro.ID;
            model.ProductName = pro.ProductName;
            model.ProductDescription = pro.ProductDescription;
            model.UnitPrice = pro.UnitPrice;
            model.UnitInStock = pro.UnitInStock;

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                Product pro = db.Products

                    .FirstOrDefault(x => x.ID == model.ID);

                pro.ProductName = model.ProductName;
                pro.ProductDescription = model.ProductDescription;
                pro.UnitPrice = model.UnitPrice;
                pro.UnitInStock = model.UnitInStock;

                pro.Status = DAL.ORM.Enum.Status.Updated;
                pro.UpdateDate = DateTime.Now;

                db.SaveChanges();

                return Redirect("/Product/ListProduct");
            }

            else return View();
        }

        public ActionResult DeleteProduct(int id)
        {
            Product pro = db.Products

                .FirstOrDefault(x => x.ID == id);

            pro.Status = DAL.ORM.Enum.Status.Deleted;
            pro.DeleteDate = DateTime.Now;

            db.SaveChanges();

            return Redirect("/Product/ListProduct");
        }
    }
}