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
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // GET: Account
        public ActionResult LoginUser(string userName, string PassWord)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);

            var users = client.Get(new AuthUser { UserName = userName, Password = PassWord });

            return View();
        }

        // GET: Account/Create
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult CreateUserRequest(FormCollection collection)
        {
            string fullName = Request.Form["Fullname"];
            string email = Request.Form["Email"];
            string userName = Request.Form["UserName"];
            string password = Request.Form["PassWord"];
            string phoneNo = Request.Form["PhoneNo"];

            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            var bookingColl = client.Post(new UserRequest
            {
                FullName = fullName,
                Email = email,
                UserName = userName,
                PhoneNo = phoneNo,
                Password = password
                
            });

            return RedirectToAction("UserCreated");
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);

            var users = client.Get(new UserRequest { Id = id });
            return View(users.Result);
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);

                client.Get(new DeleteUserRequest { Id = id });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
