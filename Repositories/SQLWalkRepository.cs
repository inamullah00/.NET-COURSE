using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Models.Domain;
using RESTAPI.Models.DTO;

namespace RESTAPI.Repositories
{
    public class SQLWalkRepository : IAddWalkRepository
    {
        private readonly NZWalksDbContext _context;
        public SQLWalkRepository(NZWalksDbContext context) {

            _context = context;
        }

        // Add Walk
        public async Task<Walk> AddWalkAsync(Walk walk)
        {
           await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        //Get All Walk
        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn=null,string? filterQuery=null,
            string? SortBy=null , bool? isAscending=true ,int pageNumber=1 , int pageSize=1000)
        {
           // .AsQueryable(); when we write .AsQueryable();then we have more controll on complex query 
            var walksData = _context.Walks.Include("Difficulty").Include("Region").AsQueryable();
           //here we check that the filterOn is not null? if not it will return true and this statement will execute  return await walksData.ToListAsync();
            if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                //we just apply filter on Name==name or something mean Smaal letter or capital
                if (filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                            walksData = walksData.Where(walk=>walk.Name.Contains(filterQuery));
                }
            }

            //Sorting

            if(string.IsNullOrWhiteSpace(SortBy) == false)
            {
              if(SortBy.Equals("name" , StringComparison.OrdinalIgnoreCase))
                {
                    bool ascending = isAscending ?? true;
                    walksData = ascending ? walksData.OrderBy(x => x.Name): walksData.OrderByDescending(x => x.Name); 
                }else if (SortBy.Equals("length",StringComparison.OrdinalIgnoreCase))
                {

                    walksData = isAscending ?? true ? walksData.OrderBy(x => x.LengthInKm) : walksData.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination

            var skipResult = (pageNumber - 1) * pageSize;
            return await walksData.Skip(skipResult).Take(pageSize).ToListAsync();
         
        }


        // Get WALK By ID
        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            return await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(walk => walk.Id == id);
        }



       //Delete Walk By ID
        public async Task<Walk>DeleteWalkByIdAsync(Guid id)
        {
           var ExistingWalk =  await _context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(walk=>walk.Id==id);
            if(ExistingWalk == null)
            {
              
                return null;
            }
            _context.Walks.Remove(ExistingWalk);
            await _context.SaveChangesAsync();
            return ExistingWalk;
            // The inside include Method we add the Navigation Property in the form of string 
        }


        // Update Walk Data

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk WalkData)
        {
            if (id == Guid.Empty && WalkData == null)
            {
                return null;
            }
            var existingModelData = await _context.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

            if (existingModelData != null)
            {
                
                existingModelData.Name = WalkData.Name;
                existingModelData.Description = WalkData.Description;
                existingModelData.LengthInKm = WalkData.LengthInKm;
                existingModelData.WalkImageUrl = WalkData.WalkImageUrl;
                existingModelData.DifficultyId = WalkData.DifficultyId;
                existingModelData.RegionId = WalkData.RegionId;

                _context.SaveChanges();

                return existingModelData;
            }
            else
            {
                return null;
            }

        }
    }
}
