using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class LabelTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        

        [ForeignKey("userTable")]
        public long userId { get; set; }
        public virtual UserTable userTable { get; set; }

        [ForeignKey("noteTable")]
        public long NoteId { get; set; }
        public virtual NoteTable noteTable { get; set; }
    }
}
