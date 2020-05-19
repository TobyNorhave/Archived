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
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult SearchBookings()
        {
            return View();
        }

        // GET: Booking/Details/5
        public ActionResult ViewBookings(int id)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);

            var bookings = client.Get(new BookingRequest { Id = id });
            return View(bookings.Result);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult CreateBooking(string date, string time, int noOfPeople, int cafeID)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            DateTime startDate = Convert.ToDateTime(date + " " + time);
            //int cafeID = ViewBag.CafeID;
            int userID = 1;
            try
            {
                var bookingColl = client.Post(new BookingRequest
                {
                    CafeId = cafeID,
                    UserId = userID,
                    NoOfPeople = noOfPeople,
                    StartDate = startDate
                });

                ViewBag.Date = date;
                ViewBag.People = noOfPeople;
                ViewBag.Time = time;

                return View();

            }
            catch (Exception)
            {
                ViewBag.Error = "Der er ingen ledige borde på valgte tidspunkt!";
                ViewBag.CafeID = cafeID;
                return View();
            }
        }

        public ActionResult DeleteBooking()
        {
            return View();
        }

        public ActionResult DeleteBookingConfirmation(int id)
        {
            JsonServiceClient client = new JsonServiceClient(ConfigurationManager.AppSettings["Client"]);
            client.Delete(new DeleteBookingRequest { Id = id });
            ViewBag.Delete = id;

            return View();
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
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

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
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
