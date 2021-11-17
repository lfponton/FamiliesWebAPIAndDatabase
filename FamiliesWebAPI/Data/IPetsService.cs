using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data
{
    public interface IPetsService
    {
        Task<IList<Pet>> GetPetsAsync();
        Task<Pet> AddPetAsync(int familyId, Pet pet);
        Task RemovePetAsync(int familyId, int id);
        Task<Pet> UpdatePetAsync(int familyId, Pet pet);
        Task<IList<Pet>> GetFamilyPetsAsync(int? familyId);

    }
}