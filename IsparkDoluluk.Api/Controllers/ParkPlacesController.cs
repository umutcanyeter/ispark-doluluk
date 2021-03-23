using AutoMapper;
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
    [Route("api/parkplaces")]
    [ApiController]
    public class ParkPlacesController : ControllerBase
    {
        private readonly IParkPlaceService _parkPlaceService;
        private readonly ILiveCapacityService _liveCapacityService;
        private readonly IMapper _mapper;

        public ParkPlacesController(IParkPlaceService parkPlaceService, ILiveCapacityService liveCapacityService)
        {
            _parkPlaceService = parkPlaceService;
            _liveCapacityService = liveCapacityService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var parkPlaces = await _parkPlaceService.GetAll();
            return Ok(parkPlaces);
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<ParkPlace>))]
        public async Task<IActionResult> GetById(int id)
        {
            var parkPlace = await _parkPlaceService.GetById(id);
            return Ok(parkPlace);
        }

        [HttpGet("district={district}")]
        public async Task<IActionResult> GetByDistrict(string district)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            if (district == null)
            {
                var districtNull = new ErrorModel()
                {
                    FieldName = "District",
                    Message = "District cannot be null. (Usage: api/parkplaces/district=districtname)"
                };
                errorModels.Add(districtNull);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            var parkPlaces = await _parkPlaceService.GetAllByFilter(I => I.District == district);
            return Ok(parkPlaces);
        }

        [HttpPost("")]
        [ValidModel]
        [Authorize(Roles = RoleInfo.ParkManager)]
        public async Task<IActionResult> AddParkPlace(ParkPlaceAddDto dto)
        {
            await _parkPlaceService.Add(_mapper.Map<ParkPlace>(dto));
            var parkPlace = await _parkPlaceService.GetByFilter(I => I.Name == dto.Name);
            await _liveCapacityService.Add(new LiveCapacity()
            {
                ParkPlaceId = parkPlace.Id,
                CurrentCapacity = parkPlace.Capacity
            });
            return Ok();
        }

        [HttpPut("")]
        [ValidModel]
        [Authorize(Roles = RoleInfo.ParkManager)]
        public async Task<IActionResult> UpdateParkPlace(ParkPlaceUpdateDto dto)
        {
            List<ErrorModel> errorModels = new List<ErrorModel>();
            var parkPlaceInDb = await _parkPlaceService.GetById(dto.Id);
            if(parkPlaceInDb == null)
            {
                var parkPlaceNotFound = new ErrorModel()
                {
                    FieldName = "Id",
                    Message = "Park place not found with id " + dto.Id
                };
                errorModels.Add(parkPlaceNotFound);
                var response = new ErrorResponse()
                {
                    Errors = errorModels
                };
                return BadRequest(response);
            }

            var liveCapacityInDb = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == dto.Id);
            liveCapacityInDb.CurrentCapacity = dto.Capacity;
            await _parkPlaceService.Update(_mapper.Map<ParkPlace>(dto));
            await _liveCapacityService.Update(liveCapacityInDb);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidId<ParkPlace>))]
        [Authorize(Roles = RoleInfo.ParkManager)]
        public async Task<IActionResult> DeleteParkPlace(int id)
        {
            var parkPlace = await _parkPlaceService.GetById(id);
            var liveCapacity = await _liveCapacityService.GetByFilter(I => I.ParkPlaceId == parkPlace.Id);

            await _parkPlaceService.Remove(parkPlace);
            await _liveCapacityService.Remove(liveCapacity);
            return Ok();
        }
    }
}
