using Application.Authentication.Queries.CityCharge;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Presentation.Controllers;
using Application.DTO.Response;
using DTO.Request.CityCharge;
using Common.Settings;
using DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Common.Helper;
using Application.Areas.Command.CreateCategory;
using Mapster;
using Application.Areas.Command.DeleteCity;
using System.Collections.Generic;
using Application.Areas.Queries.GetCityById;
using System;
using Application.Areas.Command.UpdateCity;
using Application.Areas.Command.CreateUpdateCity;

namespace Presentation.Areas.AdminPanel.Controllers
{
    public class CityChargeController : Controller1
    {

        private readonly ICityChargeService _cityChargeService;
        private readonly AppSettings _appSettings;
        private readonly UserRequest _user;
        public CityChargeController(ICityChargeService cityChargeService, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _cityChargeService = cityChargeService;
            _appSettings = appSettings.Value;
            _user = httpContextAccessor.HttpContext.Session.GetObjectFromJson<UserRequest>("LoggedInUserDetails", _appSettings.Secret);
        }
        public IActionResult Index()
        {
            return View("~/Areas/AdminPanel/Views/CityCharge/Index.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> AddModel()
        {
            return PartialView("~/Areas/AdminPanel/Views/CityCharge/_CityCharge.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(int startRow, int endRow, string filterQuery, string orderBy)
        {
            CommonRequest commonRequest = new CommonRequest()
            {
                StartRow = startRow,
                EndRow = endRow,
                FilterQuery = filterQuery,
                OrderBy = orderBy
            };
            var categories = await Mediator.Send(new GetCitiesQuery { CommonRequest = commonRequest });
            return Json(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateCity(CreateUpdateRequestDtoList createUpdateRequestDtoList)
        {
           Result  result = await Mediator.Send(createUpdateRequestDtoList.Adapt<CreateUpdateCityCommand>());
            return Json(new { message = result.Succeeded == true ? result.Messages[0] : result.Errors[0], success = result.Succeeded, isExist = result.IsExist });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateUpdateRequest CreateUpdateRequest)
        {
            Result result = await Mediator.Send(CreateUpdateRequest.Adapt<CreateCityCommand>());
            return Json(new { message = result.Succeeded == true ? result.Messages[0] : result.Errors[0], success = result.Succeeded, isExist = result.IsExist });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            var result = await Mediator.Send(new DeleteCityCommand { CityId = cityId });
            return Json(new { message = result.Succeeded == true ? result.Messages[0] : result.Errors[0], success = result.Succeeded });
        }

        [HttpGet]
        public async Task<IActionResult> EditCity(int cityId)
        {
            try
            {
                var city = await Mediator.Send(new GetCityByIdQuery { CityId = cityId });
             return PartialView("~/Areas/AdminPanel/Views/CityCharge/_EditCityCharge.cshtml", city.Adapt<CityChargeRequestDto>());

            }
            catch(Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCity(UpdateRequestDto updateRequestDto)
        {
            Result result = await Mediator.Send(updateRequestDto.Adapt<UpdateCityCommand>());
            return Json(new { message = result.Succeeded == true ? result.Messages[0] : result.Errors[0], success = result.Succeeded, isExist = result.IsExist });
        }




    }
}
