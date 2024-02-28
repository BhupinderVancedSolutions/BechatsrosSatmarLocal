using System;

namespace Application.Common.Models.Response
{
    public class TeamConnect
    {
        public int UserId { get; init; }
        public string TeamConnectToken { get; set; }
    }
}
