using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamiliesWebAPI.Data.Impl
{
    public class ChildrenWebService : IChildrenService
    {
        public ChildrenWebService()
        {
        }
        
        public async Task<IList<Child>> GetChildrenAsync()
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Children
                .Include(c => c.Interests)
                .Include(c => c.Pets).ToListAsync();;
        }

        public async Task<Child> AddChildAsync(int familyId, Child child)
        {
            await using var familiesContext = new FamiliesContext();
            var family = await familiesContext.Families
                .Include(f => f.Adults)
                .Include(f =>f.Children)
                .Include(f=>f.Pets)
                .FirstAsync(f => f.Id == familyId);
            
            family.Children.Add(child);
            familiesContext.Update(family);
            await familiesContext.SaveChangesAsync();
            return child;
        }

        public async Task RemoveChildAsync(int familyId, int id)
        {
            await using var familiesContext = new FamiliesContext();
            var toDelete = await familiesContext.Children.FirstOrDefaultAsync(c => c.Id == id);
            if (toDelete != null)
            {
                familiesContext.Children.Remove(toDelete);
                await familiesContext.SaveChangesAsync();
            }
        }

        public async Task<Child> UpdateChildAsync(int familyId, Child child)
        {
            await using var familiesContext = new FamiliesContext();
            var toUpdate = await familiesContext.Children
                .Include(c => c.Interests)
                .Include(c => c.Pets)
                .FirstAsync(c => c.Id == child.Id);
            
            toUpdate.Interests = child.Interests;
            toUpdate.Pets = child.Pets;
            toUpdate.Age = child.Age;
            toUpdate.Height = child.Height;
            toUpdate.Sex = child.Sex;
            toUpdate.Weight = child.Weight;
            toUpdate.EyeColor = child.EyeColor;
            toUpdate.HairColor = child.HairColor;
            toUpdate.FirstName = child.FirstName;
            toUpdate.LastName = child.LastName;
            
            familiesContext.Update(toUpdate);
            await familiesContext.SaveChangesAsync();
            return toUpdate;
        }
        
        public async Task<IList<Child>> GetFamilyChildrenAsync(int? familyId)
        {
            await using var familiesContext = new FamiliesContext();
            return await familiesContext.Children
                .Include(c => c.Interests)
                .Include(c => c.Pets)
                .Where(a => a.Family.Id == familyId).ToListAsync();
        }
        
    }
}