using System;
using System.Collections.Generic;

namespace Model
{
    public class Cafe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal PriceRange { get; set; }
        public int Rating { get; set; }
        public string Type { get; set; }
        public string PhoneNo { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<CafeTable> Tables { get; set; }
        //public IEnumerable<Product> Products { get; set; }
    }
}
