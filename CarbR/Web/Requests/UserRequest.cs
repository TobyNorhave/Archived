using Model;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Requests
{
    [Route("/user/{Id}", "GET")]
    [Route("/user", "POST, PUT")]
    public class UserRequest : IReturn<UserResponse>
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
    }

    [Route("/user/auth", "GET")]
    public class AuthUser : IReturn<UserResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [Route("/user/delete/{Id}", "DELETE")]
    public class DeleteUserRequest : IReturnVoid
    {
        public int? Id { get; set; }
    }

    public class UserResponse
    {
        public IEnumerable<User> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Automatic exception handling
    }
}