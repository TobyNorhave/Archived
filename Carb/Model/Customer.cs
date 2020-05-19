using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class Customer : Person
    {
        [DataMember]
        public bool VIP { get; set; }
    }
}
