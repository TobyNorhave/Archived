using CafeBooking.Model;
using Database;
using System.Collections.Generic;
using Database.Interface;

namespace Controller
{
    public class BookingCtr : ICRUD<Booking>
    {
        private BookingDb _bookingDb = new BookingDb();
        public void Create(Booking entity)
        {
            _bookingDb.Create(entity);
        }

        public void Delete(int ID)
        {
            _bookingDb.Delete(ID);
        }

        public Booking Get(int ID)
        {
            return _bookingDb.Get(ID);
        }

        public IEnumerable<Booking> GetAll()
        {
            return _bookingDb.GetAll();
        }

        public void Update(int ID)
        {
            _bookingDb.Update(ID);
        }
    }
}
