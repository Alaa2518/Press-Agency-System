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
      
        
        [HttpGet]
        public ActionResult Filter(string search)
        {
            if (ModelState.IsValid)
            {
               
                if(search !=null)
                {
                    IEnumerable<Article> Articles = db.articles.ToList().Where(x => x.IfAproveed == true && (x.ArticleTitle.Contains(search) || x.ArticleType.Contains(search)));
                    if (Articles == null)
                    {
                        Person people = db.People.FirstOrDefault(x => x.RoleUserID == 2 && (x.LastName.Contains(search) || x.FirstName.Contains(search) || x.UserName.Contains(search)));

                       if(people != null)
                          Articles = db.articles.ToList().Where(x => (x.IfAproveed == true && people.Id == x.EditorId));

                    }

                    
                    
                    return Json(new { resulet = 1 , Articles }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new {resulet = 0 },JsonRequestBehavior.AllowGet);
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
                if (db.People.FirstOrDefault(a => a.Email.Equals(users.Email) && a.Password.Equals(users.Password)) != null)
                {
                    var Use = db.People.FirstOrDefault(a => a.Email.Equals(users.Email) && a.Password.Equals(users.Password));

                    if (Use != null)
                    {
                        Session["user"] = Use;
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

        [HttpGet]
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
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
                Session["user"] = Users.Person;
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
            
            if (Session["user"] != null) {
                int userid = int.Parse(((Person)Session["user"]).Id.ToString());

                Saving save = db.saving.FirstOrDefault(x => x.PostId == id && x.userId ==userid);
                if (save != null)
                {
                    db.saving.Remove(save);
                    db.SaveChanges();
                }

            }
            return View(postesImages);
        }
        
        public ActionResult Like(int id)
        {
            if (ModelState.IsValid)
            {
                
                int userid = int.Parse(((Person)Session["user"]).Id.ToString());
                LikesPost like = db.LikesPosts.FirstOrDefault(x => x.ART_ID == id && x.Pers_ID == userid);
                if ( like == null)
                {
                    LikesPost like1 = new LikesPost();
                    var article = db.articles.Single(x => x.Id == id);
                    article.NumberOfDislikes = article.NumberOfLikes + 1;
                    DisLike dislike = db.disLike.FirstOrDefault(x => x.art_ID == id && x.per_ID == userid);
                    if (dislike != null )
                    {
                        if(article.NumberOfDislikes > 0)
                          article.NumberOfDislikes = article.NumberOfDislikes - 1;
                        db.disLike.Remove(dislike);
                    }
                    like1.Pers_ID = userid;
                    like1.ART_ID = id;
                    db.LikesPosts.Add(like1);
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { respons = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { respons = 0 }, JsonRequestBehavior.AllowGet);
                }
                
            }


            return Json(new { respons = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DisLike(int id)
        {

            if (ModelState.IsValid)
            {
                
                int userid = int.Parse(((Person)Session["user"]).Id.ToString());
                DisLike dislike = db.disLike.FirstOrDefault(x => x.art_ID == id && x.per_ID == userid);
                if (dislike == null) {
                    var article = db.articles.Single(x => x.Id == id);
                    article.NumberOfDislikes = article.NumberOfDislikes + 1;
                    LikesPost like = db.LikesPosts.FirstOrDefault(x => x.ART_ID == id && x.Pers_ID == userid);
                    if (like != null)
                    {
                        if(article.NumberOfLikes > 0 )
                        article.NumberOfLikes = article.NumberOfLikes - 1;
                        db.LikesPosts.Remove(like);
                    }
                    DisLike dislike1 = new DisLike();
                    dislike1.per_ID = userid; 
                    dislike1.art_ID = id;
                    
                    db.disLike.Add(dislike1);
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { respons = 1 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { respons = 0 }, JsonRequestBehavior.AllowGet);
                }
                
            }


            return Json(new {respons= 0 }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult Save(int id)
        {

            if (ModelState.IsValid)
            {
                if (Session["user"] != null)
                {
                    
                    int userid = int.Parse(((Person)Session["user"]).Id.ToString());
                    Saving saving = db.saving.FirstOrDefault(x => x.PostId == id && x.userId == userid);  

                    
                    if (saving == null)
                    {
                        Saving saving1 = new Saving();
                        saving1.PostId = id;
                        saving1.userId = int.Parse(((Person)Session["user"]).Id.ToString());
                        db.saving.Add(saving1);
                        db.SaveChanges();
                        return Json(new { respons = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { respons = 0 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(new { respons = 0 }, JsonRequestBehavior.AllowGet);
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
                else {
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
                    person.RoleUserID = 3;
                else
                    person.RoleUserID = editProfile.RoleUserID;
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editProfile);
        }

        public ActionResult AddQuestion(Questions questions)
        {

            
            db.questions.Add(questions);
            db.SaveChanges();

            return Json(new { result = 1 });
        }

    }
    
}