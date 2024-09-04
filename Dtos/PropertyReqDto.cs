using System.ComponentModel.DataAnnotations.Schema;
using webApi.Modals;

namespace webApi.Dtos
{
    public class PropertyReqDto
    {
        public required string SellOrRent { get; set; }
        public required int Bhk { get; set; }
        public required string Name { get; set; }
        public int PropertyTypeId { get; set; }
        public int FunrnishedTypeId { get; set; }
        public int CityId { get; set; }
        public required int Price { get; set; }
        public int Maintenance { get; set; }
        public int Security { get; set; }
        public int BuiltArea { get; set; }
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public int Floor { get; set; }
        public int NumberOfFloors { get; set; }
        public string Landmark { get; set; }
        public string ReadyToMove { get; set; }
        public DateTime AvailableFrom { get; set; }
        public int AgeOfProperty { get; set; }
        public string GatedCommunity { get; set; }
        public string MainEntrance { get; set; }
        public string Description { get; set; }
        public int PostedBy { get; set; }

    }
}
