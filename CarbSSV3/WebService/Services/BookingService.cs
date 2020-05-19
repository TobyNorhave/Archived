using Controller;
using Model;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using WebService.Requests;

namespace WebService.Services
{
    public class BookingService : Service
    {
        public Booking Post(CreateBookingRequest request)
        {
            var bookingCtr = new BookingCtr();
            var cafeData = new Cafe
            {
                ID = request.CafeID
            };
            var customerData = new Customer
            {
                ID = request.PersonID
            };
            var bookingData = new Booking
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Customer = customerData
            };
            bookingCtr.CreateBooking(cafeData, bookingData, request.NoOfPeople);
            return bookingData;
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
                ID = request.CafeID,
            };
            return bookingCtr.GetAllCafeBookings(cafeData);
        }

        public Booking Put(UpdateBookingRequest request)
        {
            var bookingCtr = new BookingCtr();
            var bookingData = new Booking
            {
                ID = request.ID,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            bookingCtr.UpdateBooking(bookingData);
            return bookingData;
        }

        public void Delete(DeleteBookingRequest request)
        {
            var bookingCtr = new BookingCtr();
            bookingCtr.Delete(request.ID);
        }
    }
}