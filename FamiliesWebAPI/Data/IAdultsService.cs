using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data
{
    public interface IAdultsService
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task<Adult> AddAdultAsync(int familyId, Adult adult);
        Task RemoveAdultAsync(int familyId, int id);
        Task<Adult> UpdateAdultAsync(int familyId, Adult adult);
        Task<IList<Adult>> GetFamilyAdultsAsync(int? familyId);


    }
}