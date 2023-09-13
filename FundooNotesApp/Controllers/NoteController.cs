using CommonLayer.Model;
using CommonLayer.Models;
using Experimental.System.Messaging;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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

        // for radis
        private readonly IDistributedCache distributedCache;

        public NoteController(FundooDBContext fundooContext, INoteBL nodeBL, ILogger<NoteController> log, IDistributedCache distributedCache)
        {
            this.InodeBL = nodeBL;
            this.fundooDBContext = fundooContext;
            this.distributedCache = distributedCache;
            this._logger = log;
            this._logger.LogDebug("Nlog injected with the NoteController");
        }

        /// <summary>
        /// Add a new note.
        /// </summary>
        /// <param name="notesModel">model of note.</param>
        /// <returns>Note added or failed.</returns>
        [HttpPost]
        [Route("AddNewNotes")]
        public IActionResult AddNewNotes(NotesModel notesModel)
            {
                try
                {
                    // long UserId = Convert.ToInt32(U.Claims.FirstOrDefault(e => e.Type == "userId").Value);
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

        // [HttpGet("GetAllNotes")]
        // public IActionResult GetAllNotes()
        //    {
        //        try
        //        {
        //            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
        //            var result = InodeBL.GetAllNotes(UserId);

        //             if (result != null)
        //            {
        //                _logger.LogInformation("fetching the Notes");
        //                return this.Ok(new { success = true, message = "Successfully fetched the Notes", data = result });
        //            }
        //            else
        //            {
        //                _logger.LogInformation("Unable to fetch the Notes");
        //                return this.BadRequest(new { success = false, message = "Failed To Load Notes" });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        _logger.LogInformation(ex.ToString());
        //        throw ex;
        //        }
        // }


        // GetAllNotes method for radis cache
        [Authorize]
        [HttpGet]
        [Route("GetAllNotes")]
        public async Task <IActionResult> GetAllNotes()
        {
            try
            {
                var cacheKey = $"noteList_{User.FindFirst("userID").Value}";   // defining the key and value
                var serializedNotesList = await distributedCache.GetStringAsync(cacheKey);

                List<NoteTable> notesList;

                if(serializedNotesList != null)
                {
                    notesList = JsonConvert.DeserializeObject<List<NoteTable>>(serializedNotesList);
                }
                else
                {
                    long UserId = Convert.ToInt32(this.User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                    // var userIdClaim = User.FindFirst("UserId");
                    // if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
                    // {
                    notesList = this.InodeBL.GetAllNotes(UserId);
                    serializedNotesList = JsonConvert.SerializeObject(notesList);

                    await this.distributedCache.SetStringAsync(
                        cacheKey, 
                        serializedNotesList,
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                            SlidingExpiration = TimeSpan.FromMinutes(2),
                        });
                }
                if (notesList != null)
                {
                    this._logger.LogInformation("fetching the Notes");
                    return this.Ok(new { success = true, message = "Successfully fetched the Notes", data = notesList });
                }
                else
                {
                    this._logger.LogInformation("Unable to fetch the Notes");
                    return this.BadRequest(new { success = false, message = "Failed To Load Notes" });
                }



            }
            catch (Exception ex)
            {
                this._logger.LogInformation(ex.ToString());
                throw;
            }
        }

        [HttpPut("updateColor")]
        public ActionResult UpdateColor(long NoteId, string color)
        {
            try
            {
                // long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = this.InodeBL.UpdateColor(NoteId, color);
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
                // long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
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
                var result = this.InodeBL.IsPinOrNot(NoteId);
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
