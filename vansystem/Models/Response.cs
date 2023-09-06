using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vansystem.Models
{
    public class Response
    {
        public string status { get; set; }
        public string code { get; set; }
        public string messages { get; set; }
        public   List<Response> res { get; set; }
        public string data { get; set; }
        public string count { get; set; }
    }
}