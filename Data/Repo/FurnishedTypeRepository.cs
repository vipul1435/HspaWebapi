
using Microsoft.EntityFrameworkCore;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Data.Repo
{
    public class FurnishedTypeRepository : IFurnishedTypeRepository
    {
        private readonly DataContext dc;

        public FurnishedTypeRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<IEnumerable<FurnishedType>> GetFurnishedTypeAsync()
        {
            return await dc.FurnishedTypes.ToListAsync();
        }
    }
}
