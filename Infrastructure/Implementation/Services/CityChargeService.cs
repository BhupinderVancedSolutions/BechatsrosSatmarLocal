using Application.Common.Dtos;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Request;
using Application.Models.Response;
using DTO.Request.CityCharge;
using DTO.Response.CityCharge;
using Infrastructure.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;


namespace Infrastructure.Implementation.Services
{
    [ScopedService]
    public class CityChargeService : ICityChargeService
    {
        private readonly ICityChargeRepository _cityChargeRepository;
        public CityChargeService(ICityChargeRepository cityChargeRepository)
        {
            _cityChargeRepository = cityChargeRepository;

        }

        public async Task<string> CreateUpdateCity(CreateUpdateRequestDtoList createUpdateRequestDtoList)
        {
            return await _cityChargeRepository.CreateUpdateCity(CreateUpdateCityXml(createUpdateRequestDtoList), createUpdateRequestDtoList);
        }

        public async Task<bool> DeleteCity(int cityId)
        {
            var IsDelete = await _cityChargeRepository.DeleteCity(cityId);
            return IsDelete;
        }

        public async Task<PaginatedList<CityChargeResponseDto>> GetCities(CommonRequest commonRequest)
        {
            var (cities, total) = await _cityChargeRepository.GetCities(commonRequest);
            return new PaginatedList<CityChargeResponseDto>(cities, total);
        }



        #region private
        private string CreateUpdateCityXml(CreateUpdateRequestDtoList createUpdateRequestDtoList)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var findItems in createUpdateRequestDtoList.createUpdateRequestDtos)
            {
                XmlNode doorSelectionNode = xmlDocument.CreateElement("CityXml");
                XmlAttribute attribute = xmlDocument.CreateAttribute("UserId");
                attribute.Value = findItems.UserId.ToString();
                doorSelectionNode.Attributes.Append(attribute);
                rootNode.AppendChild(doorSelectionNode);

                attribute = xmlDocument.CreateAttribute("Price");
                attribute.Value = findItems.Price.ToString();
                doorSelectionNode.Attributes.Append(attribute);
                rootNode.AppendChild(doorSelectionNode);

                attribute = xmlDocument.CreateAttribute("CityName");
                attribute.Value = findItems.CityName.ToString();
                doorSelectionNode.Attributes.Append(attribute);
                rootNode.AppendChild(doorSelectionNode);

                attribute = xmlDocument.CreateAttribute("CityId");
                attribute.Value = findItems.CityId.ToString();
                doorSelectionNode.Attributes.Append(attribute);
                rootNode.AppendChild(doorSelectionNode);

            }

            return xmlDocument.OuterXml;
        }
        #endregion


    }
}
