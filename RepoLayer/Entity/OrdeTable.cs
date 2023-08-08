using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Entity
{
    public class OrdeTable
    {
        [Key]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public String ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
