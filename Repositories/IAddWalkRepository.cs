using RESTAPI.Models.Domain;

namespace RESTAPI.Repositories
{
    public interface IAddWalkRepository
    {
        Task<Walk> AddWalkAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, 
            string? SortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> DeleteWalkByIdAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walkData);
    }
}
