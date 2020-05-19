using Controller;
using Model;
using ServiceStack;
using System.Collections.Generic;
using WebAppService.Requests;

namespace WebAppService.Services
{
    public class CafeService : Service
    {
        public Cafe Post(CafeRequestPP request)
        {
            var cafeCtr = new CafeCtr();
            var cafeData = new Cafe
            {
                Name = request.Name,
                Address = request.Address,
                PriceRange = request.PriceRange,
                Rating = request.PriceRange,
                OpenTime = request.OpenTime,
                CloseTime = request.CloseTime,
                ZipID = request.ZipID,
                TypeID = request.TypeID
            };
            cafeCtr.CreateCafe(cafeData);
            return cafeData;
        }

        public List<Cafe> Get(GetCafesByAdminIDRequest request)
        {
            var cafeCtr = new CafeCtr();
            return cafeCtr.GetCafesByAdminID(request.ID);
        }

        public Cafe Get(GetCafeByIDRequest request)
        {
            var cafeCtr = new CafeCtr();
            return cafeCtr.GetCafeByID(request.ID);
        }

        public List<Cafe> Get(GetAllCafesRequest request)
        {
            var cafeCtr = new CafeCtr();
            return cafeCtr.GetAllCafes();
        }

        public List<Cafe> Get(GetAllCafesByZipeRequest request)
        {
            var cafeCtr = new CafeCtr();
            return cafeCtr.GetAllCafesByZip(request.ZipID);
        }

        public Cafe Put(CafeRequestPP request)
        {
            var cafeCtr = new CafeCtr();
            var cafeData = new Cafe
            {
                ID = request.ID,
                Name = request.Name,
                Address = request.Address,
                PriceRange = request.PriceRange,
                Rating = request.PriceRange,
                OpenTime = request.OpenTime,
                CloseTime = request.CloseTime,
                ZipID = request.ZipID,
                TypeID = request.TypeID
            };
            cafeCtr.CreateCafe(cafeData);
            return cafeData;
        }

        public void Delete(DeleteCafeRequest request)
        {
            var cafeCtr = new CafeCtr();
            cafeCtr.Delete(request.ID);
        }
    }
}