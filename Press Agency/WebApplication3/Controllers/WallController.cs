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
using System.Web.Script.Serialization;
using System.Data.Entity;

namespace WebApplication3.Controllers
{
    public class WallController : Controller
    {
        ProductMangerContext db = new ProductMangerContext();
        
        // GET: Wall
        public ActionResult Index()
        {
            IEnumerable<Article> Articles = db.articles.ToList().Where(x => x.IfAproveed == true);
            
            return View(Articles);
        }
      
        
        [HttpPost]
        public ActionResult Filter(string search)
        {
            if (ModelState.IsValid)
            {
               
                if(search != "")
                {
                    IEnumerable<Article> Articles = db.articles.ToList().Where(x => x.IfAproveed == true && (x.ArticleTitle.Contains(search) || x.ArticleType.Contains(search)));
                    if (Articles == null)
                    {
                        IEnumerable<Person> people = db.People.ToList().Where(x => x.RoleUserID == 2 && (x.LastName.Contains(search) || x.FirstName.Contains(search) || x.UserName.Contains(search)));

                        Articles = getArricles(people);
                        Articles = db.articles.ToList().Where(x => x.IfAproveed == true);

                    }

                    var json = new JavaScriptSerializer().Serialize(Articles);
                    return Json(json);
                }
            }

            return Json(new { });
        }
        public IEnumerable<Article> getArricles(IEnumerable<Person> people)
        {
            IEnumerable<Article> Articles ;
            foreach(var p in people)
            {
                Articles = db.articles.ToList().Where(x =>x.EditorId == p.Id&& x.IfAproveed==true);
                if (Articles != null)
                {
                    return (Articles);

                }
            }
            return (null);
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
        
        public ActionResult Show(int id) {

            IEnumerable<Article> article = db.articles.ToList().Where(x => x.Id == id) ;

            return View(article);
        }

        public ActionResult Like(int id)
        {
            if (ModelState.IsValid)
            {

                var article = db.articles.Single(x => x.Id == id);
                article.NumberOfLikes = article.NumberOfLikes + 1;

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
            }

            
            return RedirectToAction("Index");
        }
        public ActionResult DisLike(int id)
        {

            if (ModelState.IsValid)
            {

                var article = db.articles.Single(x => x.Id == id);
                article.NumberOfDislikes = article.NumberOfDislikes + 1;

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        public ActionResult Save(int id)
        {

            if (ModelState.IsValid)
            {

                Saving saving = new Saving();
                saving.PostId = id;
                ;
                saving.userId = int.Parse(Session["UserID"]);
                db.saving.Add(saving);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}