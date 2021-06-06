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
    public class FactoryController : Controller
    {
        ProductMangerContext db = new ProductMangerContext();
        // GET: Factory
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



        public ActionResult MyPostes(int id)
        {
            Database.SetInitializer<ProductMangerContext>(null);

            PostesImagesView postesImages = new PostesImagesView();
            postesImages.articles = db.articles.ToList().Where(x => x.EditorId == id);
            postesImages.photos = db.photos.ToList();
            return View(postesImages);
        }


        public ActionResult CreatePostes()
        {
            NewPostsView post = new NewPostsView();
            
            return View(post);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreatePostes(NewPostsView post, HttpPostedFileBase file)
        {
            var lastpost = db.articles.ToList();
            Photo Images = new Photo();
            Article article = new Article();
            int lastArtId = 0; 
            
            if (ModelState.IsValid)
            {
                if (Session["user"] != null)
                {
                    int userid = int.Parse(((Person)Session["user"]).Id.ToString());
                    article.EditorId = userid;
                    article.ArticleBody = post.ArticleBody;
                    article.ArticleTitle = post.ArticleTitle;
                    article.ArticleType = post.ArticleType;
                    DateTime localDate = DateTime.Now;
                    article.CreationDate = localDate.ToString();
                    article.IfAproveed = false;
                    article.NumberOfDislikes = 0;
                    article.NumberOfLikes = 0;
                    article.NumberOfViewers = 1;
                    db.articles.Add(article);
                    db.SaveChanges();

                }
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);

                    file.SaveAs(path);
                    post.image = pic;
                    Images.Path = pic;
                }
                if (lastpost != null) {
                    foreach (var item in lastpost)
                    {
                        lastArtId = item.Id;
                    }
                    lastArtId += 1;


                }
                else
                {
                    lastArtId = 1;
                }
                
                Images.Aritcle_Id = lastArtId;
                Images.Title = post.ArticleTitle;
                db.photos.Add(Images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            
            return View(post);
        }

       public ActionResult ReceivedQuestions(int id)
        {
           IEnumerable<Questions> questions = db.questions.ToList().Where(x => x.Editor_Id == id && x.IsAnswer == false);

            return View(questions);
        }


        [HttpGet]
        public ActionResult Answer(int id, String answer)
        {
            if (answer != "")
            {

                if (ModelState.IsValid)
                {
                    Questions questions = db.questions.Single(x => x.ID == id);
                    int userId = int.Parse(((Person)Session["user"]).Id.ToString());
                    if (questions!= null) {
                        questions.IsAnswer = true;
                        questions.Editor_Id = userId;
                        questions.Answer = answer;
                        db.Entry(questions).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);

                    }
                    
                    
                    
                }

            }
            return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }
    }
}