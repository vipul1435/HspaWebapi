using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webApi.Dtos;
using webApi.Errors;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Controllers
{
    public class PropertyController:BaseController
    {
        private readonly IUnitOfWork global;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PropertyController(
            IUnitOfWork global, 
            IMapper mapper,
            IPhotoService photoService
            )
        {
            this.global = global;
            this.mapper = mapper;
            this.photoService = photoService;
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


        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddpropertyDetail(PropertyReqDto propertyReqDto)
        {
            var userId = getUserId();
            var property = mapper.Map<Property>(propertyReqDto);
            property.PostedBy = userId;
            property.LastUpdatedBy = userId;
            global.PropertyRepository.AddProperty(property);
            await global.SaveAsync();
            return Ok(new { message = "Property has been listed successfully.", Id = property.Id });
        }


        [HttpPost("add/upload/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage(IFormFile file, int id)
        {
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                return BadRequest("Upload  not uploaded");
            }

            var property = await global.PropertyRepository.GetPropertyPhotosDetailAsync(id);
            var photo = new Photo()
            {
                ImageUrl = result.SecureUri.AbsoluteUri,
                PublicId = result.PublicId,
                PropertyId = id
            };
            if (property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }
            property.Photos.Add(photo);
            await global.SaveAsync();
            return Ok(new
            {
                photo.ImageUrl,
                photo.IsPrimary,
                photo.PublicId

            });
        }


        [HttpPost("set-primary-photo/{id}/{publickey}")]
        [Authorize]

        public async Task<IActionResult> SetPrimaryPhoto(int id,string publickey)
        {
            var userId = getUserId();
            var property = await global.PropertyRepository.GetPropertyDetailAsync(id);

            if (property == null)
            {
                return BadRequest("Property does not exists.");
            }

            if (property.PostedBy != userId)
            {
                return BadRequest("You are unauthorise to change the photo.");
            }

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == publickey);
            if(photo ==null)
            {
                return BadRequest("No such property or photo exists");
            }

            if (photo.IsPrimary)
            {
                return BadRequest("This is alreadya primary photo"); 
            }

            var currentPrimaryPhoto = property.Photos.FirstOrDefault(p => p.IsPrimary);
            if(currentPrimaryPhoto!=null)
            {
                currentPrimaryPhoto.IsPrimary = false;
            }

            photo.IsPrimary = true;

            if(await global.SaveAsync())
            {
                return Ok(new { message = "Photo set to primary successfully." });
            }

            return BadRequest("Something went wrong.");
        }


        [HttpDelete("delete-photo/{id}/{publickey}")]
        [Authorize]

        public async Task<IActionResult> DeletePhoto(int id, string publickey)
        {
            var userId = getUserId();
            var property = await global.PropertyRepository.GetPropertyDetailAsync(id);

            if (property == null)
            {
                return BadRequest("Property does not exists.");
            }

            if (property.PostedBy != userId)
            {
                return BadRequest("You are unauthorise to delete the photo.");
            }

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == publickey);
            if (photo == null)
            {
                return BadRequest("No such property or photo exists");
            }

            if (photo.IsPrimary)
            {
                return BadRequest("You can't delete the primary photo");
            }

            var result = photoService.DeletePhotoAsync(publickey);

            if (result.Result == null)
            {
                return BadRequest("Unable to delete photo.");
            }
            property.Photos.Remove(photo);
            

            if (await global.SaveAsync())
            {
                return Ok(new { message = "Photo deleted successfully." });
            }

            return BadRequest("Something went wrong.");
        }


    }
}
