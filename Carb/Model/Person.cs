﻿

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string FName { get; set; }

        [DataMember]
        public string LName { get; set; }

        [DataMember]
        public string PhoneNo { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public List<Booking> Bookings { get; set; }

        public Person()
        {
            Bookings = new List<Booking>();
        }

        public override string ToString()
        {
            return
                $"ID: {ID}" +
                $"{FName} " +
                $"{LName} " +
                $"{PhoneNo}" +
                $"{Email}";
        }
    }
}
