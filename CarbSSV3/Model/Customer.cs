using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    [DataContract]
    public class Customer : Person
    {
        [DataMember]
        public bool VIP { get; set; }
    }
}
