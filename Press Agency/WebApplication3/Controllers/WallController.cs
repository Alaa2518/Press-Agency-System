using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Context;
using WebApplication3.Models;

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
        public ActionResult Login(Person users)
        {
            
             
            if (ModelState.IsValid)
            {
                using (ProductMangerContext db = new ProductMangerContext())
                {
                    var Use = db.People.Where(a => a.UserName.Equals(users.UserName) && a.Password.Equals(users.Password) ).FirstOrDefault();

                    if (Use != null)
                    {
                        Session["UserID"] = Use.Id.ToString();
                        Session["UserName"] = Use.UserName.ToString();
                        Session["UserRole"] = Use.RoleUserID.ToString();
                        if (Use.RoleUserID == 1)
                            return RedirectToAction("Dashboard", "Index");
                        
                        else if (Use.RoleUserID == 2)
                            return RedirectToAction("Facory", "Index");
                        else if (Use.RoleUserID == 3 )
                            return RedirectToAction("Wall", "Logedin");
                        else
                            return View(users);
                    }
                    
                        
                   
                        

                   

                }
            }
            return View(users);
        }

        
        
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Person user)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(user);
                db.SaveChanges();
            }
            return View(user);
        }
    }
}