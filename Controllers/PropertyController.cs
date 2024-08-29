using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webApi.Interfaces;

namespace webApi.Controllers
{
    public class PropertyController:BaseController
    {
        private readonly IUnitOfWork global;

        public PropertyController(IUnitOfWork global)
        {
            this.global = global;
        }

        [HttpGet("type/{sellRent }")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertiesList(string sellRent)
        {
            var properties = await global.PropertyRepository.GetPropertiesAsync(sellRent);
            return Ok(properties);
        }

    }
}
