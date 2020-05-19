using Model;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;

namespace WebService.Requests
{
    [Route("/booking/create", Verbs = "POST")]
    public class CreateBookingRequest : IReturn<Booking>
    {
        public int CafeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PersonID { get; set; }
        public int NoOfPeople { get; set; }
    }

    [Route("/booking/{ID}", Verbs = "GET")]
    public class GetBookingByIDRequest : IReturn<Booking>
    {
        public int ID { get; set; }
    }

    [Route("/booking/customer/{ID}", Verbs = "GET")]
    public class GetAllBookingsOnCustomerIDRequest : IReturn<List<Booking>>
    {
        public int ID { get; set; }
    }

    [Route("/booking/cafe/{CafeID}", Verbs = "GET")]
    public class GetAllCafeBookingsRequest : IReturn<List<Booking>>
    {
        public int CafeID { get; set; }
    }

    [Route("/booking/update/{ID}", Verbs = "PUT")]
    public class UpdateBookingRequest : IReturn<Booking>
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    [Route("/booking/{ID}", Verbs = "DELETE")]
    public class DeleteBookingRequest : IReturnVoid
    {
        public int ID { get; set; }
    }
}