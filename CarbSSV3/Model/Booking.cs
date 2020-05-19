using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    [DataContract]
    public class Booking
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public Customer Customer { get; set; }

        [DataMember]
        public List<Table> Tables { get; set; }

        [DataMember]
        public int NoOfPeople { get; set; }

        public Booking()
        {
            Tables = new List<Table>();
        }
    }
}
