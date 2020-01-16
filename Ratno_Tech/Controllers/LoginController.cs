using Ratno_Tech.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ratno_Tech.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Index(Login user)
        {
            if (ModelState.IsValid)
            {
                using (Ratno_TechEntities db = new Ratno_TechEntities())
                {
                    var obj = db.AdminCredentials.Where(m => m.username.Equals(user.username) && m.password.Equals(user.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["adminID"] = obj.aid.ToString();
                        Session["UserName"] = obj.username.ToString();
                        FormsAuthentication.SetAuthCookie(obj.username, true);
                        return RedirectToAction("AdminDashBoard");
                    }
                }
            }
            return View(user);
        }
        [Authorize]
        public ActionResult AdminDashBoard()
        {
            if (Session["adminID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public ActionResult Product()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Product(ProductModel product)
        {
            if (product != null)
            {
                
                string FileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);

                
                string FileExtension = Path.GetExtension(product.ImageFile.FileName);

                
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;                 
                string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();               
                product.imagepath = UploadPath + FileName;
              
                product.ImageFile.SaveAs(product.imagepath);
     
                var db = new Ratno_TechEntities();

                product _member = new product();

                _member.imagepath = product.imagepath;
                _member.name = product.name;
                _member.details = product.details;
                _member.price = product.price;

                db.products.Add(_member);
                db.SaveChanges();
                ViewBag.Message = "Successfully Inserted";
                ModelState.Clear();
                return View();

            }
            return RedirectToAction("Index", "Login");
        }

        

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}