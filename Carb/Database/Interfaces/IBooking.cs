using System.Collections.Generic;

namespace Database
{
    public interface IBooking<Person, Booking>
    {
        void Create(Booking booking);
        Booking Get(int ID);
        void Update(Booking booking);
        void Delete(int ID);
        List<Booking> GetAllCustomersOnBooking(Person person);
    }
}
