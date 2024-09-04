
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using webApi.Data;
using webApi.Dtos;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Controllers
{
    [Authorize ]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork global;
        private readonly IMapper mp;

        public CityController(IUnitOfWork global, IMapper mp)
        {
            this.global = global;
            this.mp = mp;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var cities = await global.CityRepository.GetCitiesAsync();

            //throw new UnauthorizedAccessException();
            //using automapper-> under the we write the desired data type and Destination
            // and then pass source as argument
            var citiesDto = mp.Map<IEnumerable<CityDto>>(cities);

            //var citiesDto = from city in cities
            //                select new CityDto()
            //                {
            //                    Id = city.Id,
            //                    Name = city.Name
            //                };

            return Ok(citiesDto);
        }


        // it will take city a parameter
        [HttpPost("add")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new()
            {
                Name = cityName,
                LastUpdatedOn = DateTime.Now,
                LastUpdatedBy = 1,
            };
            global.CityRepository.AddCity(city);
            await global.SaveAsync();
            return Ok(city);
        }


        //it will take the city as a json format 
        [HttpPost("post")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            //City city = new()
            //{
            //    Name = cityDto.Name,
            //    LastUpdatedOn = DateTime.Now,
            //    LastUpdatedBy = 1,
            //};

            City city = mp.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;
            global.CityRepository.AddCity(city);
            await global.SaveAsync();
            return Ok(city);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            global.CityRepository.DeleteCity(id);
            await global.SaveAsync();
            return Ok(id);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            //try
            //{
                var dbCity = await global.CityRepository.FindCity(id);

            if (id != cityDto.Id)
                return StatusCode(400, "Update not allowed");

            if (dbCity == null)
                    return StatusCode(400, "Docoment not found");

                dbCity.LastUpdatedBy = 1;
                dbCity.LastUpdatedOn = DateTime.Now;
                mp.Map(cityDto, dbCity);
                await global.SaveAsync();
                return Ok(dbCity);
            //} catch (Exception e)
            //{
            //    return StatusCode(400, e.Message);
            //}
        }


        //this is not recomended as this using a third party package Newtons JSON
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var dbCity = await global.CityRepository.FindCity(id);
            dbCity.LastUpdatedBy = 2;
            dbCity.LastUpdatedOn = DateTime.Now;
            cityToPatch.ApplyTo(dbCity, ModelState);  
            await global.SaveAsync();
            return Ok(dbCity);
        }
    }
}
