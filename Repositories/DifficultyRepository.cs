using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Models.Domain;
using RESTAPI.Models.DTO;


namespace RESTAPI.Repositories
{
    public class DifficultyRepository:IDifficultyRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public DifficultyRepository(NZWalksDbContext context) 
        {
            _dbContext = context;
        }

        //--- Add Difficulty
        public async Task<Difficulty> AddDifficultyAsync( Difficulty difficulty)
        {
          await _dbContext.Difficulties.AddAsync(difficulty);
          await _dbContext.SaveChangesAsync();

            return difficulty;
        }

      

        //---   Get Difficulty
        public async Task<List<Difficulty>> GetDifficultyAsync()
        {

            var  difficultyData =  await _dbContext.Difficulties.ToListAsync();

            if(difficultyData == null)
            {
                return null;
            }
            return difficultyData;
        }

        public async Task<Difficulty> RemoveDifficultyAsync(Guid id)
        {
           var IsExistDifficulty = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
              if(IsExistDifficulty == null)
            {
                return null;
            }
             _dbContext.Difficulties.Remove(IsExistDifficulty);
             _dbContext.SaveChanges();
            return IsExistDifficulty;
        }

        //--- Update Difficulty
        public async  Task<Difficulty> UpdateDifficultyAsync(Guid id, Difficulty difficulty)
        {          
            var existingDifficulty =   await _dbContext.Difficulties.FirstOrDefaultAsync(difficulty => difficulty.Id == id);
         

            if(existingDifficulty == null)
            {
                return null;
            }

            existingDifficulty.Name = difficulty.Name;
            _dbContext.SaveChanges();

             return existingDifficulty;
        }


        // Get single Difficulty 
        public async Task<Difficulty> GetDifficultyAsync([FromRoute] Guid id)
        {
           
            var existingDifficulty = await _dbContext.Difficulties.FirstOrDefaultAsync(Difficulty=>Difficulty.Id == id);

            if(existingDifficulty == null)
            {
                return null;
            }

            return existingDifficulty;

        }
    }
}
