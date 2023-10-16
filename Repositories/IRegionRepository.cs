using Microsoft.AspNetCore.Http.HttpResults;
using RESTAPI.Models.Domain;

namespace RESTAPI.Repositories
{
    public interface IRegionRepository
    {
         Task<List<Region>> GetAllAsync();
        Task<Region>GetByIdAsync(Guid id);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<Region>UpdateRegionAsync(Guid id,Region  region);
        Task<Region>AddRegionAsync(Region region);

    }
}
