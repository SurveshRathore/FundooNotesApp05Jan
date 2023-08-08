using CommonLayer.Model;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface INoteBL
    {
        public NoteTable AddNewNote(NotesModel notesModel, long UserId);
        public List<NoteTable> GetAllNotes(long UserId);

        public bool UpdateNotes(long NoteId, long UserId, NotesModel notesModel);
        public NoteTable UpdateColor(long NoteId, string color);
        public bool IsPinOrNot(long NoteId);
        public bool IsArchiveOrNot(long NoteId);
        public bool IsTrashOrNot(long NoteId);
        public bool DeleteNote( long NoteId);
        
        public string UploadImage(long NoteId, long UserId, IFormFile img);

        public List<NoteTable> searchNote(string query);

        public int GetNoteCount(int userId);

        public int ColorNoteCount(int userId);

        public int CountTrashNote(int userId);

        public CountModel NoteAllCount(int userId);
    }
}
