using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Models
{
    public class ResponseModelDTO
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
        public object Data { get; set; }
    }
}
