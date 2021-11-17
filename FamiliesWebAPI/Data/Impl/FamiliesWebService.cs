using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Data.Impl
{
    public class FamiliesWebService : IFamiliesService
    {
        public FamiliesWebService()
        {
        }

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Families.ToListAsync();
        }

        public async Task<Family> AddFamilyAsync(Family family)
        {
            await using var familiesContext = new FamiliesContext();
            await familiesContext.Families.AddAsync(family);
            await familiesContext.SaveChangesAsync();
            return family;
        }

        public async Task RemoveFamilyAsync(int id)
        {
            await using var familiesContext = new FamiliesContext();
            var toDelete = await familiesContext.Families.FirstOrDefaultAsync(f => f.Id == id);
            if (toDelete != null)
            {
                familiesContext.Families.Remove(toDelete);
                await familiesContext.SaveChangesAsync();
            }
        }

        public async Task<Family> UpdateFamilyAsync(Family family)
        {
            await using var familiesContext = new FamiliesContext();
            try
            {
                var toUpdate = await familiesContext.Families.FirstAsync(f => f.Id == family.Id);
                toUpdate.StreetName = family.StreetName;
                toUpdate.HouseNumber = family.HouseNumber;
                toUpdate.Adults = family.Adults;
                toUpdate.Children = family.Children;
                toUpdate.Pets = family.Pets;
                await familiesContext.SaveChangesAsync();
                return toUpdate;
            }
            catch (Exception e)
            {
                throw new Exception($"Could not find family with id {family.Id}");
            }
        }

        public async Task<Family> GetFamilyByIdAsync(int? id)
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Families.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}