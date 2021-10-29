using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Services
{
    public interface ICurrentUserService
    {
        //expose some proporties and methods that can be implemented by CurrentUserService class
        // that weill read user info from HttpContext
        //readonly 
        public int UserId { get;}
        public bool IsAuthenticated { get;}
        public string FullName { get;}
        public string Email { get;}
        public IEnumerable<string> Roles { get;}
        public bool IsAdmin { get;}

        public DateTime DateOfBirth { get; }
    }
}
