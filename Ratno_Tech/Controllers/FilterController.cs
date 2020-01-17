using Ratno_Tech.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ratno_Tech.Controllers
{
    public class FilterController : Controller
    {
        // GET: Filter
        [Authorize]
        public ActionResult Index(string SortOrder,string name,string price,string details)
        {
            ViewBag.name = string.IsNullOrEmpty(SortOrder) ? "Name_desc" : "";
            ViewBag.price = string.IsNullOrEmpty(SortOrder) ? "Price_desc" : "";
            ViewBag.details = string.IsNullOrEmpty(SortOrder) ? "details_desc" : "";
            var db = new Ratno_TechEntities();

            var rawdata = (from s in db.products select s).ToList();

            var p = from s in rawdata select s;


            if (!String.IsNullOrEmpty(name))
            {
                p = p.Where(s => s.name.Trim().Equals(name.Trim()));
            }


            //if (!String.IsNullOrEmpty(price))
            //{
            //    p = p.Where(s => s.price.Equals(price.Trim()));
            //}

            if (!String.IsNullOrEmpty(details))
            {
                p = p.Where(s => s.details.Trim().Equals(details.Trim()));
            }


            var Uniquename = from s in p
                             group s by s.name into newGroup
                             where newGroup.Key != null
                             orderby newGroup.Key
                             select new { name = newGroup.Key };
            ViewBag.Uniquename = Uniquename.Select(m => new SelectListItem { Value = m.name, Text = m.name }).ToList();


            //var Uniqueprice = from s in db.products
            //                  group s by s.price into newGroup
            //                  where newGroup.Key = true
            //                  orderby newGroup.Key
            //                  select new { price = newGroup.Key };
            //ViewBag.Uniqueprice = Uniqueprice.Select(m => new SelectListItem { Value = m.name, Text = m.name }).ToList();


            var Uniquedetails = from s in p
                             group s by s.details into newGroup
                             where newGroup.Key != null
                             orderby newGroup.Key
                             select new { details = newGroup.Key };
            ViewBag.Uniquedetails = Uniquedetails.Select(m => new SelectListItem { Value = m.details, Text = m.details }).ToList();



            ViewBag.Selectname = name;
            ViewBag.Selectdetails = details;



            switch (SortOrder)
            {
                case "Name_desc":
                    p = p.OrderByDescending(s => s.name);
                    break;
                case "Price_desc":
                    p = p.OrderByDescending(s => s.price);
                    break;

                case "price":
                    p = p.OrderBy(s => s.price);
                    break;

                case "details_desc":
                    p = p.OrderByDescending(s => s.details);
                    break;

                case "details":
                    p = p.OrderBy(s => s.details);
                    break;

                default:

                    p=p.OrderBy(s => s.name);
                    break;

            }

            return View(p);
        }

        // GET: Filter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        

        // GET: Filter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Filter/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filter/Delete/5
        public ActionResult Delete(int id)
        {
            var db = new Ratno_TechEntities();

            product p = db.products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            db.products.Remove(p);
            db.SaveChanges();
           
            return RedirectToAction("Index");
        }

       


    }
}
