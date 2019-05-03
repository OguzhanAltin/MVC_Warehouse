using MVC_Warehouse.DAL.ORM.Entity;
using MVC_Warehouse.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Warehouse.Controllers
{
    public class AppUserController : BaseController
    {
        // GET: AppUser
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(AppUserDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Password = model.Password;

                db.AppUsers.Add(user);
                db.SaveChanges();

                return Redirect("/AppUser/UserList");
            }

            else return View();
        }

        public ActionResult UserList()
        {
            List<AppUserDTO> model = db.AppUsers
                .Where(x => x.Status == DAL.ORM.Enum.Status.Active || x.Status == DAL.ORM.Enum.Status.Updated)

            .Select(y => new AppUserDTO
            {
                ID = y.ID,
                FirstName = y.FirstName,
                LastName = y.LastName,
                Email = y.Email,
                Password = y.Password

            }).ToList();

            return View(model);
        }

        public ActionResult UpdateUser(int id)
        {
            AppUser user = db.AppUsers

                .FirstOrDefault(x => x.ID == id);

            AppUserDTO model = new AppUserDTO();

            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.Email = user.Email;
            model.Password = user.Password;

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateUser(AppUserDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = db.AppUsers

                    .FirstOrDefault(x => x.ID == model.ID);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Password = model.Password;

                user.Status = DAL.ORM.Enum.Status.Updated;
                user.UpdateDate = DateTime.Now;

                db.SaveChanges();

                return Redirect("/AppUser/UserList");
            }

            else return View();
        }

        public ActionResult DeleteUser(int id)
        {
            AppUser user = db.AppUsers
               .FirstOrDefault(x => x.ID == id);

            user.Status = DAL.ORM.Enum.Status.Deleted;
            user.DeleteDate = DateTime.Now;

            db.SaveChanges();

            return Redirect("/AppUser/UserList");
        }
    }
}