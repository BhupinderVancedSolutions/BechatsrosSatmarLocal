

namespace DTO.Request.CityCharge
{
    public class UpdateRequestDto
    {
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CityId { get; set; }
    }
}
