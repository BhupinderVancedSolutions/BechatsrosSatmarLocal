using System;

namespace DTO.Response.CityCharge
{
    public class CityChargeResponseDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
     
    }
}
