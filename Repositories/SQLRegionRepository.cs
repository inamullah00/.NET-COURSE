using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Data;
using RESTAPI.Models.Domain;

namespace RESTAPI.Repositories
{
    public class SQLRegionRepository :IRegionRepository
    {
        private readonly NZWalksDbContext _context;

        public SQLRegionRepository(NZWalksDbContext context) {
            _context = context;
 
        }

        //GetAll
        public async Task<List<Region>> GetAllAsync()
        {

            return  await _context.Regions.ToListAsync();
        }

        //GetById
        public  async Task<Region> GetByIdAsync(Guid id)
        {
           return  await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);
       
        }

        //Delete
        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var existingRegion =  await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
             _context.Regions.Remove(existingRegion);
            await _context.SaveChangesAsync();
            return existingRegion;
        }

        //Update
        public async Task<Region> UpdateRegionAsync(Guid id,Region regionUpdatedData)
        {
            var existRegion= await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (existRegion == null)
            {
                return null;     
            }

            existRegion.Code = regionUpdatedData.Code;
            existRegion.Name = regionUpdatedData.Name;
            existRegion.RegionImageUrl = regionUpdatedData.RegionImageUrl;
            await _context.SaveChangesAsync();
        return existRegion;
        }


        //Create
        public async Task<Region> AddRegionAsync(Region region)
        {
             await _context.Regions.AddAsync(region);
             await _context.SaveChangesAsync();
            return region;
        }

       


    }
}
