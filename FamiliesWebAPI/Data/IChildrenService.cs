using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data
{
    public interface IChildrenService
    {
        Task<IList<Child>> GetChildrenAsync();
        Task<Child> AddChildAsync(int familyId, Child child);
        Task RemoveChildAsync(int familyId, int id);
        Task<Child> UpdateChildAsync(int familyId, Child child);
        Task<IList<Child>> GetFamilyChildrenAsync(int? familyId);
    }
}