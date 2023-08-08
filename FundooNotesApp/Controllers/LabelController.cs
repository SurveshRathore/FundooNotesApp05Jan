using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController: ControllerBase
    {
        private readonly ILabelBL IlabelBL;
        private readonly FundooDBContext fundooDBContext;

        public LabelController(FundooDBContext fundooContext, ILabelBL labelBL)
        {
            this.IlabelBL = labelBL;
            this.fundooDBContext = fundooContext;
        }

        [HttpPost]
        [Route("AddNewLabel")]
        public IActionResult AddLabel(string LabelName, long NoteId)
        {
            try
            {
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = IlabelBL.AddNewLabel(LabelName, UserId, NoteId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "label Added successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "label Adding Failed" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetLabels(long LabelId)
        {
            try
            {

                var result = IlabelBL.GetAllLabels(LabelId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All the labels", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to get the labels" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel(long LabelId, string NewLabelName)
        {
            try
            {
                var result = IlabelBL.UpdateLabel(LabelId, NewLabelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "label Updated successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "label updation Failed" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(long LabelId)
        {
            try
            {
                var result = IlabelBL.DeleteLabel(LabelId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "label Deleted successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "label Deletion Failed" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
