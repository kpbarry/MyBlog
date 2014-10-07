using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// For using path
using System.IO;
// For using membership features
using System.Web.Security;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        // Set up data context
        Models.BlogEntities db = new Models.BlogEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Step 3. Add a httpPostedFileBase parameter
        [HttpPost]
        public ActionResult Register(Models.Registration reg, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                // Save the image to our site
                string filename = Guid.NewGuid().ToString().Substring(0, 6) + Image.FileName;
                // Specify the path to save the file to
                string path = Path.Combine(Server.MapPath("~/content/"), filename);
                // Save the file
                Image.SaveAs(path);
                // Update registration object with the Image
                reg.Image = "/content/" + filename;
            }
            // Create our Membership user
            Membership.CreateUser(reg.Username, reg.Password);
            // Create our author object
            Models.Author author = new Models.Author();
            author.Name = reg.Name;
            author.ImageUrl = reg.Image;
            author.Username = reg.Username;

            // Add author to db
            db.Authors.Add(author);
            db.SaveChanges();

            // Log in the user
            FormsAuthentication.SetAuthCookie(reg.Username, false);
            return RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Login login)
        {
            // Check if valid user
            if(Membership.ValidateUser(login.Username, login.Password))
            {
                // Credentials are gold, log them in
                FormsAuthentication.SetAuthCookie(login.Username, false);
                return RedirectToAction("Index", "Post");
            }
            else
            {
                //Bad pw or username
                ViewBag.ErrorMessage = "Invalid username and/or password";
                return View(login);
            }
        }
    }
}
