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
            return await familiesContext.Families
                .Include(f => f.Adults)
                .Include(f => f.Children)
                .Include(f => f.Pets).ToListAsync();
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
            var toUpdate = await familiesContext.Families.Include(f => f.Adults)
                .Include(f => f.Children)
                .Include(f => f.Pets)
                .FirstAsync(f => f.Id == family.Id);
            toUpdate.StreetName = family.StreetName;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            familiesContext.Update(toUpdate);
            await familiesContext.SaveChangesAsync();
            return toUpdate;
        }

        public async Task<Family> GetFamilyByIdAsync(int? id)
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Families
                .Include(f => f.Adults)
                .Include(f => f.Children)
                .Include(f => f.Pets).FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}