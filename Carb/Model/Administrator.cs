using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Administrator : Person
    {
        [DataMember]
        public List<Cafe> cafes { get; set; }
    }
}
