using webApi.Modals;

namespace webApi.Interfaces
{
    public interface IFurnishedTypeRepository
    {
        Task<IEnumerable<FurnishedType>> GetFurnishedTypeAsync();

    }
}
