using CommonLayer.Model;
using Experimental.System.Messaging;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTestController : ControllerBase
    {
        private readonly IOrderTestBL orderTestBL;
        private readonly FundooDBContext fundooDBContext;

        public OrderTestController(IOrderTestBL orderTestBL, FundooDBContext fundooDBContext)
        {
            this.orderTestBL = orderTestBL;
            this.fundooDBContext = fundooDBContext;
        }

        [HttpPost]
        [Route("addorder")]
        public IActionResult Addorder(OrderModel orderModel) {
            try
            {
                orderModel.UserID = Convert.ToInt32(User.Claims.FirstOrDefault(id => id.Type == "userID").Value);

                var order = this.orderTestBL.AddNewOrder(orderModel);
                if(order == null)
                {
                    return this.BadRequest(new { success = false, Message = "Failed to Add Order" });
                }
                else
                {
                    return this.Ok(new { success = true, Message = " Order Added Successfully", Response = order });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
