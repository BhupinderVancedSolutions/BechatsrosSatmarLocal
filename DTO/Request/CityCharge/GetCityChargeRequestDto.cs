

namespace DTO.Request.CityCharge
{
    public class GetCityChargeRequestDto
    {
        public int CityId { get; set; }
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
    }
}
