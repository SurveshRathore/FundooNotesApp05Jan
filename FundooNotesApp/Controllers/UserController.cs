using CommonLayer.Model;
using Experimental.System.Messaging;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using StackExchange.Redis;
using System.Security.Claims;

namespace FundooNotesApp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController:ControllerBase
    {
        
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> _logger;

        public UserController( IUserBL userBl, ILogger<UserController> log)
        {

            this.userBL = userBl ;
            this._logger = log;
        }
        [HttpPost]
        [Route("api/Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.UserRegitration(userRegistration);
                if(result != null)
                {
                    return this.Ok(new { sucess = true, message = "User Registered Successfully.", result = result });
                    
                }
                else
                {
                    return this.BadRequest(new {sucess = false, message = "User already exists"});
                }
                
            }
            catch(Exception  ex)
            {
                return this.BadRequest(new { sucess = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Login")]
        public IActionResult userLogin(UserLogin ULogin)
        {
            try
            {
                var result = userBL.userLogin(ULogin);
                if (result != null)
                {
                    
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string FirstName = database.StringGet("FirstName");
                    string LastName = database.StringGet("LastName");
                    long UserId = Convert.ToInt32(database.StringGet("UserId"));
                    this._logger.LogInformation(FirstName + " Is LoggerIn");

                    UserTable user = new UserTable
                    {
                        userId = UserId,
                        FirstName = FirstName,
                        LastName = LastName,
                        EmailId = ULogin.EmailId
                    };
                    _logger.LogInformation("SuccessFully");
                    return this.Ok(new { sucess = true, message = "User Login Successfully.", result = result });
                }
                else
                {
                    _logger.LogWarning("UnSuccessFully");
                    return this.BadRequest(new { sucess = false, message = "Error" });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { sucess = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/ForgetPassword")]
        public IActionResult userForget( string email)
        {
            try
            {
                var result = userBL.userPasswordFoget(email);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Forget password mail send Successfully." });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Email not found" });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { sucess = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/ResetPassword")]
        public IActionResult userResetPass( string pass, string confirmPass)
        {
            try
            {
                string emailID = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.userResetPassword(emailID, pass, confirmPass);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Password changed Successfully.", result = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Password changing Failed" });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { sucess = false, message = ex.Message });
            }
        }

    }
}
