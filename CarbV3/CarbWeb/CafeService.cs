using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbWeb
{
    public class CafeService : Service
    {
        public object Post(Cafe request)
        {
            var message = this.GetSession().DisplayName;
            return new CafeResponse
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                OpenTime = request.CloseTime,
                CloseTime = request.CloseTime,
                PriceRange = request.PriceRange,
                Rating = request.Rating,
                Type = request.Type,
                ZipCode = request.ZipCode,
                Message = message
            };
        }
    }
}