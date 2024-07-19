using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTokTtsAPI.Deserialization
{
    public class ApiResponse
    {
        public string data { get; set; }
        public object error { get; set; }
        public bool success { get; set; }
    }
}
