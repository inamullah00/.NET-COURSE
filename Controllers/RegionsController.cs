using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.CustomeActionFilter;
using RESTAPI.Data;
using RESTAPI.Models.Domain;
using RESTAPI.Models.DTO;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers

{
    [EnableCors("AllowReactApp")]
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
         private readonly NZWalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(NZWalksDbContext context, IRegionRepository regionRepository,IMapper mapper)
        {
            _context = context;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
  
        
        [HttpGet]
        [Route("GetAll")]
       
        public async Task<IActionResult> GetAll()
        {
            // get Data from Database in the form of list or collection
            var Data_From_regions_Models = await _regionRepository.GetAllAsync();

            //we are Map the Domin Model to DTO
            //we have Used return type List because it return List of record
            var regionsDTO = _mapper.Map<List<RegionsDTO>>(Data_From_regions_Models);
             return Ok(regionsDTO);
        }


        
        [HttpGet]
        [Route("GetById/{id:Guid}")]
         // for type safe we add Guid with Id
       
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {        
            var Data_From_regions_Models = await _regionRepository.GetByIdAsync(id);

            if (Data_From_regions_Models == null)
            {
                return NotFound();
            }

           // Create DTO and MAP Data from region into DTO
            var regionDTO = _mapper.Map<RegionsDTO>(Data_From_regions_Models);
            // RETURN data from DTO
            return Ok(regionDTO);

        }


        
      [HttpDelete]
      [Route("DeleteRegion/{id:Guid}")]
    
        public async Task<IActionResult> DeleteRegion([FromRoute ] Guid id)
        {
            var region = await _regionRepository.DeleteRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            var DeleteRegionDTOData = _mapper.Map<RegionsDTO>(region);
  
            return Ok(new
            {// here we have returned the  DeleteRegionDTOData back to the client but if we don't need to send data back toward the client then we don't need to use DTO
                DATA = DeleteRegionDTOData,
                Message = "Region Deleted Successfully!!"
            });
   }


       
        [HttpPut]
        [Route("UpdateRegion/{id:Guid}")]
        //[ValidateModel]    // we have Iplement it another file it just check weather the model is valid or not 

        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO  RegionsDTOUpdatedData)
        {
            
                // Map Data from DTO into Model
                var region = _mapper.Map<Region>(RegionsDTOUpdatedData);

                //First we check that the RegionDTOUpdatedData exist or not in collection Regions
                var createdRegion = await _regionRepository.UpdateRegionAsync(id, region);

                // Convert Back model into DTO 

                var regionDTOData = _mapper.Map<RegionsDTO>(region);

                // return response back
                return Ok(new
                {
                    Data = regionDTOData,
                    Message = "Region Updated Successfully!!"

                });
           
        }

 
        //---------------- Add Regions
        [HttpPost]
        [Route("AddRegions")]
        //[ValidateModel]
  
        public async Task<IActionResult> AddRegions([FromBody] AddRegionDTO DTOData)
        {
         
                // Map Data from DTO into Model
                 var regionModel = _mapper.Map<Region>(DTOData);//yaha pe hm DTOData ko Convert krte hai Region mai yani Model mai

                // Add the Mapped Data into Model
                var addedRegion = await _regionRepository.AddRegionAsync(regionModel);

                // Return Response again we need to Map Data from Model into DTO Region

                var regionDTO = _mapper.Map<RegionsDTO>(addedRegion);

            return Ok(new
            {
                Data = regionDTO,
                Message = "Region Added Successfully!"

            });
                // 1) ActionMethodName 2)
                // We create this response because if the client need to locate the newly Added Region in Frontend then this will be much helpfull and it will return 201
                //return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id, Message = "Region Added Successfully!" }, regionDTO);
        }
    }
}
