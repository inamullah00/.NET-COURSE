using AutoMapper;
using RESTAPI.Models.Domain;
using RESTAPI.Models.DTO;

namespace RESTAPI.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // we are mapping the Region into a RegionDTO
            // Region is SRC and RegionDTO is Destination
            CreateMap<Region, RegionsDTO>().ReverseMap(); // yaha pe hm Region ko DTO mai convert krte hai 
            CreateMap<AddRegionDTO, Region>().ReverseMap();//Or yaha pe hm DTO ko Region mai convert krte hai
            CreateMap<Region, DeleteRegionDTO>().ReverseMap();//Or yaha per hm  Region ko DTO mai convert krte hai
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();


            // Automapper For Walk Domin Model

            CreateMap<AddWalkDTO,Walk>().ReverseMap();  // yaha pe hm just DTO to Model mai Convert krte hai model ko DTO mai Convert krne k leye Hm Dosra Map banaye ge
            CreateMap<Walk,WalksDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();

            // Automapper For Difficulty Domin Model
            CreateMap<DifficultyDTO, Difficulty>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        
           
        }

    }
}
