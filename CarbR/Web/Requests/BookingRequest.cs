using Model;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Requests
{
    [Route("/booking/{Id}","GET")]
    [Route("/booking", "POST")]
    public class BookingRequest : IReturn<BookingResponse>
    {
        public int? Id { get; set; }
        public DateTime? StartDate { get; set; }
        public int? UserId { get; set; }
        public int? NoOfPeople { get; set; }
        public int? CafeId { get; set; }
    }

    [Route("/booking/user/{UserId}", "GET")]
    public class GetBookingByUserIdRequest : IReturn<BookingResponse>
    {
        public int? UserId { get; set; }
    }

    [Route("/booking/cafe/{CafeId}", "GET")]
    public class GetAllCafeBookingsRequest : IReturn<BookingResponse>
    {
        public int? CafeId { get; set; }
    }

    [Route("/booking/delete/{Id}", "DELETE")]
    public class DeleteBookingRequest : IReturnVoid
    {
        public int? Id { get; set; }
    }

    public class BookingResponse
    {
        public IEnumerable<Booking> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
    }
}