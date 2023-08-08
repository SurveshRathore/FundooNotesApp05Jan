namespace FundooNotesApp.Controllers
{
    using System.Security.Claims;
    using CommonLayer.Model;
    using Experimental.System.Messaging;
    using ManagerLayer.Interface;
    using Microsoft.AspNetCore.Mvc;
    using RepoLayer.Entity;
    using StackExchange.Redis;

    [Route("api/controller")]
    [ApiController]
    public class UserController : ControllerBase
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
            catch (Exception ex)
            {
                return this.BadRequest(new { sucess = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Login")]
        public IActionResult UserLogin(UserLogin ULogin)
        {
            try
            {
                // UserRegistration userData = this.userBL.userLogin(ULogin);
                var result = this.userBL.userLogin(ULogin);
                if (result != null)
                {
                    //UserTable userSessionData = new UserTable();
                    //SetSession(userSessionData);
                    //var name = HttpContext.Session.GetString("UserFullName");
                    //var id = HttpContext.Session.GetString("UserID");

                    //ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    //IDatabase database = connectionMultiplexer.GetDatabase();
                    //string FirstName = database.StringGet("FirstName");
                    //string LastName = database.StringGet("LastName");
                    //long UserId = Convert.ToInt32(database.StringGet("UserId"));

                    //UserTable user = new UserTable
                    //{
                    //    userId = UserId,
                    //    FirstName = FirstName,
                    //    LastName = LastName,
                    //    EmailId = ULogin.EmailId
                    //};



                    //this._logger.LogInformation(FirstName + " Is LogIn successfully");

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
        public IActionResult userForget ( string email)
        {
            try
            {
                var result = userBL.userPasswordFoget(email);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Forget password mail send Successfully." , response= result});
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

        //public void SetSession(UserTable userTable)
        //{
        //    HttpContext.Session.SetString("UserFullName", userTable.FirstName + " " + userTable.LastName);
        //    HttpContext.Session.SetString("UserEmail", userTable.EmailId);
        //    HttpContext.Session.SetInt32("UserID", Convert.ToInt32(userTable.userId));
        //}

        [HttpGet]
        [Route("/allUser")]
        public IActionResult GetAllUser()
        {
            try
            {
                var AllUser = this.userBL.GetAllUser();

                if (AllUser != null)
                {
                    return this.Ok(new { success = true, Message = "All User Fetched Successfully", Result = AllUser });
                }
                else
                    return this.BadRequest(new { success = false, Message = "Unable to fetch all user" });
            }
            catch
            {
                throw;
            }
        }

        //[HttpPost("RabbitMQ")]
        //public async Task <IActionResult> ForgetPassUsingRabbitMQ(String Email)
        //{
        //    try
        //    {
        //        var result = userBL.userPasswordFogetByRabbitMQ(Email);
              
        //        if (result != null)
        //        {
        //            return this.Ok(new { sucess = true, message = "Forget password mail send Successfully.", response = result });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { sucess = false, message = "Email not found" });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(new { sucess = false, message = ex.Message });
        //    }
        //}

    }
}
