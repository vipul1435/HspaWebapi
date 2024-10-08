﻿using Microsoft.EntityFrameworkCore;
using webApi.Errors;
using webApi.Interfaces;
using webApi.Modals;

namespace webApi.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;

        public PropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void AddProperty(Property property)
        {
            dc.Properties.Add(property);
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(string sellOrRent)
        {
            var properties = await dc.Properties
                .Where(p=>p.SellOrRent==sellOrRent)
                .Include(p => p.PropertyType)
                .Include(p=>p.FunrnishedType)
                .Include(p => p.Photos)
                .Include(p=>p.City)
                .ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var property = await dc.Properties
                .Where(p => p.Id == id)
                .Include(p => p.PropertyType)
                .Include(p => p.FunrnishedType)
                .Include(p=>p.Photos)
                .Include(p => p.City)
                .FirstOrDefaultAsync();
            return property;
        }

        public async Task<Property> GetPropertyPhotosDetailAsync(int id)
        {
            var property = await dc.Properties
                .Where(p => p.Id == id)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync();
            return property;
        }
    }
}
