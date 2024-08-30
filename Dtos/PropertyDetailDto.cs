using System.ComponentModel.DataAnnotations.Schema;
using webApi.Modals;

namespace webApi.Dtos
{
    public class PropertyDetailDto:PropertyListDto
    {
        public required string SellOrRent { get; set; }
        public int Maintenance { get; set; }
        public int Security { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public int Floor { get; set; }
        public int NumberOfFloors { get; set; }
        public string Landmark { get; set; }
        public DateTime AvailableFrom { get; set; }
        public int AgeOfProperty { get; set; }
        public string GatedCommunity { get; set; }
        public string MainEntrance { get; set; }
        public string Description { get; set; }
    }
}
