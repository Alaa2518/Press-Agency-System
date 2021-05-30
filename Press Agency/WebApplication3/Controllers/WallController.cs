using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication3.Context;
using WebApplication3.Models;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Controllers
{
    public class WallController : Controller
    {
        ProductMangerContext db = new ProductMangerContext();
        
        // GET: Wall
        public ActionResult Index()
        {
            List<Article> Articles = db.articles.ToList();
            return View(Articles);
        }
        
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLongin users)
        {
            
             
            if (ModelState.IsValid)
            {
                
                 var Use = db.People.Where(a => a.Email.Equals(users.Email) && a.Password.Equals(users.Password) ).FirstOrDefault();
           
                    if (Use != null)
                    {
                        Session["UserID"] = Use.Id.ToString();
                        Session["UserName"] = Use.Email.ToString();
                        Session["UserRole"] = Use.RoleUserID.ToString();
                        
                        if (Use.RoleUserID == 1)
                            return RedirectToAction( "Index", "Dashboard");
                        else if (Use.RoleUserID == 2)
                            return RedirectToAction( "Index", "Factory");
                        else if (Use.RoleUserID == 3)
                            return RedirectToAction( "Logedin", "Wall");
                        else 
                            return View(users);
                    }
                return View(users);

            }
            return View(users);
        }

        
        
        public ActionResult Register()
        {
            PersonRoleViewModel Users = new PersonRoleViewModel
            {
                userRoles = db.UserRoles.ToList().Where(x => x.Id != 1)
            };
            return View(Users);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Register(PersonRoleViewModel Users, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);

                    file.SaveAs(path);
                    Users.Person.Image = pic;
                    
                }
                db.People.Add(Users.Person);
                db.SaveChanges();
                if(Users.Person.RoleUserID == 3)
                {
                    return RedirectToAction("Index");
                }
                else if (Users.Person.RoleUserID == 2)
                {
                    return RedirectToAction("Index", "Factory");
                }
                

            }
            
          
            IEnumerable<UserRole> department = db.UserRoles.ToList().Where(x =>x.Id != 1 ) ;
            Users.userRoles = department;
            return View(Users);
        }

       
    }
}