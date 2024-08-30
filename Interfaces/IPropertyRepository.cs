using webApi.Modals;

namespace webApi.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(string sellOrRent);

        Task<Property> GetPropertyDetailAsync(int id);
        void AddProperty(Property property);

        void DeleteProperty(int id);
    }
}
