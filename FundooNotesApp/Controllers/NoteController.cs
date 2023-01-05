using CommonLayer.Models;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using RepoLayer.Entity;
using System.Security.Claims;

namespace FundooNotesApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL InodeBL;
        private readonly FundooDBContext fundooDBContext;
        private readonly ILogger<NoteController> _logger;
                      

        public NoteController(FundooDBContext fundooContext, INoteBL nodeBL, ILogger<NoteController> log)
        {
            this.InodeBL = nodeBL;
            this.fundooDBContext = fundooContext;
            this._logger = log;
            _logger.LogDebug("Nlog injected with the NoteController");
            }

            [HttpPost]
            [Route("AddNewNotes")]
            public IActionResult AddNewNotes(NotesModel notesModel)
            {
                try
                {
                //long UserId = Convert.ToInt32(U.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    var result = InodeBL.AddNewNote(notesModel, UserId);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, message = "Note Added", data = result });
                       
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = "Note Adding Failed" });
                    
                    }
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }
            [HttpGet("GetAllNotes")]
            public IActionResult GetAllNotes()
            {
                try
                {
                    long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    var result = InodeBL.GetAllNotes(UserId);
                
                 if (result != null)
                    {
                    _logger.LogInformation("fetching the Notes");
                    return this.Ok(new { success = true, message = "Successfully fetched the Notes", data = result });
                    }
                    else
                    {
                    _logger.LogInformation("Unable to fetch the Notes");
                    return this.BadRequest(new { success = false, message = "Failed To Load Notes" });
                        
                    }
                }
                catch (Exception ex)
                {
                _logger.LogInformation(ex.ToString());
                throw ex;
                }
            }

        [HttpPut("UpdateNotes")]
        public ActionResult UpdateNotes(long NoteId, NotesModel notesModel)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = InodeBL.UpdateNotes(NoteId, UserId, notesModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Updating Notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Update Notes" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete("DeleteNote")]
        public ActionResult DeleteNote(long NoteId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = InodeBL.DeleteNote(UserId, NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Delete Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsPin")]
        public ActionResult IsPin(long NoteId)
        {
            try
            {
                var result = InodeBL.IsPinOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Pinned Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Pin Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsArchive")]
        public ActionResult IsArchive(long NoteId)
        {
            try
            {
                var result = InodeBL.IsArchiveOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Archived Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Archive Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("IsTrash")]
        public ActionResult IsTrash(long NoteId)
        {
            try
            {
                var result = InodeBL.IsTrashOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Trashed Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Trash Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateColor")]
        public ActionResult UpdateColor(long NoteId, string Color)
        {
            try
            {
                var result = InodeBL.UpdateColor(NoteId, Color);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Color Updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Color Updation Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("UploadImages")]
        public IActionResult UploadImage(long NoteId, IFormFile img)
        {
                        
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = InodeBL.UploadImage(NoteId, UserId, img);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Image Uploaded Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Image Uplodation Failed" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
