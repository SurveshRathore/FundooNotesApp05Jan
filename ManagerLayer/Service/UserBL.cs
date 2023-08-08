using CommonLayer.Model;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Identity;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class UserBL: IUserBL
    {
        
        private readonly IUserRL userInterfaceRL;

        public UserBL (IUserRL userInterfaceRL)
        {
            this.userInterfaceRL = userInterfaceRL;
        }

      
        public UserTable UserRegitration(UserRegistration userRegistration)
        {
            try
            {
                return this.userInterfaceRL.UserRegitration(userRegistration);
                

            }
            catch (Exception)
            {
                throw;
            }

        }

        public string userLogin (UserLogin login)
        {
            try
            {
                return this.userInterfaceRL.userLogin(login);


            }
            catch (Exception)
            {
                throw;
            }

        }

        public string userPasswordFoget(string emailID)
        {
            try
            {
                return this.userInterfaceRL.userPasswordFoget(emailID);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool userResetPassword(string emailID, string password, string confirmPass)
        {
            try
            {
                return this.userInterfaceRL.userPasswordReset(emailID, password, confirmPass);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<UserTable> GetAllUser()
        {
            try
            {
                return this.userInterfaceRL.GetAllUser();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string userPasswordFogetByRabbitMQ (string emailID)
        {
            try
            {
                return this.userInterfaceRL.userPasswordFogetByRabbitMQ(emailID);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
