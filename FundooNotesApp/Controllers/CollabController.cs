using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using RepoLayer.Entity;

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
                var UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
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
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [HttpPut]
        [Route("RetrieveCollab")]
        public IActionResult GetAllCollab(long NoteId)
        {
            try
            {

                var result = IcollabBL.GetAllCollab(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting All the collab", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to get the collab" });

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    

        [HttpDelete]
        [Route("DeleteCollab")]
        public IActionResult RemoveCollab(long CollabId)
        {
            try
            {

                var result = IcollabBL.RemoveCollab(CollabId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Collab Deleted successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Collab Deletion Failed" });

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
