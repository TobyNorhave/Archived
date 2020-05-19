using System.Collections.Generic;
using System.ServiceModel;
using Controller;
using Model;

namespace Services
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class BookingService : IBookingService
    {
        private BookingCtr _bookingCtr;

        public BookingService()
        {
            _bookingCtr = new BookingCtr();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void BookTable(Booking booking)
        {
            _bookingCtr.Create(booking);
        }

        public List<Booking> GetAllCustomersOnBooking(Customer customer)
        {
            return _bookingCtr.GetAllCustomersOnBooking(customer);
        }
    }
}
