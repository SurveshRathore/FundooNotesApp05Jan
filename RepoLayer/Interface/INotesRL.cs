using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INotesRL
    {
        public NoteTable AddNewNote(NotesModel notesModel, long UserId);
        public List<NoteTable> GetAllNotes(long UserId);

        public bool UpdateNotes(long NoteId, long UserId, NotesModel notesModel);
        
        public bool IsPinOrNot(long NoteId);
        public bool IsArchiveOrNot(long NoteId);
        public bool IsTrashOrNot(long NoteId);
        public bool DeleteNote(long UserId, long NoteId);
        public NoteTable UpdateColor(long NoteId, string Color);

        public string UploadImage(long NoteId, long UserId, IFormFile img);
    }
}
