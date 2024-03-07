
using System;
using System.Collections.Generic;

namespace DTO.Request.CityCharge
{
    public class CreateUpdateRequestDtoList
    {
  
        public List<CreateUpdateRequestDto> createUpdateRequestDtos { get; set; }
    }
    public class CreateUpdateRequestDto
    {
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CityId { get; set; }
    }


    public class CreateUpdateRequest
    {
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
    }
}
