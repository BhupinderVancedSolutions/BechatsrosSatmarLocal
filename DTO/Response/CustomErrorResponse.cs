using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response
{
    public class ResultViewModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
        public int Id { get; set; }
        public bool isSuperAdmin { get; set; }
    }
}
