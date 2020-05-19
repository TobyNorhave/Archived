using Controller;
using Model;
using ServiceStack;
using System.Collections.Generic;
using WebAppService.Requests;

namespace WebAppService.Services
{
    public class BookingService : Service
    {
        public Booking Post(BookingRequestPP request)
        {
            var bookingCtr = new BookingCtr();
            var cafeData = new Cafe
            {
                ID = request.ID
            };
            var bookingData = new Booking
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            bookingCtr.CreateBooking(cafeData, bookingData, request.NoOfPeople);
            return null;
        }

        public Booking Get(GetBookingByIDRequest request)
        {
            var bookingCtr = new BookingCtr();
            return bookingCtr.GetBookingByID(request.ID);
        }

        public List<Booking> Get(GetAllBookingsOnCustomerIDRequest request)
        {
            var bookingCtr = new BookingCtr();
            var personData = new Customer
            {
                ID = request.ID
            };
            return bookingCtr.GetAllBookingsOnCustomerID(personData);
        }

        public List<Booking> Get(GetAllCafeBookingsRequest request)
        {
            var bookingCtr = new BookingCtr();
            var cafeData =  new Cafe
            {
                ID = request.ID,
            };
            return bookingCtr.GetAllCafeBookings(cafeData);
        }

        public Booking Put(BookingRequestPP request)
        {
            var bookingCtr = new BookingCtr();
            var cafeData = new Cafe
            {
                ID = request.ID
            };
            var bookingData = new Booking
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            bookingCtr.CreateBooking(cafeData, bookingData, request.NoOfPeople);
            return null;
        }

        public void Delete(DeleteBookingRequest request)
        {
            var bookingCtr = new BookingCtr();
            bookingCtr.Delete(request.ID);
        }
    }
}