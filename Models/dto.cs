using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareWinAuth.Models
{
    public class dto
    {
        public class TokenDetail
        {
            public string name { get; set; }
            public string name2 { get; set; }
            //[JsonIgnore]
            public string token { get; set; }
            //[JsonIgnore]
            public string AccessToken { get; set; }
            public string error { get; set; }
        }
    }
}
