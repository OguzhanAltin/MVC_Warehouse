using MVC_Warehouse.DAL.ORM.Entity;
using MVC_Warehouse.Models.DTO;
using MVC_Warehouse.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Warehouse.Controllers
{
    public class ProductController : BaseController
    {
        
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
                Product product = new Product();

                product.ProductName = model.ProductName;
                product.ProductDescription = model.ProductDescription;
                product.UnitPrice = model.UnitPrice;
                product.UnitInStock = model.UnitInStock;
                product.CategoryID = model.CategoryID;

                db.Products.Add(product);
                db.SaveChanges();

                return Redirect("/Product/ListProduct");
            }
            else
            {
                return View();
            }
        }



        public ActionResult ListProduct()
        {
            List<ProductDTO> model = db.Products
                .Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated)
                .Select(x => new ProductDTO
            {
                ID=x.ID,
                ProductName=x.ProductName,
                ProductDescription = x.ProductDescription,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock,
                CategoryID = x.CategoryID,
                CategoryName = x.Category.CategoryName

            }).ToList();

            return View(model);
        }


        public ActionResult UpdateProduct(int id)
        {
            ProductVM model = new ProductVM();

            Product product = db.Products
                .FirstOrDefault(x => x.ID == id);

            model.Product.ID = product.ID;
            model.Product.ProductName = product.ProductName;
            model.Product.ProductDescription = product.ProductDescription;
            model.Product.UnitPrice = product.UnitPrice;
            model.Product.UnitInStock = product.UnitInStock;

            List<Category> categorymodel = db.Categories.Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated).ToList();

            model.Categories = categorymodel;

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products
                    .FirstOrDefault(x => x.ID == model.ID);

                product.ProductName = model.ProductName;
                product.ProductDescription = model.ProductDescription;
                product.UnitPrice = model.UnitPrice;
                product.UnitInStock = model.UnitInStock;
                product.CategoryID = model.CategoryID;

                product.Status = DAL.ORM.Enum.Status.Updated;
                product.UpdateDate = DateTime.Now;

                db.SaveChanges();

                return Redirect("/Product/ListProduct");
            }
            else
            {
                return View();
            }
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