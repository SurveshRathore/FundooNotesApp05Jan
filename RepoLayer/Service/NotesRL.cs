using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class NotesRL: INotesRL
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public NotesRL(FundooDBContext fundooDB, IConfiguration config)
        {
            this.fundooDBContext = fundooDB;
            this.configuration = config;
        }
        public NoteTable AddNewNote(NotesModel notesModel, long UserId)
        {
            try
            {
                NoteTable notes = new NoteTable();
                notes.NoteTitle = notesModel.NoteTitle;
                notes.NoteDesciption = notesModel.NoteDesciption;
                notes.NoteReminder = notesModel.NoteReminder;
                notes.NoteColor = notesModel.NoteColor;
                notes.NoteImage = notesModel.NoteImage;
                notes.NoteIsArchive = notesModel.NoteIsArchive;
                notes.NoteIsPin = notesModel.NoteIsPin;
                notes.NoteIsTrash = notesModel.NoteIsTrash;
                notes.userId = UserId;
                notes.NoteCreated = notesModel.NoteCreated;
                notes.NoteModified = notesModel.NoteModified;
                fundooDBContext.Notes.Add(notes);
                
                int result = fundooDBContext.SaveChanges();
                if (result != 0)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<NoteTable> GetAllNotes(long UserId)
        {
            try
            {
                var allnotes = fundooDBContext.Notes.Where(n => n.userId == UserId).ToList();
                if (allnotes != null)
                {
                    return allnotes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public NoteTable UpdateColor(long NoteId, string color)
        {
            try
            {
                //var updateColor = fundooDBContext.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.userId == UserId);
                var updateColor = fundooDBContext.Notes.FirstOrDefault(n => n.NoteId == NoteId);
                if (updateColor != null)
                {
                    updateColor.NoteColor = color;
                    updateColor.NoteModified = DateTime.Now;
                    fundooDBContext.SaveChanges();
                    return updateColor;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateNotes(long NoteId, long UserId, NotesModel notesModel)
        {
            try
            {
                //var updatenotes = fundooDBContext.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.userId == UserId);
                //if (updatenotes != null)
                //{
                //    if (notesModel.NoteTitle != null)
                //    {
                //        updatenotes.NoteTitle = notesModel.NoteTitle;
                //    }
                //    if (notesModel.NoteDesciption != null)
                //    {
                //        updatenotes.NoteDesciption = notesModel.NoteDesciption;
                //    }
                //    if(notesModel.NoteColor != null)
                //    {
                //        updatenotes.NoteColor = notesModel.NoteColor;
                //    }
                //    else
                //    {
                //        updatenotes.NoteColor = "";
                //    }
                //    if (notesModel.NoteImage != null)
                //    {
                //        updatenotes.NoteImage = notesModel.NoteImage;
                //    }
                //    else
                //    {
                //        updatenotes.NoteImage = "";
                //    }
                var updatenotes = fundooDBContext.Notes.FirstOrDefault(n => n.NoteId == NoteId);
                if(updatenotes != null)
                {
                    updatenotes.NoteTitle = notesModel.NoteTitle;
                    updatenotes.NoteDesciption = notesModel.NoteDesciption;
                    updatenotes.NoteColor = notesModel.NoteColor;
                    updatenotes.NoteImage = notesModel.NoteImage;
                    updatenotes.NoteReminder = notesModel.NoteReminder;
                    updatenotes.NoteModified = DateTime.Now;
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteNote( long NoteId)
        {
            try
            {
                var deletenotes = fundooDBContext.Notes.FirstOrDefault(n => n.NoteId == NoteId);
                if (deletenotes != null)
                {
                    fundooDBContext.Notes.Remove(deletenotes);
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsPinOrNot(long NoteId)
        {
            try
            {
                NoteTable result = this.fundooDBContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if (result.NoteIsPin == false)
                {
                    result.NoteIsPin = true;
                    fundooDBContext.SaveChanges();
                    return result.NoteIsPin;
                }
                else
                {
                    result.NoteIsPin = false;
                    fundooDBContext.SaveChanges();
                    return result.NoteIsPin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsArchiveOrNot(long NoteId)
        {
            try
            {
                NoteTable result = this.fundooDBContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if (result != null)
                {
                    result.NoteIsArchive = true;
                    fundooDBContext.SaveChanges();
                    return result.NoteIsArchive;
                }
                else
                {
                    //result.NoteIsArchive = true;
                    //return result.NoteIsArchive;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsTrashOrNot(long NoteId)
        {
            try
            {
                NoteTable result = fundooDBContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                if (result != null)
                {
                    //result.NoteIsTrash = !result.NoteIsTrash;
                    result.NoteIsTrash = true;
                    fundooDBContext.SaveChanges();
                    return result.NoteIsTrash;
                }
                else
                {
                    //result.NoteIsTrash = !result.NoteIsTrash;
                    //result.NoteIsTrash = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public NoteTable UpdateColor(long NoteId, string Color)
        //{
        //    try
        //    {
        //        var result = this.fundooDBContext.Notes.FirstOrDefault(e => e.NoteId == NoteId);
        //        if (result != null)
        //        {
        //            result.NoteColor = Color;
        //            fundooDBContext.SaveChanges();
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string UploadImage(long NoteId, long UserId, IFormFile img)
        {
            try
            {
                var filterUserNote = this.fundooDBContext.Notes.FirstOrDefault(e => e.userId == UserId && e.NoteId == NoteId );
                if (filterUserNote != null)
                {
                    //Account account = new Account("dzbxwvln6", "174237474712121", "qGE7PH1Pej_7CgZ_ldxkwjaSzAY");
                    Account account = new Account(
                        this.configuration["CloudinaryDetails:Name"],
                        this.configuration["CloudinaryDetails:APIKey"],
                        this.configuration["CloudinaryDetails:APISecretKey"]
                        );
                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadImage = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };

                    var imgUploadResult = cloudinary.Upload(uploadImage);
                    string imgPath = imgUploadResult.Url.ToString();

                    filterUserNote.NoteImage = imgPath;

                    fundooDBContext.SaveChanges();
                    return "Image uploaded Successfully";
                }
                else
                {
                    return "Failed to upload image.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteTable> searchNote(string query)
        {
            try
            {
                var fundooNotes = fundooDBContext.Notes.Where(notes => notes.NoteTitle.Contains(query)).ToList();

                if(fundooNotes != null)
                {
                    return fundooNotes;
                }
                else
                {
                    return null;
                }
               
            }
            catch(Exception)
            {
                throw;
            }
        }



        public int GetNoteCount(int userId)
        {
            try
            {
                int count = this.fundooDBContext.Notes.Where(u => u.userId == userId).Count();
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ColorNoteCount(int userId)
        {
            try
            {
                //int count = this.fundooDBContext.Notes.Where(u => u.NoteColor != "" ).Count();
                //int count = this.fundooDBContext.Notes.Where(u => u.NoteColor != "" && u.NoteColor != "NULL").Count();
                int count = this.fundooDBContext.Notes.Where(u => u.userId == userId && u.NoteColor != "" && u.NoteColor != null).Count();

                
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CountTrashNote(int userId)
        {
            try
            {
                int count = this.fundooDBContext.Notes.Where(u => u.userId == userId && u.NoteIsTrash == true).Count();

                return count;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public CountModel NoteAllCount(int userId)
        {
            CountModel countModel = new CountModel();
            countModel.NoteCount = this.fundooDBContext.Notes.Where(u => u.userId == userId).Count();
            countModel.TrashNoteCount = this.fundooDBContext.Notes.Where(u => u.userId == userId && u.NoteIsTrash == true).Count();
            countModel.ColorNoteCount = this.fundooDBContext.Notes.Where(u => u.userId == userId && u.NoteColor != null).Count();
            countModel.ArchiveCount = this.fundooDBContext.Notes.Where(u => u.userId == userId && u.NoteIsArchive != null).Count();

            return countModel;
        }

    }
}
