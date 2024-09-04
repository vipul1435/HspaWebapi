using webApi.Modals;

namespace webApi.Interfaces
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetProppertyType();
    }
}
