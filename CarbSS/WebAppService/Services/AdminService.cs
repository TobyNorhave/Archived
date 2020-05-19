using System.Collections.Generic;
using Controller;
using Model;
using ServiceStack;
using WebAppService.Requests;

namespace WebAppService.Services
{
    public class AdminService : Service
    {
        public Administrator Post(AdminRequestPP request)
        {
            var personCtr = new PersonCtr();
            var adminData = new Administrator
            {
                FName = request.FName,
                LName = request.LName,
                PhoneNo = request.PhoneNo,
                Email = request.Email
            };
            personCtr.CreateAdmin(adminData);
            return adminData;
        }

        public Administrator Get(GetAdminByIDRequest request)
        {
            var personCtr = new PersonCtr();
            return personCtr.GetAdminByID(request.ID);
        }

        public List<Administrator> Get(GetAllAdminsRequest request)
        {
            var personCtr = new PersonCtr();
            return personCtr.GetAllAdmins();
        }

        public Administrator Put(AdminRequestPP request)
        {
            var personCtr = new PersonCtr();
            var adminData = new Administrator
            {
                ID = request.ID,
                FName = request.FName,
                LName = request.LName,
                PhoneNo = request.PhoneNo,
                Email = request.Email
            };
            personCtr.UpdateAdmin(adminData);
            return adminData;
        }

        public void Delete(DeleteAdminRequest request)
        {
            var personCtr = new PersonCtr();
            personCtr.Delete(request.ID);
        }

    }
}