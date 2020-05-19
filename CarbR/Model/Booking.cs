using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }
        public Cafe Cafe { get; set; }
        public CafeTable Table { get; set; }
        //public Order Order { get; set; }
    }
}
