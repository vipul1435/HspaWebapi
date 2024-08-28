using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dc;

        public CityRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void AddCity(City city)
        {
            dc.Cities.AddAsync(city);
        }

        public void DeleteCity(int cityId)
        {
            var city = dc.Cities.Find(cityId);
            if (city != null)
            {
                dc.Cities.Remove(city);
            }
        }

        public async Task<City> FindCity(int id)
        {
            var city=  await dc.Cities.FindAsync(id);
            return city;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await dc.Cities.ToListAsync();
        }

    }
}
