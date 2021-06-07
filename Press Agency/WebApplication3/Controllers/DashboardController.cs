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
       

        [HttpPost, ActionName("DeleteUsers")]
        public ActionResult DeleteUsers(int id)
        {
            var person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("UsersPage");
        }

        public ActionResult PostsPage()
        {
            Database.SetInitializer<ProductMangerContext>(null);

            PostesImagesView postesImages = new PostesImagesView();
            postesImages.articles = db.articles.ToList().Where(x => x.IfAproveed == true);
            postesImages.photos = db.photos.ToList();
            return View(postesImages);
        }

        public ActionResult PostsRequests()
        {
            Database.SetInitializer<ProductMangerContext>(null);

            PostesImagesView postesImages = new PostesImagesView();
            postesImages.articles = db.articles.ToList().Where(x => x.IfAproveed == false);
            postesImages.photos = db.photos.ToList();
            return View(postesImages);
        }

        public ActionResult DeletePost(int id)
        {
            Article article = db.articles.SingleOrDefault(x => x.Id == id);
            db.articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("PostsPage");

        }

        public ActionResult Approve(int id)
        {
            Article art = db.articles.SingleOrDefault(x => x.Id == id);
            Article article = new Article();
            article = art;
            article.IfAproveed = true;
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("PostsRequests");

        }

        public ActionResult EditPost(int id)
        {

            
            NewPostsView postesImages = new NewPostsView();
            var articles = db.articles.Single(x => x.Id == id);
            var photos = db.photos.Single(x =>x.Aritcle_Id == id);
            postesImages.ArticleBody = articles.ArticleBody;
            postesImages.ArticleTitle = articles.ArticleTitle;
            postesImages.ArticleType = articles.ArticleType;
            postesImages.Id = articles.Id;
            postesImages.image = photos.Path;
                
                
             return View(postesImages);
        }

        [HttpPost]
        public ActionResult EditPost(NewPostsView post, HttpPostedFileBase file)
        {
            Article articles = new Article();
            Photo Images = new Photo();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);

                    file.SaveAs(path);
                    post.image = pic;

                }
                var article = db.articles.Single(x => x.Id == post.Id);
                Photo Image = db.photos.SingleOrDefault(x => x.Aritcle_Id == post.Id);
                articles = article;
                articles.ArticleBody = post.ArticleBody;
                articles.ArticleTitle = post.ArticleTitle; 
                articles.ArticleType =  post.ArticleType; 
                if (Image == null) {

                    Images.Aritcle_Id = post.Id;
                    Images.Path = post.image;
                    Images.Title = post.ArticleTitle;
                    db.photos.Add(Images);
                    db.SaveChanges();
                    db.Entry(Images).State = EntityState.Modified;
                } else
                {
                    Images = Image;
                    Images.Path = post.image;
                    db.Entry(articles).State = EntityState.Modified;
                    db.Entry(Images).State = EntityState.Modified;
                }
               
                db.SaveChanges();
                return RedirectToAction("PostsPage");
            }
            return View(post);
        }
       

    }
}