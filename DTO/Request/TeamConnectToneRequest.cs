using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Request
{
    public class TeamConnectToneRequest
    {
        public bool? ToneKJ { get; set; }
        public bool? ToneLinden { get; set; }
        public string TeamConnectToken { get; set; }
    }
}
