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
            Database.SetInitializer<ProductMangerContext>(null);
            
            PostesImagesView postesImages = new PostesImagesView();
            postesImages.articles = db.articles.ToList().Where(x => x.IfAproveed == true);
            postesImages.photos = db.photos.ToList();
            return View(postesImages);
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
                        Person people = db.People.Single(x => x.RoleUserID == 2 && (x.LastName.Contains(search) || x.FirstName.Contains(search) || x.UserName.Contains(search)));

                       
                        Articles = db.articles.ToList().Where(x => (x.IfAproveed == true && people.Id == x.EditorId));

                    }

                    var json = new JavaScriptSerializer().Serialize(Articles);
                    
                    return Json(Articles);
                }
            }

            return Json(new {resulet = 0 });
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
                if (db.People.Single(a => a.Email.Equals(users.Email) && a.Password.Equals(users.Password)) != null)
                {
                    var Use = db.People.Single(a => a.Email.Equals(users.Email) && a.Password.Equals(users.Password));

                    if (Use != null)
                    {
                        Session["UserID"] = Use.Id.ToString();
                        Session["UserName"] = Use.UserName.ToString();
                        Session["UserRole"] = Use.RoleUserID.ToString();
                        Response.Cookies.Add(new HttpCookie("UserID", Use.Id.ToString()));
                        Response.Cookies.Add(new HttpCookie("UserName", Use.UserName.ToString()));

                        Response.Cookies.Add(new HttpCookie("UserRole", Use.RoleUserID.ToString()));




                        if (Use.RoleUserID == 1)
                            return RedirectToAction("Index", "Dashboard");
                        else if (Use.RoleUserID == 2)
                            return RedirectToAction("Index", "Factory");
                        else if (Use.RoleUserID == 3)
                            return RedirectToAction("Index", "Wall");
                        else
                            return View(users);
                    }
                    return View(users);
                }
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
                Session["UserID"] = Users.Person.Id.ToString();
                Session["UserName"] = Users.Person.UserName.ToString();
                Session["UserRole"] = Users.Person.RoleUserID.ToString();
                Response.Cookies.Add(new HttpCookie("UserID", Users.Person.Id.ToString()));
                Response.Cookies.Add(new HttpCookie("UserName", Users.Person.UserName.ToString()));
                Response.Cookies.Add(new HttpCookie("UserRole", Users.Person.RoleUserID.ToString()));

                if (Users.Person.RoleUserID == 3)
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

            
            PostesImagesView postesImages = new PostesImagesView();
            postesImages.articles = db.articles.ToList().Where(x => x.Id == id);
            postesImages.photos = db.photos.ToList();
            return View(postesImages);
        }

        public ActionResult Like(int id)
        {
            if (ModelState.IsValid)
            {
                int userid = int.Parse(Request.Cookies["UserID"].Value.ToString());
                Like like = db.Like.Single(x => x.ART_ID == id && x.person_ID == userid);
                if ( like== null)
                {
                    var article = db.articles.Single(x => x.Id == id);
                    article.NumberOfDislikes = article.NumberOfLikes + 1;
                   
                    like.person_ID = userid;
                    like.ART_ID = id;
                    db.Like.Add(like);
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    return Json(new { respons = 0 });
                }
                
            }


            return Json(new { respons = 1 });
        }
        public ActionResult DisLike(int id)
        {

            if (ModelState.IsValid)
            {
                int userid = int.Parse(Request.Cookies["UserID"].Value.ToString());
                DisLike dislike = db.disLike.Single(x => x.art_ID == id && x.per_ID == userid);
                if (dislike == null) {
                    var article = db.articles.Single(x => x.Id == id);
                    article.NumberOfDislikes = article.NumberOfDislikes + 1;

                    dislike.per_ID = userid; 
                    dislike.art_ID = id;
                    db.disLike.Add(dislike);
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                else
                {
                    return Json(new { respons = 0 });
                }
                
            }


            return Json(new {respons= 1 });
        }

        public ActionResult Save(int id)
        {

            if (ModelState.IsValid)
            {
                if (Request.Cookies["UserID"] != null)
                {
                    int userid = int.Parse(Request.Cookies["UserID"].Value.ToString());
                    Saving saving = db.saving.Single(x => x.PostId == id && x.userId == userid);  

                    
                    if (saving == null)
                    {
                        saving.PostId = id;
                        saving.userId = int.Parse(Request.Cookies["UserID"].Value.ToString());
                        db.saving.Add(saving);
                        db.SaveChanges();
                        return Json(new { respons = 0 });
                    }
                    else
                    {
                        return Json(new { respons = 0 });
                    }
                }
            }

            return Json(new { respons = 1 });
        }
        
        public ActionResult SavedPostes(int id)
        {
            IEnumerable<Saving> save = db.saving.ToList().Where(x => x.userId == id);

    
            return View(save);
        }
        
        public ActionResult ViewProfile(int id)
        {
            Person person = db.People.Single(x => x.Id == id);


            return View(person);

        }

    }
    
}