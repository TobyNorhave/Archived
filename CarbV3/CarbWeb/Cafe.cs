using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbWeb
{
    [Route("/cafe", "Post")]
    [Authenticate]
    [RequiredRole("Customer")]
    [RequiredPermission("GetCafe")]
    [RecordIpFilter]
    public class Cafe : IReturn<CafeResponse>
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Type { get; set; }
        public decimal PriceRange { get; set; }
        public decimal Rating { get; set; }
    }

    [LastIpFilter]
    public class CafeResponse
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Type { get; set; }
        public decimal PriceRange { get; set; }
        public decimal Rating { get; set; }
        public string Message { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | City: {City} \n" +
                $"Address: {Address} | ZipCode: {ZipCode} \n" +
                $"OpenTime: {OpenTime} | CloseTime {CloseTime} \n" +
                $"Type: {Type} | PriceRange: {PriceRange} | Rating: {Rating} \n";
        }
    }
}