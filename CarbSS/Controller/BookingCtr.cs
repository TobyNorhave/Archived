using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class BookingCtr : IBooking<Customer, Cafe, Booking>
    {
        private BookingDb _bookingDb = new BookingDb();

        public void CreateBooking(Cafe cafe, Booking booking, int noOfPeople) => _bookingDb.CreateBooking(cafe, booking, noOfPeople);

        public Booking GetBookingByID(int ID) => _bookingDb.GetBookingByID(ID);

        public List<Booking> GetAllBookingsOnCustomerID(Customer customer) => _bookingDb.GetAllBookingsOnCustomerID(customer);

        public List<Booking> GetAllCafeBookings(Cafe cafe) => _bookingDb.GetAllCafeBookings(cafe);

        public void UpdateBooking(Booking booking) => _bookingDb.UpdateBooking(booking);

        public void Delete(int ID) => _bookingDb.Delete(ID);
    }
}
