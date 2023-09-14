using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class UserRL : IUserRL
    {

        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public UserRL(FundooDBContext fundooDB, IConfiguration config)
        {
            this.fundooDBContext = fundooDB;
            this.configuration = config;
        }
        public UserTable UserRegitration(UserRegistration userRegistration)
        {
            try
            {
                UserTable userTable = new UserTable();
                userTable.FirstName = userRegistration.FirstName;
                userTable.LastName = userRegistration.LastName;
                userTable.EmailId = userRegistration.EmailId;
                userTable.Password = EncryptPass(userRegistration.Password);

                fundooDBContext.UserTable.Add(userTable);
                int result = fundooDBContext.SaveChanges();

                if (result > 0)
                {
                    return userTable;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        //public string userLogin (UserLogin uLogin)
        //{
        //    try
        //    {
        //        UserTable userTable = new UserTable();
        //        var result = this.fundooDBContext.UserTable.FirstOrDefault(ud => ud.EmailId == uLogin.EmailId && ud.Password == EncryptPass(uLogin.Password));
        //        if(result != null)
        //        {
        //            var token = this.GenerateJwtToken(result.EmailId, result.userId);
        //            return token;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //        // var result = this.fundooDBContext.UserTable.FirstOrDefault(ud => ud.EmailId == uLogin.EmailId && ud.Password == EncryptPass(uLogin.Password));
        //        // userTable = this.fundooDBContext.UserTable.FirstOrDefault(ud=>ud.EmailId== uLogin.EmailId && ud.Password== EncryptPass(uLogin.Password));
        //        if (result != null)
        //        {
        //            //ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
        //            //IDatabase database = connectionMultiplexer.GetDatabase();
        //            //database.StringSet(key: "FirstName", userTable.FirstName);
        //            //database.StringSet(key: "LastName", userTable.LastName);
        //            //database.StringSet(key: "UserId", userTable.userId.ToString());

        //            string encrytPass = EncryptPass(uLogin.Password);
        //            var token = this.GenerateJwtToken(uLogin.EmailId, userTable.userId);
        //            return token;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public string userLogin(UserLogin uLogin)
        {
            try
            {
                UserTable userTable = new UserTable();
                userTable = this.fundooDBContext.UserTable.FirstOrDefault(ud => ud.EmailId == uLogin.EmailId && ud.Password == EncryptPass(uLogin.Password));
                if (userTable != null)
                {
                    var token = this.GenerateJwtToken(uLogin.EmailId, userTable.userId);
                    return token;
                }
                else
                {
                    return null;
                }

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

                var result = this.fundooDBContext.UserTable.FirstOrDefault(ue => ue.EmailId == emailID);
                if (result != null)
                {
                    var token = this.GenerateJwtToken(result.EmailId, result.userId);
                    userMSMQ userMSMQ = new userMSMQ();
                    userMSMQ.SendMail(token, result.EmailId, result.FirstName);
                    return token.ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool userPasswordReset(string emailID, string password, string confirmPassword)
        {
            try
            {

                if (password.Equals(confirmPassword))
                {
                    var user = fundooDBContext.UserTable.Where(ue => ue.EmailId == emailID).FirstOrDefault();
                    user.Password = EncryptPass(confirmPassword);
                    fundooDBContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateJwtToken(string emailID, long userID)
        {
            try
            {
                var userTokenHandler = new JwtSecurityTokenHandler();
                var userKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["Jwt:key"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, emailID),
                    new Claim("userID",userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),

                    SigningCredentials = new SigningCredentials(userKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = userTokenHandler.CreateToken(tokenDescriptor);
                return userTokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string EncryptPass(string pass)
        {
            try
            {
                byte[] passBytes = new byte[pass.Length];
                passBytes = Encoding.UTF8.GetBytes(pass);
                String encodePass = Convert.ToBase64String(passBytes);
                return encodePass;
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
                var allUser = this.fundooDBContext.UserTable.ToList();

                if (allUser != null)
                {
                    return allUser;
                }
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }


        string IUserRL.userPasswordFogetByRabbitMQ(string emailID)
        {
            throw new NotImplementedException();
        }

        //public async string userPasswordFogetByRabbitMQ(string emailID) {
        //    try
        //    {
        //        var result = fundooDBContext.UserTable.FirstOrDefault(ut => ut.EmailId == emailID);

        //        if (result != null)
        //        {
        //            RabbitMQ rabbitMQ = new RabbitMQ();
        //            var token = GenerateJwtToken(result.EmailId, result.userId);
        //            rabbitMQ.SendMail(emailID, token);

        //Uri uri = new Uri("rabbitmq://localhost/FundoNotesEmail_Queue");
        //var endPoint = await _bus.GetSendEndpoint(uri);
        //await endPoint.

        //     await endPoint.Send(forgotPasswordModel);
        //    return Ok(new ResponseModel<string> { Success = true, Message = "email send succesfull", Data = email });
        //}
        //return BadRequest(new ResponseModel<string>
        //{
        //    Success = true,
        //    Message = "email send succesfull",
        //    Data = email
        //});


        //        return 1;
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }

}
