using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
