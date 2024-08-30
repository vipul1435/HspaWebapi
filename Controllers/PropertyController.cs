using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos;
using webApi.Errors;
using webApi.Interfaces;

namespace webApi.Controllers
{
    public class PropertyController:BaseController
    {
        private readonly IUnitOfWork global;
        private readonly IMapper mapper;

        public PropertyController(IUnitOfWork global, IMapper mapper)
        {
            this.global = global;
            this.mapper = mapper;
        }

        [HttpGet("type/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertiesList(string sellRent)
        {
            var properties = await global.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertListDto = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertListDto);
        }

        [HttpGet("property/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await global.PropertyRepository.GetPropertyDetailAsync(id);
            if (property == null)
            {
                ApiError apiError = new(NotFound().StatusCode, "Property does not exists.");
                return NotFound(apiError);
            }
            var propertyDto = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDto);
        }

    }
}
