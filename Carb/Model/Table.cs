using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Table
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int NoOfSeats { get; set; }

        [DataMember]
        public bool Available { get; set; }

        [DataMember]
        public int TableNumber { get; set; }

        [DataMember]
        public Cafe Cafe { get; set; }

        [DataMember]
        public Booking Booking { get; set; }

        public override string ToString()
        {
            return $"ID: {ID} " +
                $"NoOfSeats: {NoOfSeats} " +
                $"Available: {Available} " +
                $"TableNumber: {TableNumber} ";
        }
    }
}
