
using System;
using System.Collections.Generic;

namespace DTO.Request.CityCharge
{
    public class CreateUpdateRequestDtoList
    {
        //public CreateUpdateRequestDtoList()
        //{
        //    createUpdateRequestDtos = new List<CreateUpdateRequestDto>();
        //}
        public List<CreateUpdateRequestDto> createUpdateRequestDtos { get; set; }
    }
    public class CreateUpdateRequestDto
    {
        public int UserId { get; set; }
        public string CityName { get; set; }
        public decimal? Price { get; set; }
        public int CityId { get; set; }
    }
}
