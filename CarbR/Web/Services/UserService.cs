using Database;
using Model;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using System;
using Web.Requests;

namespace Web.Services
{
    public class UserService : Service
    {
        public object Post(UserRequest request)
        {
            DbUser dbUser = new DbUser();
            new SaltedHash().GetHashAndSaltString(request.Password, out string hash, out string salt);
            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNo = request.PhoneNo,
                Salt = salt,
                PasswordHash = hash
            };

            var userId = dbUser.CreateUser(user);
            user.Id = userId;
            return new UserResponse { Result = new User[] { user } };
        }

        public object Get(AuthUser request)
        {
            DbUser dbUser = new DbUser();
            var user = new User();
            if (request.UserName == null)
            {
                var userRes = dbUser.GetUserByUserName(request.UserName);
                if (new SaltedHash().VerifyHashString(request.Password, user.PasswordHash, user.Salt))
                {
                    user = userRes;
                }
                else
                {
                    throw new Exception("Please enter a vaild UserId");
                }
            }
            return new UserResponse { Result = new User[] { user } };
        }

        public object Put(UserRequest request)
        {
            DbUser dbUser = new DbUser();
            new SaltedHash().GetHashAndSaltString(request.Password, out string hash, out string salt);
            if (request.Id.HasValue)
            {
                var user = new User
                {
                    Id = request.Id.Value,
                    FullName = request.FullName,
                    Email = request.Email,
                    UserName = request.UserName,
                    PhoneNo = request.PhoneNo,
                    Salt = salt,
                    PasswordHash = hash
                };

                var userId = dbUser.CreateUser(user);
                user.Id = userId;
                return new UserResponse { Result = new User[] { user } };
            }
            else
            {
                throw new Exception("Please enter a vaild User ID");
            }
        }

        public void Delete(DeleteUserRequest request)
        {
            DbUser dbUser = new DbUser();
            dbUser.Delete(request.Id.Value);
        }
    }
}