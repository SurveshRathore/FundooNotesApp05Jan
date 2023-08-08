using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class CountModel
    {
        public int NoteCount {  get; set; }
        public int ArchiveCount { get; set; }

        public int ColorNoteCount { get; set; }

        public int TrashNoteCount { get; set; }

    }
}
