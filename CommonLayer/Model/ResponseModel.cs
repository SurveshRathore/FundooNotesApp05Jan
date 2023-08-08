using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class ResponseModel <T>
    {
        public bool Status {get; set;}
        public String Message { get; set;}

        public T Data { get; set;}
    }
}
