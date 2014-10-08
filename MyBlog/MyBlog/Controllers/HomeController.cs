using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        BlogEntities db = new BlogEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Posts.OrderByDescending(x=> x.DateCreated));
        }

        public ActionResult AddComment(Models.Comment commentToAdd)
        {
            //Make sure comment is fully filled out
            commentToAdd.DateCreated = DateTime.Now;
            
            //Add comment to database
            db.Comments.Add(commentToAdd);
            db.SaveChanges();

            //Replace with AJAX!!!!!!!!!!!!!!!!
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Likes()
        {
            Post post = new Post();
            return Content(post.Likes + "Likes");
        }
    }
}
