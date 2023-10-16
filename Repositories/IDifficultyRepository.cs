using RESTAPI.Models.Domain;

namespace RESTAPI.Repositories
{
    public interface IDifficultyRepository 
    {
          Task<List<Difficulty>> GetDifficultyAsync();
        Task<Difficulty>AddDifficultyAsync(Difficulty difficulty);
        Task<Difficulty>RemoveDifficultyAsync(Guid id);
        Task<Difficulty> UpdateDifficultyAsync(Guid id, Difficulty difficulty);
        Task<Difficulty> GetDifficultyAsync(Guid id);

    }

}
