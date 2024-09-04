namespace webApi.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }

        IUserRepository UserRepository { get; }

        IPropertyRepository PropertyRepository { get; }

        IFurnishedTypeRepository FurnishedTypeRepository { get; }

        IPropertyTypeRepository PropertyTypeRepository { get; }
        Task<bool> SaveAsync();

    }
}
