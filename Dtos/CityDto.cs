

using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [StringLength(20,MinimumLength =2)]
        public required string Name { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public required string Country { get; set; }
    }
}
