using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class LabelModel
    {
        public long LabelId { get; set; }
        
        public string LabelName { get; set; }

        public long userId { get; set; }
        
        public long NoteId { get; set; }
    }
}
