using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class StudentTable
    {
        [Key]
        public long StudentId { get; set; }
        public string StudentName { get; set; }
       
        public string StudentEmail { get; set; }

        public int StudentMobile { get; set; }
        
    }
}
