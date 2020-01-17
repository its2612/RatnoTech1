using Ratno_Tech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ratno_Tech.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Product()
        {
            using(var db=new Ratno_TechEntities())
            {
                ProductModel details = new ProductModel();
                var productdetails = db.products.ToList();
                return View(productdetails);
            } 
        }


       
    }
}