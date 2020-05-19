using Database;
using Model;
using ServiceStack.ServiceInterface;
using System;
using Web.Requests;

namespace Web.Services
{
    public class CafeService : Service
    {
        public object Post(CafeRequest request)
        {
            DbCafe dbCafe = new DbCafe();

            var cafe = new Cafe
            {
                Name = request.Name,
                Zip = request.Zip,
                Address = request.Address,
                OpenTime = request.OpenTime,
                CloseTime = request.CloseTime,
                Type = request.Type,
                PhoneNo = request.PhoneNo,
                Description = request.Description
            };
            var cafeId = dbCafe.CreateCafe(cafe, request.UserId.Value);
            cafe.Id = cafeId;
            return new CafeResponse { Result = new Cafe[] { cafe } };
        }

        public object Get(CafeRequest request)
        {
            DbCafe dbCafe = new DbCafe();

            if (request.Id.Value == -1)
            {

                return new CafeResponse { Result = dbCafe.GetAllCafes() };
            }
            else
            {
                var cafe = dbCafe.GetCafeById(request.Id.Value);
                return new CafeResponse
                {
                    Result = new Cafe[] { cafe }
                };
            }
        }

        public object Get(GetCafesByUserId request)
        {
            DbCafe dbCafe = new DbCafe();
            if (request.UserId.HasValue)
            {
                return new CafeResponse
                {
                    Result = dbCafe.GetCafesByUserId(request.UserId.Value)
                };
            }
            else
            {
                throw new Exception("Please enter a vaild UserID!");
            }
        }

        public object Get(GetCafeByZipRequest request)
        {
            DbCafe dbCafe = new DbCafe();

            if (request.Zip.Length <= 4)
            {
                return new CafeResponse { Result = dbCafe.GetAllCafesByZip(request.Zip) };
            }
            else
            {
                return new CafeResponse { Result = dbCafe.GetAllCafes() };
            }
        }


        public object Put(CafeRequest request)
        {
            DbCafe dbCafe = new DbCafe();
            if (request.Id.HasValue)
            {
                var cafe = new Cafe
                {
                    Id = request.Id.Value,
                    Name = request.Name,
                    Zip = request.Zip,
                    Address = request.Address,
                    OpenTime = request.OpenTime,
                    CloseTime = request.CloseTime,
                    Type = request.Type,
                    PhoneNo = request.PhoneNo,
                    Description = request.Description
                };
                dbCafe.UpdateCafe(cafe);
                var upCafe = dbCafe.GetCafeById(cafe.Id);
                return new CafeResponse
                {
                    Result = new Cafe[] { upCafe }
                };
            }
            else
            {
                throw new Exception("Please enter a vaild ID on the cafe!");
            }

        }
        public void Delete(DeleteCafeRequest request)
        {
            DbCafe dbCafe = new DbCafe();

            dbCafe.Delete(request.Id.Value);
        }
    }
}