using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using WebService.Requests;

namespace UnitTests
{
    [TestClass]
    public class BookingTest
    {
        [TestMethod]
        public void TestAllBookingMethods()
        {
            IRestClient client = new JsonServiceClient(ConfigurationManager.AppSettings["host"]);

            var startDate = DateTime.Now;
            var newBooking = client.Post(new CreateBookingRequest
            {
                CafeID = 1,
                StartDate = startDate,
                EndDate = startDate.AddMinutes(60),
                PersonID = 58,
                NoOfPeople = 2
            });

            List<Booking> bookingOnCustomer = client.Get(new GetAllBookingsOnCustomerIDRequest { ID = 58 });
            Assert.IsNotNull(bookingOnCustomer.Count);

            Booking bookingWithID = new Booking();

            foreach (var booking in bookingOnCustomer)
            {
                if(booking.StartDate == newBooking.StartDate)
                {
                    bookingWithID = booking;
                }
            }

            var bookingByID = client.Get(new GetBookingByIDRequest { ID =  bookingWithID.ID});
            Assert.IsNotNull(bookingByID.StartDate);

            List<Booking> allBookingsOnCafe = client.Get(new GetAllCafeBookingsRequest { CafeID = 1 });
            Assert.IsNotNull(allBookingsOnCafe.Count);

            newBooking.StartDate = DateTime.Now;
            client.Put(new UpdateBookingRequest
            {
                ID = bookingWithID.ID,
                StartDate = newBooking.StartDate,
                EndDate = newBooking.StartDate.AddMinutes(60),
            });

            Assert.AreNotSame(newBooking.StartDate, bookingWithID.StartDate);

            client.Delete(new DeleteBookingRequest { ID = bookingWithID.ID });
        }
    }
}
