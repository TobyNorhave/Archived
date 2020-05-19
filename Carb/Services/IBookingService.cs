using Model;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        void BookTable(Booking booking);

        [OperationContract]
        List<Booking> GetAllCustomersOnBooking(Customer customer);
    }
}
