using ServiceStack;
using System;

namespace WebAppService.Requests
{
    [Route("/booking/update/{ID}", Verbs = "PUT")]
    [Route("/booking/create", Verbs = "POST")]
    public class BookingRequestPP
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfPeople { get; set; }
    }

    [Route("/booking/{ID}", Verbs = "GET")]
    public class GetBookingByIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/booking/customer/{ID}", Verbs = "GET")]
    public class GetAllBookingsOnCustomerIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/booking/cafe/{ID}", Verbs = "GET")]
    public class GetAllCafeBookingsRequest
    {
        public int ID { get; set; }
    }

    [Route("/booking/{ID}", Verbs = "DELETE")]
    public class DeleteBookingRequest
    {
        public int ID { get; set; }
    }
}