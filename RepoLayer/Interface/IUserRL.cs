using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface IUserRL
    {
        public UserTable UserRegitration(UserRegistration userRegistration);
        public string userLogin(UserLogin uLogin);
        public string userPasswordFoget(string emailID);
        public bool userPasswordReset(string emailID, string password, string confirmPassword);
    }
}
