using webApi.Modals;

namespace webApi.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(string sellOrRent);
        void AddProperty(Property property);

        void DeleteProperty(int id);
    }
}
