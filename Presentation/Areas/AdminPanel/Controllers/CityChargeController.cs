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
        public async Task<IActionResult> CreateUpdateCity(CreateUpdateRequestDtoList CreateUpdateRequestDto)
        {
           var dd =  await _cityChargeService.CreateUpdateCity(CreateUpdateRequestDto,_user.UserId);
            return Json(dd);

        }


    }
}
