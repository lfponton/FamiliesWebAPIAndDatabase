using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Data.Impl
{
    public class PetsWebService : IPetsService
    {
        public PetsWebService()
        {
        }
        
        public async Task<IList<Pet>> GetPetsAsync()
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Pets.ToListAsync();;
        }

        public async Task<Pet> AddPetAsync(int familyId, Pet pet)
        {
            await using var familiesContext = new FamiliesContext();
            var family = await familiesContext.Families
                .Include(f => f.Adults)
                .Include(f =>f.Children)
                .Include(f=>f.Pets)
                .FirstAsync(f => f.Id == familyId);
            
            family.Pets.Add(pet);
            familiesContext.Update(family);
            await familiesContext.SaveChangesAsync();
            return pet;
        }

        public async Task RemovePetAsync(int familyId, int id)
        {
            await using var familiesContext = new FamiliesContext();
            var toDelete = await familiesContext.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (toDelete != null)
            {
                familiesContext.Pets.Remove(toDelete);
                await familiesContext.SaveChangesAsync();
            }
        }

        public async Task<Pet> UpdatePetAsync(int familyId, Pet pet)
        {
            await using var familiesContext = new FamiliesContext();
            var toUpdate = await familiesContext.Pets
                .FirstAsync(p => p.Id == pet.Id);

            toUpdate.Age = pet.Age;
            toUpdate.Species = pet.Species;
            toUpdate.Name = pet.Name;
            
            familiesContext.Update(toUpdate);
            await familiesContext.SaveChangesAsync();
            return toUpdate;
        }

        public async Task<IList<Pet>> GetFamilyPetsAsync(int? familyId)
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Pets
                .Where(p => p.Family.Id == familyId).ToListAsync();
        }
    }
}