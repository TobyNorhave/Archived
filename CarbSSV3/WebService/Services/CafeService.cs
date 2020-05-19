using Controller;
using Model;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;
using WebService.Requests;

namespace WebService.Services
{
    public class CafeService : Service
    {
        public Cafe Post(CreateCafeRequest request)
        {
            var cafeCtr = new CafeCtr();
            var adminData = new Administrator { ID = request.AdminID };
            var cafeData = new Cafe
            {
                Name = request.Name,
                Address = request.Address,
                PriceRange = request.PriceRange,
                Rating = request.PriceRange,
                OpenTime = request.OpenTime,
                CloseTime = request.CloseTime,
                ZipID = request.ZipID,
                TypeID = request.TypeID,
                Admin = adminData
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

        public List<Cafe> Get(GetAllCafesByZipRequest request)
        {
            var cafeCtr = new CafeCtr();
            return cafeCtr.GetAllCafesByZip(request.ZipID);
        }

        public Cafe Put(UpdateCafeRequest request)
        {
            var cafeCtr = new CafeCtr();
            var adminData = new Administrator { ID = request.AdminID };
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
                TypeID = request.TypeID,
                Admin = adminData
            };
            cafeCtr.UpdateCafe(cafeData);
            return cafeData;
        }

        public void Delete(DeleteCafeRequest request)
        {
            var cafeCtr = new CafeCtr();
            cafeCtr.Delete(request.ID);
        }
    }
}