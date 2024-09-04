using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos;
using webApi.Interfaces;

namespace webApi.Controllers
{
    public class PropertyTypeController:BaseController
    {
        private readonly IUnitOfWork global;
        private readonly IMapper mp;

        public PropertyTypeController(IUnitOfWork global, IMapper mp)
        {
            this.global = global;
            this.mp = mp;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await global.PropertyTypeRepository.GetProppertyType();
            var propertyTypeDto = mp.Map<IEnumerable<PropertyTypeDto>>(propertyTypes);
            return Ok(propertyTypeDto);
        }


    }
}
