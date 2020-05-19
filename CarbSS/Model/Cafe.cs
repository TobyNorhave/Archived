using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    [DataContract]
    public class Cafe
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public decimal PriceRange { get; set; }

        [DataMember]
        public decimal Rating { get; set; }

        [DataMember]
        public DateTime OpenTime { get; set; }

        [DataMember]
        public DateTime CloseTime { get; set; }

        [DataMember]
        public Administrator Admin { get; set; }

        [DataMember]
        public int ZipID { get; set; }

        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public List<Table> Tables { get; set; }

        public Cafe()
        {
            Tables = new List<Table>();
        }
    }
}
