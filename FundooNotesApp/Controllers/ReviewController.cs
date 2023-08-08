using CommonLayer.Model;
using Experimental.System.Messaging;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ReviewController:ControllerBase
    {
        public IReviewBL reviewBL;

        public ReviewController(IReviewBL reviewBL)
        {
            this.reviewBL = reviewBL;
        }

        [HttpPost]
        [Route("api/AddReview")]
        public IActionResult AddNewReview(ReviewModel reviewModel)
        {
            try
            {
                var result = reviewBL.AddReview(reviewModel);
                if(result != null)
                {
                    return this.Ok(new { success = true, Message = "Review Added successfully", data = result }); 
                }
                else
                {

                    return this.BadRequest(new {success = false, Message = "Unable to add the review"}); 
                }
            }
            catch(Exception)
            {
                throw;
            }

        }
    }
}
