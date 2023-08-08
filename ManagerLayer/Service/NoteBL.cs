using CommonLayer.Model;
using CommonLayer.Models;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class NoteBL: INoteBL
    {
        private readonly INotesRL noteInterfaceRL;

        public NoteBL(INotesRL noteInterfaceRL)
        {
            this.noteInterfaceRL = noteInterfaceRL;
        }

        public NoteTable AddNewNote(NotesModel notesModel, long UserId)
        {
            try
            {
                return this.noteInterfaceRL.AddNewNote(notesModel, UserId);
                
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
                return this.noteInterfaceRL.GetAllNotes(UserId);
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
                return this.noteInterfaceRL.UpdateColor(NoteId, color);
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
                return this.noteInterfaceRL.UpdateNotes(NoteId, UserId, notesModel);
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
                return this.noteInterfaceRL.IsPinOrNot(NoteId);
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
                return this.noteInterfaceRL.IsArchiveOrNot(NoteId);
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
                return this.noteInterfaceRL.IsTrashOrNot(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool DeleteNote( long NoteId)
        {
            try
            {
                return this.noteInterfaceRL.DeleteNote( NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public string UploadImage(long NoteId, long UserId, IFormFile img)
        {
            try
            {
                return this.noteInterfaceRL.UploadImage(NoteId, UserId, img);
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
                return this.noteInterfaceRL.searchNote(query);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetNoteCount(int userId)
        {
            try
            {
                return this.noteInterfaceRL.GetNoteCount(userId);
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
                return this.noteInterfaceRL.ColorNoteCount(userId);
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
                return this.noteInterfaceRL.CountTrashNote(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CountModel NoteAllCount(int userId)
        {
            try
            {
                return this.noteInterfaceRL.NoteAllCount(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
