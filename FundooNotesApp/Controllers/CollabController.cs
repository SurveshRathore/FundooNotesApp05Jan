using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController:ControllerBase
    {
        private readonly ICollabBL IcollabBL;
        private readonly FundooDBContext fundooDBContext;

        public CollabController(FundooDBContext fundooContext, ICollabBL collabBL)
        {
            this.IcollabBL = collabBL;
            this.fundooDBContext = fundooContext;
        }

        [HttpPost]
        [Route("AddNewCollab")]
        public IActionResult AddNewCollab(long NoteId, string Email)
        {
            try
            {
                var UserId = Convert.ToInt32(this.User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = IcollabBL.AddNewCollab(UserId, NoteId, Email);
                if (result != null)
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

        [HttpPut]
        [Route("RetrieveCollab")]
        public IActionResult GetAllCollab(long NoteId)
        {
            try
            {

                var result = this.IcollabBL.GetAllCollab(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All the collab", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to get the collab" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteCollab")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {

                var result = this.IcollabBL.RemoveCollab(collabId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Collab Deleted successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Collab Deletion Failed" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
