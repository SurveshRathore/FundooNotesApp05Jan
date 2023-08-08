using CommonLayer.Model;
using CommonLayer.Models;
using Experimental.System.Messaging;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog.Targets;
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
                catch (Exception)
                {
                    throw;
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

        [HttpPut("updateColor")]
        public ActionResult UpdateColor(long NoteId, string color)
        {
            try
            {
                //long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = InodeBL.UpdateColor(NoteId, color);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Updating color in Note", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Update color in Note" });
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }


        [HttpDelete("DeleteNote")]
        public ActionResult DeleteNote(long NoteId)
        {
            try
            {
                //long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = InodeBL.DeleteNote(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable To Delete Note" });
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("searchNote")]
        public IActionResult searchNote(String query)
        {
            try
            {
                var result = this.InodeBL.searchNote(query);

                if(result != null)
                {
                    return this.Ok(new { Success = true, Message = "Note found", result = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Note not found." });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("NoteCount")]
        public IActionResult NoteCount()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(id => id.Type == "userID").Value);
                var result = this.InodeBL.GetNoteCount(userid);

                if(result > 0)
                {
                    return this.Ok(new { Success = true, Message = "Successfully Count", result = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Failed to count" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("ColorNoteCount")]
        public IActionResult ColoredNoteCount()
        {
            int userid = Convert.ToInt32(User.Claims.FirstOrDefault(nd => nd.Type == "userID").Value);

            int result = this.InodeBL.ColorNoteCount(userid);

            if(result > 0)
            {
                return this.Ok(new { Success = true, Message = "Notes Count successfully", result = result });
            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "Unable to count Notes"});
            }
        }

        [HttpGet("trashCount")]
        public IActionResult TrashNoteCount()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(ui => ui.Type == "userID").Value);

            int result = this.InodeBL.CountTrashNote(userId);

            if(result > 0)
            {
                return this.Ok(new ResponseModel<int> { Status = true, Message = "Trash note Count successfully", Data = result }); 
            }
            else
            {
                return this.BadRequest(new ResponseModel<int> { Status = false, Message = "Failed", Data = result });
            }
        }

        [HttpGet("AllCount")]
        public IActionResult DisplyAllCount()
        {
            int userid = Convert.ToInt32(User.Claims.FirstOrDefault(u=>u.Type == "userID").Value);
            var result = this.InodeBL.NoteAllCount(userid);

            if(result !=null)
            {
                return this.Ok(new ResponseModel<CountModel> { Status = true, Message = "Count all ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<CountModel> { Status = false, Message = "failed", Data = result });
            }
        }

        //public IActionResult Count ()
        //{
        //    int userid = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "userID").Value);


            
        //    var obj = new CountModel
        //    {
        //        obj.NoteCount = this.InodeBL.GetNoteCount(userid),

        //        "Count of notes" = this.InodeBL.GetNoteCount(userid),

        //    }
        //}

    }
}
