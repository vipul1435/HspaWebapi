using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos;
using webApi.Interfaces;

namespace webApi.Controllers
{
    public class FurnishedTypeController:BaseController
    {
        private readonly IUnitOfWork global;
        private readonly IMapper mp;

        public FurnishedTypeController(IUnitOfWork global, IMapper mp)
        {
            this.global = global;
            this.mp = mp;
        }

        [HttpGet]
        public async Task<IActionResult> GetFurnishedTypes()
        {
            var furnishedTypes =  await global.FurnishedTypeRepository.GetFurnishedTypeAsync();
            var furnishedDto = mp.Map<IEnumerable<FurnishedTypeDto>>(furnishedTypes);
            return Ok(furnishedDto);
        }
    }
}
