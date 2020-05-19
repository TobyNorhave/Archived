using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    [DataContract]
    public class Table
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int NoOfSeats { get; set; }  

        [DataMember]
        public int TableNumber { get; set; }
    }
}
