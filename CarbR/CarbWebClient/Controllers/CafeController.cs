using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Requests;

namespace CarbWebClient.Controllers
{
    public class CafeController : Controller
    {
        // GET: Cafe
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cafe/Details/5
        public ActionResult ViewCafe(int id)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafeColl = client.Get(new CafeRequest { Id = id });
            return View(cafeColl.Result);
        }

        // GET: Cafe/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult SearchZip(string Search)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var cafes = client.Get(new GetCafeByZipRequest { Zip = Search });
            return View(cafes.Result);
        }
        public ActionResult ViewCafeError(int cafeID)
        {
            ViewBag.ErrorBooking = "Der skete en fejl under bookning";
            return View("ViewCafe/" + cafeID);
        }
        // POST: Cafe/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cafe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cafe/Edit/5
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

        // GET: Cafe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cafe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
