using Database;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class BookingCtr : IBooking<Customer, Booking>
    {
        private BookingDb _bookingDb = new BookingDb();

        public void Create(Booking booking)
        {
            _bookingDb.Create(booking);
        }

        public Booking Get(int ID)
        {
            return _bookingDb.Get(ID);
        }

        public void Update(Booking booking)
        {
            _bookingDb.Update(booking);
        }

        public void Delete(int ID)
        {
            _bookingDb.Delete(ID);
        }

        public List<Booking> GetAllCustomersOnBooking(Customer person)
        {
            return _bookingDb.GetAllCustomersOnBooking(person);
        }
    }
}
