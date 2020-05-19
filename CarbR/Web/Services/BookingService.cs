using ServiceStack.ServiceInterface;
using Database;
using Model;
using System.Collections.Generic;
using Web.Requests;
using System;

namespace Web.Services
{
    public class BookingService : Service
    {
        public object Post(BookingRequest request)
        {
            DbBooking dbBooking = new DbBooking();
            var bookingDate = DateTime.Now;
            var booking = new Booking
            {
                BookedDate = bookingDate,
                StartDate = request.StartDate.Value,
                EndDate = request.StartDate.Value.AddHours(2)
            };

            var bookingId = dbBooking.CreateBooking(request.CafeId.Value, request.UserId.Value, booking, request.NoOfPeople.Value);
            booking.Id = bookingId;
            return new BookingResponse { Result = new Booking[] { booking } };
        }

        public object Get(BookingRequest request)
        {
            DbBooking dbBooking = new DbBooking();
            if (request.Id.HasValue)
            {
                var booking = dbBooking.GetBookingById(request.Id.Value);
                return new BookingResponse
                {
                    Result = new Booking[] { booking }
                };
            }
            else
            {
                throw new Exception("Please enter a vaild BookingID");
            };
        }

        public object Get(GetBookingByUserIdRequest request)
        {
            DbBooking dbBooking = new DbBooking();
            if (request.UserId.HasValue)
            {
                var booking = dbBooking.GetBookingByUserID(request.UserId.Value);
                return new BookingResponse
                {
                    Result = booking
                };
            }
            else
            {
                throw new Exception("Please enter a vaild UserID");
            };
        }

        public object Get(GetAllCafeBookingsRequest request)
        {
            DbBooking dbBooking = new DbBooking();
            if (request.CafeId.HasValue)
            {
                var booking = dbBooking.GetAllCafeBookings(request.CafeId.Value);
                return new BookingResponse
                {
                    Result = booking
                };
            }
            else
            {
                throw new Exception("Please enter a vaild CafeID");
            };
        }

        public void Delete(DeleteBookingRequest request)
        {
            DbBooking dbBooking = new DbBooking();
            if (request.Id.HasValue)
            {
                dbBooking.Delete(request.Id.Value);
            }
            else
            {
                throw new Exception("Please enter a valid ID");
            }
        }
    }
}
