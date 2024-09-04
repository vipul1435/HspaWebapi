using Microsoft.EntityFrameworkCore;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Data.Repo
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DataContext dc;

        public PropertyTypeRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<IEnumerable<PropertyType>> GetProppertyType()
        {
            return await dc.PropertyTypes.ToListAsync();
        }
    }
}
