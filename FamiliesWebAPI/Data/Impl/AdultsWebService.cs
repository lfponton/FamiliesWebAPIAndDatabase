using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data.Impl
{
    public class AdultsWebService : IAdultsService
    {
        public IList<Adult> Adults { get; private set; }
        private readonly IFamiliesService familiesService;

        public AdultsWebService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetAdultsAsync();
        }
        
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            Adults = new List<Adult>();
            foreach (var family in await familiesService.GetFamiliesAsync())
            {
                foreach (var adult in family.Adults)
                {
                    Adults.Add(adult);
                }
            }
            return Adults;
        }

        public async Task<Adult> AddAdultAsync(int familyId, Adult adult)
        {
            Adults.Add(adult);
            Family family = await familiesService.GetFamilyByIdAsync(familyId);
            family.Adults.Add(adult);
            await familiesService.UpdateFamilyAsync(family);
            return adult;
        }

        public async Task RemoveAdultAsync(int familyId, int id)
        {
            Adult toRemove = Adults.First(a => a.Id == id);
            Adults.Remove(toRemove);
            Family family = await familiesService.GetFamilyByIdAsync(familyId);
            family.Adults.Remove(toRemove);
            await familiesService.UpdateFamilyAsync(family);
        }

        public async Task<Adult> UpdateAdultAsync(int familyId, Adult adult)
        {
            Adult toUpdate = Adults.First(a => a.Id == adult.Id);
            toUpdate.Job = adult.Job;
            toUpdate.Age = adult.Age;
            toUpdate.Height = adult.Height;
            toUpdate.Sex = adult.Sex;
            toUpdate.Weight = adult.Weight;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            await familiesService.UpdateFamilyAsync(await familiesService.GetFamilyByIdAsync(familyId));
            return toUpdate;
        }

        public async Task<IList<Adult>> GetFamilyAdultsAsync(int? familyId)
        {
            IList<Adult> adults = new List<Adult>();
            Family family = await familiesService.GetFamilyByIdAsync(familyId);
            foreach (var a in family.Adults)
            {
                adults.Add(a);
            }
            return adults;
        }
    }
}