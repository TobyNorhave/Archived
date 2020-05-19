using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    [DataContract]
    public class Administrator : Person
    {
        [DataMember]
        public List<Cafe> Cafes { get; set; }

        public Administrator()
        {
            Cafes = new List<Cafe>();
        }
    }
}
