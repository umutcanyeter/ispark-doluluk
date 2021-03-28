using IsparkDoluluk.Api.CustomErrors;
using IsparkDoluluk.Api.CustomFilters;
using IsparkDoluluk.Business.Abstract;
using IsparkDoluluk.Business.StringInfos;
using IsparkDoluluk.Dto.Concrete;
using IsparkDoluluk.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsparkDoluluk.Api.Controllers
{
    [Route("api/livecapacity")]
    [ApiController]
    public class LiveCapacityController : ControllerBase
    {
        private readonly ILiveCapacityService _liveCapacityService;
        private readonly IParkPlaceService _parkPlaceService;

        public LiveCapacityController(ILiveCapacityService liveCapacityService, IParkPlaceService parkPlaceService)
        {
            _liveCapacityService = liveCapacityService;
            _parkPlaceService = parkPlaceService;
        }

        [HttpGet("{parkPlaceId}")]
        public async Task<IActionResult> GetByParkPlaceId(int? parkPlaceId)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var liveCapacity = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == parkPlaceId);
            if(liveCapacity == null)
            {
                var parkPlaceNotFound = new ErrorModel()
                {
                    FieldName = "ParkPlaceId",
                    Message = "Not found park place for " + parkPlaceId.ToString()
                };

                errorModels.Add(parkPlaceNotFound);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }
            return Ok(liveCapacity);
        }

        [HttpGet("district={district}")]
        public async Task<IActionResult> GetByDistrict(string district)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var parkPlaces = await _parkPlaceService.GetAllByFilter(I => I.District == district);
            if(parkPlaces == null)
            {
                var parkPlaceNotFound = new ErrorModel()
                {
                    FieldName = "District",
                    Message = "Park Place not found with district " + district
                };
                errorModels.Add(parkPlaceNotFound);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }
            var responseDto = new List<LiveCapacityGetByDistrictReponseDto>();
            
            foreach(var parkPlace in parkPlaces)
            {
                var liveCapacity = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == parkPlace.Id);
                responseDto.Add(new LiveCapacityGetByDistrictReponseDto()
                {
                    ParkPlace = parkPlace,
                    CurrentCapacity = liveCapacity.CurrentCapacity
                });
            }

            return Ok(responseDto);
        }

        [HttpPut("updatecapacity")]
        [ValidModel]
        [Authorize(Roles = RoleInfo.ParkEmployee)]
        public async Task<IActionResult> UpdateCapacity([FromBody] UpdateCapacityDto dto)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var parkPlace = await _parkPlaceService.GetById(dto.ParkPlaceId);
            if(parkPlace == null)
            {
                var error = new ErrorModel()
                {
                    FieldName = "ParkPlaceId",
                    Message = "Bu id ile " + dto.ParkPlaceId + "park alanı bulunamadı."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            var liveCapacity = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == dto.ParkPlaceId);

            if(parkPlace.Capacity < dto.Capacity)
            {
                var error = new ErrorModel()
                {
                    FieldName = "Capacity",
                    Message = "Gönderdiğiniz kapasite park alanının gerçek kapasitesinden (" + parkPlace.Capacity + ") büyüktür."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            liveCapacity.CurrentCapacity = dto.Capacity;
            return Ok();
        }

        [HttpGet("decreaselivecapacity/{id}")]
        [ServiceFilter(typeof(ValidId<ParkPlace>))]
        [Authorize(Roles = RoleInfo.ParkPlaceTerminal)]
        public async Task<IActionResult> DecreaseLiveCapacity(int id)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var parkPlace = await _parkPlaceService.GetById(id);
            var liveCapacity = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == id);
            if(liveCapacity.CurrentCapacity <= 0)
            {
                var error = new ErrorModel()
                {
                    FieldName = "Capacity",
                    Message = "Geçerli park alanı için boş alan sayısı 0'dır. Daha fazla düşürülemez."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            liveCapacity.CurrentCapacity -= 1;
            await _liveCapacityService.Update(liveCapacity);
            return Ok();
        }

        [HttpGet("increaselivecapacity/{id}")]
        [ServiceFilter(typeof(ValidId<ParkPlace>))]
        [Authorize(Roles = RoleInfo.ParkPlaceTerminal)]
        public async Task<IActionResult> IncreaseLiveCapacity(int id)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var parkPlace = await _parkPlaceService.GetById(id);
            var liveCapacity = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == id);
            if (liveCapacity.CurrentCapacity >= parkPlace.Capacity)
            {
                var error = new ErrorModel()
                {
                    FieldName = "Capacity",
                    Message = "Geçerli park alanı için boş alan sayısı park alanın maksimum kapasitesi ile aynıdır. Daha fazla arttıramazsınız."
                };
                errorModels.Add(error);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            liveCapacity.CurrentCapacity += 1;
            await _liveCapacityService.Update(liveCapacity);
            return Ok();
        }
    }
}
