using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Context;
using WebApplication3.Models;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Controllers
{
    public class DashboardController : Controller
    {

        ProductMangerContext db = new ProductMangerContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfile(int id)
        {
            Person person = db.People.Single(x => x.Id == id);


            return View(person);

        }


        public ActionResult Edit(int id)
        {

            Person person = db.People.Single(x => x.Id == id);
            EditProfile editProfile = new EditProfile();
            editProfile.Id = person.Id;
            editProfile.FirstName = person.FirstName;
            editProfile.LastName = person.LastName;
            editProfile.Password = person.Password;
            editProfile.PhoneNumber = person.PhoneNumber;
            editProfile.Email = person.Email;
            editProfile.UserName = person.UserName;
            editProfile.Image = person.Image;
            editProfile.RoleUserID = person.RoleUserID;
            return View(editProfile);
        }

        [HttpPost]
        public ActionResult Edit(EditProfile editProfile, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person();
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);

                    file.SaveAs(path);

                    editProfile.Image = pic;
                    person.Image = pic;
                }
                else
                {
                    person.Image = editProfile.Image;
                }
                person.Id = editProfile.Id;
                person.FirstName = editProfile.FirstName;
                person.LastName = editProfile.LastName;
                person.Password = editProfile.Password;
                person.PhoneNumber = editProfile.PhoneNumber;
                person.Email = editProfile.Email;
                person.UserName = editProfile.UserName;
                if (editProfile.RoleUserID == 0)
                    person.RoleUserID = 2;
                else
                    person.RoleUserID = editProfile.RoleUserID;
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return View("Index");
            }
            return View(editProfile);
        }

        public ActionResult UsersPage()
        {
            IEnumerable<Person> person = db.People.ToList();
            return View(person);
        }


    }
}