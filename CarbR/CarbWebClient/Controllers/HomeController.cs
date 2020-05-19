using Model;
using ServiceStack.ServiceClient.Web;
using System;
using System.Configuration;
using System.Web.Mvc;
using Web.Requests;

namespace CarbWebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Carb";

            return View();
        }
        
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult IndexError()
        {
            ViewBag.Error = "Search must be valid";
            return View("Index");
        }


        public ActionResult ViewCafeNew(int id)
        {
            return View();
        }
    }
}