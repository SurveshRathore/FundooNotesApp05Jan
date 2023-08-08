using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IUserBL
    {
        public UserTable UserRegitration(UserRegistration userRegistration);
        public string userLogin(UserLogin login);

        public string userPasswordFoget(string emailID);
        public bool userResetPassword(string emailID, string password, string confirmPass);

        public string userPasswordFogetByRabbitMQ(string emailID);

        public List<UserTable> GetAllUser();
    }
}
