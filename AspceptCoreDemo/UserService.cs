using System;
using AspectCore.DynamicProxy;

namespace AspceptCoreDemo
{
    public interface IUserService
    {
        [NonAspect]
        String GetUserName(ApiRequest req);
    }

    public class UserService : IUserService
    {
        public string GetUserName(ApiRequest req)
        {
            if (req == null)
                return null;

            Console.WriteLine($"The User Name is {req.Name}");
            return req.Name;
        }
    }
}
