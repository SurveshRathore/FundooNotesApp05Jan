using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class CollabModel
    {
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        public DateTime? CollabModifiedDate { get; set; }
    }
}
