using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbWeb
{
    public class RecordIpFilter : RequestFilterAttribute
    {
        public ICacheClient Cache { get; set; }
        public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            Cache.Add("LastIP", req.UserHostAddress);
        }
    }

    public class LastIpFilter : ResponseFilterAttribute
    {
        public ICacheClient Cache { get; set; }
        public override void Execute(IHttpRequest req, IHttpResponse res, object responseDto)
        {
            var status = responseDto as CafeResponse;
            if(status != null)
            {
                status.Message += "Last IP: " + Cache.Get<string>("LastIP");
            }
        }
    }
}