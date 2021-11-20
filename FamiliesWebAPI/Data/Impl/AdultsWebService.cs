using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Data.Impl
{
    public class AdultsWebService : IAdultsService
    {
        public AdultsWebService()
        {
        }
        
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Adults.Include(a => a.Job).ToListAsync();;
        }

        public async Task<Adult> AddAdultAsync(int familyId, Adult adult)
        {
            await using var familiesContext = new FamiliesContext();
            var family = await familiesContext.Families
                .Include(f => f.Adults)
                .Include(f =>f.Children)
                .Include(f=>f.Pets)
                .FirstAsync(f => f.Id == familyId);
            
            family.Adults.Add(adult);
            familiesContext.Update(family);
            await familiesContext.SaveChangesAsync();
            return adult;
        }

        public async Task RemoveAdultAsync(int familyId, int id)
        {
            await using var familiesContext = new FamiliesContext();
            var toDelete = await familiesContext.Adults.FirstOrDefaultAsync(a => a.Id == id);
            if (toDelete != null)
            {
                familiesContext.Adults.Remove(toDelete);
                await familiesContext.SaveChangesAsync();
            }
        }

        public async Task<Adult> UpdateAdultAsync(int familyId, Adult adult)
        {
            await using var familiesContext = new FamiliesContext();
            var jobToUpdate = await familiesContext.Jobs.FirstAsync(j => j.Id == adult.Job.Id);
            var toUpdate = await familiesContext.Adults.Include(a=> a.Job)
                .FirstAsync(a => a.Id == adult.Id);
            jobToUpdate.JobTitle = adult.Job.JobTitle;
            jobToUpdate.Salary = adult.Job.Salary;
            
            toUpdate.Age = adult.Age;
            toUpdate.Height = adult.Height;
            toUpdate.Sex = adult.Sex;
            toUpdate.Weight = adult.Weight;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;

            familiesContext.Update(jobToUpdate);
            familiesContext.Update(toUpdate);
            await familiesContext.SaveChangesAsync();
            return toUpdate;
        }

        public async Task<IList<Adult>> GetFamilyAdultsAsync(int? familyId)
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Adults
                .Include(a => a.Job)
                .Where(a => a.Family.Id == familyId).ToListAsync();
        }
    }
}