

namespace DTO.Request.CityCharge
{
    public class CityChargeRequestDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
