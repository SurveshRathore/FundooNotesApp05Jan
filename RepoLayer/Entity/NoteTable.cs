using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class NoteTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        
        public string? NoteTitle { get; set; }
        public string? NoteDesciption { get; set; }
        public DateTime NoteReminder { get; set; }
        public string? NoteColor { get; set; }
        public string? NoteImage { get; set; }
        public bool NoteIsArchive { get; set; }
        public bool NoteIsPin { get; set; }
        public bool NoteIsTrash { get; set; }
        public DateTime NoteCreated { get; set; }
        public DateTime NoteModified { get; set; }

        [ForeignKey("userTable")]
        public long userId { get; set; }
        public virtual UserTable userTable { get; set; }
    }
}
