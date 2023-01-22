using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.BL.Utility
{
    
    public class ResponseHendler<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        
    }
}
