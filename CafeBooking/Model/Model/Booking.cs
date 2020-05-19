using System;

namespace CafeBooking.Model
{
    public class Booking
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public Person Person { get; set; }
        public Table Table { get; set; }

    }
}
