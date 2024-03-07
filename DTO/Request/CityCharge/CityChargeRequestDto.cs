

using System.ComponentModel.DataAnnotations;

namespace DTO.Request.CityCharge
{
    public class CityChargeRequestDto
    {
        public int CityId { get; set; }
        [Required(ErrorMessage = "Please enter a CityName.")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Price Is Required.")]
        public decimal? Price { get; set; }
        public int CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
