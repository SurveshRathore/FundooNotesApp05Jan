using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class NotesModel
    {
        public string? NoteTitle { get; set; }
        public string? NoteDesciption { get; set; }
        public DateTime NoteReminder { get; set; }
        public string? NoteColor { get; set; }
        public string? NoteImage { get; set; }

        [DefaultValue(false)]
        public bool NoteIsArchive { get; set; }

        [DefaultValue(false)]
        public bool NoteIsPin { get; set; }

        [DefaultValue(false)]
        public bool NoteIsTrash { get; set; }
        public DateTime NoteCreated { get; set; }
        public DateTime NoteModified { get; set; }
    }
}
