using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Booking
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime BookingDate { get; set; }

        [DataMember]
        public Customer Customer { get; set; }

        [DataMember]
        public List<Table> Tables { get; set; }

        public Booking()
        {
            Tables = new List<Table>();
        }
    }
}
