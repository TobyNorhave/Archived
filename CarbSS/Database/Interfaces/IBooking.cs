using System.Collections.Generic;

namespace Database
{
    public interface IBooking<Customer, Cafe, Booking>
    {
        void CreateBooking(Cafe cafe, Booking booking, int noOfPeople);
        Booking GetBookingByID(int ID);
        List<Booking> GetAllBookingsOnCustomerID(Customer customer);
        List<Booking> GetAllCafeBookings(Cafe cafe);
        void UpdateBooking(Booking booking);
        void Delete(int ID);
    }
}
